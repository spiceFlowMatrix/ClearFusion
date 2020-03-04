using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.FileManagement.Queries
{
    public class GetSignedURLQueryHandler : IRequestHandler<GetSignedURLQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetSignedURLQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetSignedURLQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            
            try
            {
                string bucketName = Environment.GetEnvironmentVariable("GOOGLE_BUCKET_NAME");
                // string bucketName = "cf-prod-cha";


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
                    request.ObjectName,
                    TimeSpan.FromMinutes(10),
                    HttpMethod.Put,
                    contentHeaders: new Dictionary<string, IEnumerable<string>> {
                    { 
                        "Content-Type", new[] { request.FileType } 
                    
                    } }
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
    }
}
