using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace HumanitarianAssistance.Application.Project.Commands.Common
{
        public class UploadReviewDragAndDropCommandHandler : IRequestHandler<UploadReviewDragAndDropCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public UploadReviewDragAndDropCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(UploadReviewDragAndDropCommand request, CancellationToken cancellationToken)
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
                    //Console.WriteLine($"*********GoogleServiceAccountDirectory :{GoogleServiceAccountDirectory}");
                    Console.WriteLine($"------GoogleServiceAccountDirectory cred null-----");
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

                    ApproveProjectDetails objRes = new ApproveProjectDetails();

                    try
                    {
                        // --------------------code to get response credential from environment variables.
                        string obj = await GCBucket.UploadOtherProposalDocuments(BucketName, folderWithProposalFile, request.file, request.FileName, request.ext);
                        objRes = await _dbContext.ApproveProjectDetails.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId && x.IsDeleted == false);

                        Console.WriteLine($"Final bucket response : {obj}");

                        if (obj != null)
                        {
                            if (objRes == null)
                            {
                                objRes = new ApproveProjectDetails
                                {
                                    ProjectId = request.ProjectId,
                                    FileName = request.FileName,
                                    FilePath = obj,
                                    CommentText = request.CommentText,
                                    UploadedFile = null,
                                    IsDeleted = false,
                                    CreatedById = request.CreatedById,
                                    IsApproved = request.IsApproved,
                                    CreatedDate = request.CreatedDate,
                                    //Review completion date for proposal report when proposal is completed as isapproved is true
                                    ReviewCompletionDate = DateTime.UtcNow
                                };
                                await _dbContext.ApproveProjectDetails.AddAsync(objRes);
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
                                objRes.IsApproved = request.IsApproved;
                                //Review completion date for proposal report when proposal is completed as isapproved is true
                                objRes.ReviewCompletionDate = DateTime.UtcNow;
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

                    response.data.ApproveProjectDetails = objRes;
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
