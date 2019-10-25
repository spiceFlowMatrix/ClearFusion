using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes
{
    public class FileManagementService : IFileManagement
    {
        IUnitOfWork _uow;

        public FileManagementService(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        public APIResponse GetSignedURL(DownloadObjectGCBucketModel model)
        {
            APIResponse response = new APIResponse();

            //string googleApplicationCredentail = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");

            try
            {
                string bucketName = Environment.GetEnvironmentVariable("GOOGLE_BUCKET_NAME");

                var scopes = new string[] { "https://www.googleapis.com/auth/devstorage.read_write" };

                ServiceAccountCredential cred;

                using (var stream = new FileStream(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"), FileMode.Open, FileAccess.Read))
                {
                    cred = GoogleCredential.FromStream(stream)
                                           .CreateScoped(scopes)
                                           .UnderlyingCredential as ServiceAccountCredential;
                }

                //var urlSigner = UrlSigner.FromServiceAccountCredential(cred);

                UrlSigner urlSigner = UrlSigner.FromServiceAccountCredential(cred);

                response.data.SignedUrl = urlSigner.Sign(
                    bucketName,
                    model.ObjectName,
                    TimeSpan.FromMinutes(10),
                    HttpMethod.Put,
                    contentHeaders: new Dictionary<string, IEnumerable<string>> {
                    { "Content-Type", new[] { model.FileType } } }
                    );

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception exception)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + exception.Message;
            }
            return response;
        }

        public async Task<APIResponse> SaveUploadedFileInfo(FileManagementModel model)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (model != null)
                {
                    DocumentFileDetail fileDetail = new DocumentFileDetail
                    {
                        IsDeleted = false,
                        CreatedDate = DateTime.UtcNow,
                        CreatedById = model.CreatedById,
                        Name = model.FileName,
                        RawFileSizeBytes = model.FileSize,
                        RawFileMimeType = model.FileType,
                        Description = model.FileName,
                        PageId = model.PageId,
                        StorageDirectoryPath = model.FilePath,
                        DocumentTypeId= model.DocumentTypeId
                    };

                    await _uow.GetDbContext().DocumentFileDetail.AddAsync(fileDetail);
                    await _uow.GetDbContext().SaveChangesAsync();

                    EntitySourceDocumentDetail docDetail = new EntitySourceDocumentDetail
                    {
                        CreatedDate = DateTime.UtcNow,
                        CreatedById = model.CreatedById,
                        DocumentFileId = fileDetail.DocumentFileId,
                        EntityId = model.RecordId,
                        IsDeleted = false
                    };

                    await _uow.GetDbContext().EntitySourceDocumentDetails.AddAsync(docDetail);
                    await _uow.GetDbContext().SaveChangesAsync();
                }
               
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetDocumentFiles(FileModel model)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (model != null)
                {

                    List<FileListModel> fileList = new List<FileListModel>();

                    fileList = await _uow.GetDbContext().EntitySourceDocumentDetails
                                       .Include(x => x.DocumentFileDetail)
                                       .Where(x => x.IsDeleted == false && x.EntityId == model.RecordId
                                              && x.DocumentFileDetail.PageId== model.PageId)
                                       .Select(x => new FileListModel
                                       {
                                           FileName = x.DocumentFileDetail.Name,
                                           FilePath = x.DocumentFileDetail.StorageDirectoryPath,
                                           DocumentFileId = x.DocumentFileId,
                                           DocumentTypeId = x.DocumentFileDetail.DocumentTypeId
                                       }).ToListAsync();

                    if (fileList.Any())
                    {
                        DownloadObjectGCBucketModel bucketModel = new DownloadObjectGCBucketModel();

                        foreach (var item in fileList)
                        {
                            bucketModel.ObjectName = item.FilePath;

                            APIResponse responses = await DownloadFileFromBucket(bucketModel);

                            if (!string.IsNullOrEmpty(responses.data.SignedUrl))
                            {
                                item.FileSignedURL = responses.data.SignedUrl;
                            }
                        }

                        response.data.DocumentFileList = fileList;
                    }
                }
                
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        #region "DownloadFileFromBucket"
        public async Task<APIResponse> DownloadFileFromBucket(DownloadObjectGCBucketModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                string bucketName = Environment.GetEnvironmentVariable("GOOGLE_BUCKET_NAME");

                response.data.SignedUrl = await GCBucket.GetSignedURL(bucketName, model.ObjectName);
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex;
            }
            return response;
        }
        #endregion

        #region GetSignedURLStore

        public async Task<StoreDocumentModel> GetFilesByRecordIdAndDocumentType(FileModel model)
        {
            StoreDocumentModel documentModel = new StoreDocumentModel();

            try
            {
                if (model != null)
                {

                    FileListModel fileModel = new FileListModel();

                    fileModel = await _uow.GetDbContext().EntitySourceDocumentDetails
                                       .Include(x => x.DocumentFileDetail)
                                       .Where(x => x.IsDeleted == false && x.EntityId == model.RecordId
                                              && x.DocumentFileDetail.PageId == model.PageId &&
                                              x.DocumentFileDetail.DocumentTypeId == model.DocumentTypeId)
                                       .Select(x => new FileListModel
                                       {
                                           FileName = x.DocumentFileDetail.Name,
                                           FilePath = x.DocumentFileDetail.StorageDirectoryPath,
                                           DocumentFileId = x.DocumentFileId,
                                           DocumentTypeId = x.DocumentFileDetail.DocumentTypeId
                                       }).FirstOrDefaultAsync();

                    if (fileModel != null)
                    {
                        DownloadObjectGCBucketModel bucketModel = new DownloadObjectGCBucketModel();
                        bucketModel.ObjectName = fileModel.FilePath;
                        APIResponse responses = await DownloadFileFromBucket(bucketModel);

                        documentModel.SignedURL = responses.data.SignedUrl;
                        documentModel.DocumentFileId = fileModel.DocumentFileId ?? 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return documentModel;
        }

        #endregion

        #region "DeleteDocumentFile"
        public async Task<APIResponse> DeleteDocumentFile(FileModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model.DocumentFileId != null)
                {
                    //switch (model.PageId)
                    //{
                    //    case (int)FileSourceEntityTypes.Voucher:

                    //        //var result = await _uow.GetDbContext()
                    //        //           .VoucherDocumentDetail
                    //        //           .Include(x => x.DocumentFileDetail)
                    //        //           .FirstOrDefaultAsync(x => x.IsDeleted == false && x.DocumentFileId == model.DocumentFileId);

                    //        //result.IsDeleted = true;
                    //        //result.DocumentFileDetail.IsDeleted = true;

                    //        // _uow.GetDbContext().VoucherDocumentDetail.Update(result);
                    //        // await _uow.GetDbContext().SaveChangesAsync();

                    //        break;
                    //}


                    var result = await _uow.GetDbContext()
                               .EntitySourceDocumentDetails
                               .Include(x => x.DocumentFileDetail)
                               .FirstOrDefaultAsync(x => x.IsDeleted == false && x.DocumentFileId == model.DocumentFileId);

                    result.IsDeleted = true;
                    result.DocumentFileDetail.IsDeleted = true;

                    _uow.GetDbContext().EntitySourceDocumentDetails.Update(result);
                    await _uow.GetDbContext().SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex;
            }
            return response;
        }
        #endregion

        #region UpdateSavedFileInfo
        public async Task<APIResponse> UpdateUploadedFileInfo(FileManagementModel model)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (model != null)
                {
                    DocumentFileDetail docDetail = await _uow.GetDbContext().DocumentFileDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.DocumentFileId == model.DocumentFileId);

                    docDetail.ModifiedById = model.ModifiedById;
                    docDetail.ModifiedDate = DateTime.UtcNow;
                    docDetail.Name = model.FileName;
                    docDetail.RawFileMimeType = model.FileType;
                    docDetail.RawFileSizeBytes = model.FileSize;
                    docDetail.StorageDirectoryPath = model.FilePath;

                    await _uow.GetDbContext().SaveChangesAsync();
                }

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
        #endregion
    }
}
