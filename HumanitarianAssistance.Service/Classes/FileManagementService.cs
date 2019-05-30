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
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<APIResponse> GetSignedURL(DownloadObjectGCBucketModel model)
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
                    { "Content-Type", new[] { "text/plain" } } }
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

                    switch (model.PageId)
                    {
                        case (int)FileSourceEntityTypes.Voucher:

                            //add entry to voucherdocumentdetail table
                            VoucherDocumentDetail voucherDocumentDetail = new VoucherDocumentDetail();
                            voucherDocumentDetail.CreatedDate = DateTime.UtcNow;
                            voucherDocumentDetail.CreatedById = model.CreatedById;
                            voucherDocumentDetail.DocumentFileId = fileDetail.DocumentFileId;
                            voucherDocumentDetail.VoucherNo = model.RecordId;
                            voucherDocumentDetail.IsDeleted = false;
                            await _uow.GetDbContext().VoucherDocumentDetail.AddAsync(voucherDocumentDetail);
                            await _uow.GetDbContext().SaveChangesAsync();
                            break;
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
    }
}
