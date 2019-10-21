using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class DownloadFileFromBucketCommandHandler : IRequestHandler<DownloadFileFromBucketCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public DownloadFileFromBucketCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(DownloadFileFromBucketCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                string bucketName = Environment.GetEnvironmentVariable("GOOGLE_BUCKET_NAME");
                response.data.SignedUrl = await GCBucket.GetSignedURL(bucketName, request.ObjectName);
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
