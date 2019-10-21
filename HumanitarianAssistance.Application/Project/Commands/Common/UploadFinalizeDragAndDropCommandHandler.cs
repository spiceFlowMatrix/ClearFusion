using System;
using System.IO;
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
    public class UploadFinalizeDragAndDropCommandHandler : IRequestHandler<UploadFinalizeDragAndDropCommand, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public UploadFinalizeDragAndDropCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(UploadFinalizeDragAndDropCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ProjectDetail projectDetail = await _dbContext.ProjectDetail.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId && x.IsDeleted == false);
                if (projectDetail == null)
                {
                    throw new Exception("Project Id not found");
                }
                string folderName = projectDetail.ProjectCode;
                //code to read credential from environment variables
                //Console.WriteLine("---------- Before Credential create path----------");
                string googleApplicationCredentail = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
                //Console.WriteLine($"*******************googleApplicationCredentail are:{googleApplicationCredentail}");
                if (googleApplicationCredentail == null)
                {
                    string GoogleServiceAccountDirectory = Path.Combine(Directory.GetCurrentDirectory(), "GoogleCredentials/" + "credentials.json");
                    Console.WriteLine("-----UploadFinalizeDragAndDrop cred null-------");
                    GoogleServiceAccountDirectory = GoogleServiceAccountDirectory.Replace(@"\", "/");
                    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", GoogleServiceAccountDirectory);
                }

                using (Stream objStream = new FileStream(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"), FileMode.Open, FileAccess.Read))
                {
                    //Console.WriteLine("--------- Environment Credential Read successfully----- ----------");

                    string BucketName = Environment.GetEnvironmentVariable("GOOGLE_BUCKET_NAME");
                    //Console.WriteLine($"BucketName:{BucketName}");
                    //Console.WriteLine("--------- check For upload----- ----------");
                    string folderWithProposalFile = StaticResource.ProjectsFolderName + "/" + folderName + "/" + request.FileName;

                    WinProjectDetails objRes = new WinProjectDetails();
                    try
                    {
                        // --------------------code to get response credential from environment variables.
                        string obj = await GCBucket.UploadOtherProposalDocuments(BucketName, folderWithProposalFile, request.file, request.FileName, request.ext);
                        objRes = await _dbContext.WinProjectDetails.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId && x.IsDeleted == false);

                        //Console.WriteLine($"Final bucket response : {obj}");

                        if (obj != null)
                        {
                            if (objRes == null)
                            {
                                objRes = new WinProjectDetails();
                                objRes.ProjectId = request.ProjectId;
                                objRes.FileName = request.FileName;
                                objRes.FilePath = obj;
                                objRes.CommentText = request.CommentText;
                                objRes.UploadedFile = null;
                                objRes.IsDeleted = false;
                                objRes.CreatedById = request.CreatedById;
                                objRes.IsWin = request.IsWin;
                                objRes.CreatedDate = request.CreatedDate;
                                await _dbContext.WinProjectDetails.AddAsync(objRes);
                                await _dbContext.SaveChangesAsync();
                            }
                            else
                            {
                                objRes.ProjectId = request.ProjectId;
                                objRes.FileName = request.FileName;
                                objRes.CommentText = request.CommentText;
                                objRes.FilePath = obj;
                                objRes.UploadedFile = null;
                                objRes.IsDeleted = false;
                                objRes.ModifiedDate = request.ModifiedDate;
                                objRes.ModifiedById = request.ModifiedById;
                                objRes.IsWin = request.IsWin;
                                await _dbContext.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            throw new Exception("Failed to upload. Try again!");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Upload using Environment variable failed");
                        Console.WriteLine($"--------------Using environment variable exception--: {ex}");

                    }

                    response.data.WinProjectDetails = objRes;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
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
