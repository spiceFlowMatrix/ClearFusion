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
    public class StartProposalDragAndDropCommandHandler : IRequestHandler<StartProposalDragAndDropCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public StartProposalDragAndDropCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(StartProposalDragAndDropCommand request, CancellationToken cancellationToken)
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
                    Console.WriteLine($"*********GoogleServiceAccountDirectory :{GoogleServiceAccountDirectory}");
                    GoogleServiceAccountDirectory = GoogleServiceAccountDirectory.Replace(@"\", "/");
                    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", GoogleServiceAccountDirectory);
                }

                using (Stream objStream = new FileStream(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"), FileMode.Open, FileAccess.Read))
                {
                    Console.WriteLine("--------- Environment Credential Read successfully----- ----------");

                    string BucketName = Environment.GetEnvironmentVariable("GOOGLE_BUCKET_NAME");
                    //Console.WriteLine($"BucketName:{BucketName}");
                    //Console.WriteLine("--------- check For upload----- ----------");
                    string folderWithProposalFile = StaticResource.ProjectsFolderName + "/" + folderName + "/" + request.FileName;

                    ProjectProposalDetail proposaldata = new ProjectProposalDetail();
                    try
                    {
                        // --------------------code to get response credential from environment variables.
                        string obj = await GCBucket.UploadOtherProposalDocuments(BucketName, folderWithProposalFile, request.file, request.FileName, request.ext);
                        proposaldata = await _dbContext.ProjectProposalDetail.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId && x.IsDeleted == false);
                        Console.WriteLine($"Final bucket response : {obj}");

                        if (obj != null)
                        {
                            if (proposaldata == null)
                            {
                                proposaldata = new ProjectProposalDetail();
                                proposaldata.FolderName = folderName;
                                proposaldata.ProposalFileName = request.FileName;
                                proposaldata.ProposalWebLink = obj;
                                proposaldata.ProjectId = request.ProjectId;
                                proposaldata.IsDeleted = false;
                                proposaldata.ProposalExtType = ".docx";
                                proposaldata.CreatedById = request.CreatedById;
                                proposaldata.CreatedDate = request.CreatedDate;
                                proposaldata.ProposalStartDate = DateTime.UtcNow;
                                await _dbContext.ProjectProposalDetail.AddAsync(proposaldata);
                                await _dbContext.SaveChangesAsync();
                            }
                            else
                            {
                                proposaldata.FolderName = folderName;
                                proposaldata.ProposalFileName = request.FileName;
                                proposaldata.ProposalWebLink = obj;
                                proposaldata.ProjectId = request.ProjectId;
                                proposaldata.IsDeleted = false;
                                proposaldata.ProposalExtType = ".docx";
                                proposaldata.ModifiedDate = request.ModifiedDate;
                                proposaldata.ModifiedById = request.ModifiedById;
                                proposaldata.ProposalStartDate = DateTime.UtcNow;
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

                    response.data.ProjectProposalDetail = proposaldata;
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
