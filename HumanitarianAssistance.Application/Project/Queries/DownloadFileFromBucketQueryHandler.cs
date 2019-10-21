using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class DownloadFileFromBucketQueryHandler : IRequestHandler<DownloadFileFromBucketQuery, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public DownloadFileFromBucketQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(DownloadFileFromBucketQuery request, CancellationToken cancellationToken)
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
