using System;
using System.Linq;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.FileManagement.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.CommonServices
{
    public class FileManagementService : IFileManagementService
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public FileManagementService(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StoreDocumentModel> GetFilesByRecordIdAndDocumentType(FileModel model)
        {
            StoreDocumentModel documentModel = new StoreDocumentModel();

            try
            {
                if (model != null)
                {

                    FileListModel fileModel = new FileListModel();

                    fileModel = await _dbContext.EntitySourceDocumentDetails
                                       .Include(x => x.DocumentFileDetail)
                                       .Where(x => x.IsDeleted == false && x.DocumentFileDetail.IsDeleted== false && x.EntityId == model.RecordId
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
                        ApiResponse responses = await DownloadFileFromBucket(bucketModel);

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

        #region "DownloadFileFromBucket"
        public async Task<ApiResponse> DownloadFileFromBucket(DownloadObjectGCBucketModel model)
        {
            ApiResponse response = new ApiResponse();
            
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
        
    }
}