using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServices;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.FileManagement.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.FileManagement.Queries
{
    public class GetSignedURLByDocumentFileIdQueryHandler: IRequestHandler<GetSignedURLByDocumentFileIdQuery, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IFileManagementService _fileManagementService;

        public GetSignedURLByDocumentFileIdQueryHandler(HumanitarianAssistanceDbContext dbContext, IFileManagementService fileManagementService)
        {
            _dbContext = dbContext;
            _fileManagementService= fileManagementService;
        }

        public async Task<ApiResponse> Handle(GetSignedURLByDocumentFileIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                DocumentFileDetail fileDetail = await _dbContext.DocumentFileDetail
                                                          .FirstOrDefaultAsync(x => x.IsDeleted == false
                                                            &&
                                                           x.DocumentFileId == request.DocumentFileId);

                if (fileDetail != null)
                {

                    DownloadObjectGCBucketModel model = new DownloadObjectGCBucketModel
                    {
                        ObjectName = fileDetail.StorageDirectoryPath
                    };

                   ApiResponse signedURLResponse = await _fileManagementService.DownloadFileFromBucket(model);

                   if(!string.IsNullOrEmpty(signedURLResponse.data.SignedUrl))
                   {
                       response.data.SignedUrl = signedURLResponse.data.SignedUrl; 
                   }
                }

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
}
}