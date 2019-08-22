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
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        private IHostingEnvironment _hostingEnvironment;

        public FileManagementService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
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
                    DocumentFileDetail fileDetail = new DocumentFileDetail();
                    fileDetail.IsDeleted = false;
                    fileDetail.CreatedDate = DateTime.UtcNow;
                    fileDetail.CreatedById = model.CreatedById;
                    fileDetail.Name = model.FileName;
                    fileDetail.RawFileSizeBytes = model.FileSize;
                    fileDetail.RawFileMimeType = model.FileType;
                    fileDetail.Description= model.FileName;
                    fileDetail.PageId = model.PageId;
                    fileDetail.StorageDirectoryPath = model.FilePath;

                    await _uow.GetDbContext().DocumentFileDetail.AddAsync(fileDetail);
                    await _uow.GetDbContext().SaveChangesAsync();

                    //switch (model.PageId)
                    //{
                    //    case (int)FileSourceEntityTypes.Voucher:

                            //add entry to voucherdocumentdetail table
                            EntitySourceDocumentDetail docDetail = new EntitySourceDocumentDetail();
                    docDetail.CreatedDate = DateTime.UtcNow;
                    docDetail.CreatedById = model.CreatedById;
                    docDetail.DocumentFileId = fileDetail.DocumentFileId;
                    docDetail.EntityId = model.RecordId;
                    docDetail.IsDeleted = false;
                     await _uow.GetDbContext().EntitySourceDocumentDetails.AddAsync(docDetail);
                     await _uow.GetDbContext().SaveChangesAsync();
                            //break;
                    //}
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

                    //switch (model.PageId)
                    //{
                    //    case (int)FileSourceEntityTypes.Voucher:

                    fileList = await _uow.GetDbContext().EntitySourceDocumentDetails
                                       .Include(x => x.DocumentFileDetail)
                                       .Where(x => x.IsDeleted == false && x.EntityId == model.RecordId
                                              && x.DocumentFileDetail.PageId== model.PageId)
                                       .Select(x => new FileListModel
                                       {
                                           FileName = x.DocumentFileDetail.Name,
                                           FilePath = x.DocumentFileDetail.StorageDirectoryPath,
                                           DocumentFileId = x.DocumentFileId
                                       }).ToListAsync();
                    //        break;
                    //}

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
    }
}
