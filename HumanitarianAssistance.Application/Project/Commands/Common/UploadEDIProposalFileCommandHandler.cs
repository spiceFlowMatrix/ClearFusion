using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class UploadEDIProposalFileCommandHandler : IRequestHandler<UploadEDIProposalFileCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public UploadEDIProposalFileCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(UploadEDIProposalFileCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                long projectId = request.ProjectId;

                var folderDetail = await _dbContext.ProjectProposalDetail.FirstOrDefaultAsync(x => x.ProjectId == projectId && x.IsDeleted == false);

                //Console.WriteLine("------Before other file Credential path Upload----------");

                string googleApplicationCredentail = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
                //Console.WriteLine($"*******************googleApplicationCredentail are:{googleApplicationCredentail}");
                if (googleApplicationCredentail == null)
                {
                    string GoogleServiceAccountDirectory = Path.Combine(Directory.GetCurrentDirectory(), "GoogleCredentials/" + "credentials.json");
                    //Console.WriteLine($"*********GoogleServiceAccountDirectory :{GoogleServiceAccountDirectory}");
                    GoogleServiceAccountDirectory = GoogleServiceAccountDirectory.Replace(@"\", "/");
                    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", GoogleServiceAccountDirectory);
                }

                GoogleCredentialModel result = new GoogleCredentialModel
                {
                    ApplicationName = StaticResource.ApplicationName,
                    BucketName = StaticResource.BucketName,
                    EmailId = StaticResource.EmailId,
                    ProjectId = StaticResource.ProjectId,
                    Projects = StaticResource.ProjectsFolderName,
                    HR = StaticResource.HRFolderName,
                    Accounting = StaticResource.AccountingFolderName,
                    Store = StaticResource.StoreFolderName
                };

                using (Stream objStream = new FileStream(Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS"), FileMode.Open, FileAccess.Read))
                {

                    var proposaldata = await _dbContext.ProjectProposalDetail.FirstOrDefaultAsync(x => x.ProjectId == projectId && x.IsDeleted == false);
                    string BucketName = Environment.GetEnvironmentVariable("GOOGLE_BUCKET_NAME");
                    string folderWithProposalFile = StaticResource.ProjectsFolderName + "/" + folderDetail.FolderName + "/" + request.fileName;

                    //Console.WriteLine($"BucketName:{BucketName}");
                    string uploadedFileResponse = await GCBucket.UploadOtherProposalDocuments(BucketName, folderWithProposalFile, request.file, request.fileName, request.ext);
                    if (uploadedFileResponse != null)
                    {
                        ProjectProposalDetail proposaldetails = await _dbContext.ProjectProposalDetail.FirstOrDefaultAsync(x => x.ProjectId == projectId && x.IsDeleted == false);

                        if (proposaldetails == null)
                        {
                            proposaldetails = new ProjectProposalDetail();
                        }

                        if (request.ProposalType == "Proposal")
                        {
                            proposaldetails.FolderName = folderDetail.FolderName;
                            proposaldetails.ProposalFileName = request.fileName;
                            proposaldetails.ProposalWebLink = uploadedFileResponse;
                            proposaldetails.ProjectId = request.ProjectId;
                            proposaldetails.CreatedDate = request.CreatedDate;
                            proposaldetails.IsDeleted = false;
                            proposaldetails.CreatedById = request.CreatedById;
                            response.data.ProposalWebLink = uploadedFileResponse;
                            response.data.ProposalWebLinkExtType = request.ext;
                        }
                        else
                        {
                            if (request.ProposalType == "EOI")
                            {
                                proposaldetails.FolderName = folderDetail.FolderName;
                                proposaldetails.EDIFileName = request.fileName;
                                proposaldetails.EDIFileWebLink = uploadedFileResponse;
                                proposaldetails.EDIFileExtType = request.ext;
                                // response folder path
                                response.data.EDIWebLink = uploadedFileResponse;
                                response.data.EDIWebLinkExtType = request.ext;
                            }
                            else if (request.ProposalType == "BUDGET")
                            {
                                proposaldetails.FolderName = folderDetail.FolderName;

                                proposaldetails.BudgetFileName = request.fileName;
                                proposaldetails.BudgetFileWebLink = uploadedFileResponse;
                                proposaldetails.BudgetFileExtType = request.ext;

                                // response folder path
                                response.data.BudgetWebLink = uploadedFileResponse;
                                response.data.BudgetWebLinkExtType = request.ext;
                            }
                            else if (request.ProposalType == "CONCEPT")
                            {
                                proposaldetails.FolderName = folderDetail.FolderName;
                                proposaldetails.ConceptFileName = request.fileName;
                                proposaldetails.ConceptFileWebLink = uploadedFileResponse;
                                proposaldetails.ConceptFileExtType = request.ext;
                                response.data.ConceptWebLink = uploadedFileResponse;
                                response.data.ConceptWebLinkExtType = request.ext;
                            }
                            else if (request.ProposalType == "PRESENTATION")
                            {
                                proposaldetails.FolderName = folderDetail.FolderName;
                                proposaldetails.PresentationFileName = request.fileName;
                                proposaldetails.PresentationFileWebLink = uploadedFileResponse;
                                proposaldetails.PresentationExtType = request.ext;

                                // response folder path
                                response.data.PresentationWebLink = uploadedFileResponse;
                                response.data.PresentationWebLinkExtType = request.ext;
                            }
                            proposaldata.ProjectId = request.ProjectId;
                            proposaldata.ModifiedDate = request.ModifiedDate;

                        }

                        if (proposaldetails.ProjectProposaldetailId == 0)
                        {
                            await _dbContext.ProjectProposalDetail.AddAsync(proposaldetails);
                            await _dbContext.SaveChangesAsync();

                        }
                        else
                        {
                            await _dbContext.SaveChangesAsync();
                        }

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    }

                    else
                    {
                        throw new Exception("Failed to upload. Try again!");
                    }
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
