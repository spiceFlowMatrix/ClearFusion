using HumanitarianAssistance.Application.FileManagement.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.FileManagement.Queries
{
    public class GetDocumentFilesQueryHandler : IRequestHandler<GetDocumentFilesQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetDocumentFilesQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetDocumentFilesQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (request != null)
                {
                    List<FileListModel> fileList = new List<FileListModel>();

                    var query = _dbContext.EntitySourceDocumentDetails
                                       .Include(x => x.DocumentFileDetail)
                                       .Where(x => x.IsDeleted == false && x.DocumentFileDetail.IsDeleted == false && x.EntityId == request.RecordId
                                              && x.DocumentFileDetail.PageId == request.PageId)
                                       .AsQueryable();


                    if (request.DocumentTypeId != null && request.DocumentTypeId != 0)
                    {
                        query = query.Where(x => x.DocumentFileDetail.DocumentTypeId == request.DocumentTypeId);
                    }

                    fileList = await query.Select(x => new FileListModel
                    {
                        FileName = x.DocumentFileDetail.Name,
                        FilePath = x.DocumentFileDetail.StorageDirectoryPath,
                        DocumentFileId = x.DocumentFileId,
                        DocumentTypeId = x.DocumentFileDetail.DocumentTypeId,
                        StorageDirectoryPath = x.DocumentFileDetail.StorageDirectoryPath,
                        Date= x.DocumentFileDetail.CreatedDate.Value.ToShortDateString()
                    }).ToListAsync();

                    if (fileList.Any())
                    {
                        DownloadObjectGCBucketModel bucketModel = new DownloadObjectGCBucketModel();

                        foreach (var item in fileList)
                        {
                            bucketModel.ObjectName = item.FilePath;

                            ApiResponse responses = await DownloadFileFromBucket(bucketModel);

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
                response.Message = ex.Message;
            }
            return response;
        }

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
    }
}
