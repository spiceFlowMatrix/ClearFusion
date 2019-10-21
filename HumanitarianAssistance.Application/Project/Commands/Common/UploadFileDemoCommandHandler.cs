using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class UploadFileDemoCommandHandler : IRequestHandler<UploadFileDemoCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IHostingEnvironment _hostingEnvironment;
        public UploadFileDemoCommandHandler(HumanitarianAssistanceDbContext dbContext, IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<ApiResponse> Handle(UploadFileDemoCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var file = request.fileData;
                string fileName = request.fileData.FileName;
                string ext = Path.GetExtension(fileName).ToLower();

                // validate file type 
                if (ext == ".doc" || ext == ".docx" || ext == ".txt" || ext == ".xlsx" || ext == ".pdf")
                {
                    // Auth 
                    var webRootfilePath = _hostingEnvironment.WebRootPath;

                    var path = Path.Combine(webRootfilePath, "demo.xlsx");


                    string GoogleServiceAccountDirectory = Path.Combine(Directory.GetCurrentDirectory(), "GoogleCredentials/" + "credentials.json");
                    GoogleServiceAccountDirectory = GoogleServiceAccountDirectory.Replace(@"\", "/");

                    Console.WriteLine($"Linux GoogleServiceAccountDirectory : {GoogleServiceAccountDirectory}");

                    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", GoogleServiceAccountDirectory);
                    using (Stream objStream = new FileStream(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"), FileMode.Open, FileAccess.Read))
                    {
                        Console.WriteLine($"obj stream:{"GOOGLE_APPLICATION_CREDENTIALS"}");
                        //UploadFile(StaticResource.BucketName, @"D:\poonam\newdoc.docx", "abc");

                        var intdata = GCBucket.UploadFile(Environment.GetEnvironmentVariable("GOOGLE_BUCKET_NAME"), path);
                    }

                    // Upload                  
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.FileText;
                }


            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
                await Task.Delay(0);
            }          
            return response;
        }
    }
}
