using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddProjectProposalFileDetailCommandHandler: IRequestHandler<AddProjectProposalFileDetailCommand, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public AddProjectProposalFileDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(AddProjectProposalFileDetailCommand request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            
            try
            {
                ProjectDetail projectDetail = await _dbContext.ProjectDetail.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId && x.IsDeleted == false);
                if (projectDetail == null)
                {
                    throw new Exception("Project Id not found");
                }
               
                    ProjectProposalDocument proposaldata = new ProjectProposalDocument();
                    try
                    {
                        var details = await _dbContext.ProjectProposalDetail.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId && x.IsDeleted == false);
                        if (details == null)
                        {
                            ProjectProposalDetail objDetails = new ProjectProposalDetail()
                            {
                                ProjectId = request.ProjectId,
                                ProposalStartDate = DateTime.UtcNow
                            };
                            await _dbContext.ProjectProposalDetail.AddAsync(objDetails);
                            await _dbContext.SaveChangesAsync();
                        }
                        else if (details.ProposalStartDate == null)
                        {
                            details.ProposalStartDate = DateTime.UtcNow;
                            await _dbContext.SaveChangesAsync();
                        }
                        proposaldata = await _dbContext.ProjectProposalDocument.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId && x.ProposalDocumentName == request.FileName && x.IsDeleted == false);

                        if (!string.IsNullOrEmpty(request.FilePath))
                        {
                            if (proposaldata == null)
                            {
                                proposaldata = new ProjectProposalDocument();
                                proposaldata.ProposalDocumentName = request.FileName;
                                proposaldata.ProposalWebLink = request.FilePath;
                                proposaldata.ProjectId = request.ProjectId;
                                proposaldata.IsDeleted = false;
                                proposaldata.ProposalExtType = request.Extension;
                                proposaldata.CreatedById = request.CreatedById;
                                proposaldata.CreatedDate = request.CreatedDate;
                                proposaldata.ProposalDocumentTypeId = request.ProposalTypeId;
                                await _dbContext.ProjectProposalDocument.AddAsync(proposaldata);
                                await _dbContext.SaveChangesAsync();
                                
                                var username = (from u in _dbContext.UserDetails
                                                where u.AspNetUserId == request.CreatedById && u.IsDeleted == false
                                                select (u.FirstName + ' ' + u.LastName)).FirstOrDefault();
                                ProjectProposalModelList objProposalModel = new ProjectProposalModelList()
                                {
                                    ProjectProposaldetailId = proposaldata.ProjectProposalDocumnetId,
                                    ProposalDocumentName = proposaldata.ProposalDocumentName,
                                    ProposalWebLink = proposaldata.ProposalWebLink,
                                    ProjectId = proposaldata.ProjectId,
                                    ProposalExtType = proposaldata.ProposalExtType,
                                    CreatedDate = proposaldata.CreatedDate,
                                    ProposalDocumentTypeId = proposaldata.ProposalDocumentTypeId,
                                    UserName = username.ToString()
                                };

                                response.Add("proposal", objProposalModel);
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
                
            }
            catch (Exception ex)
            {
               throw ex;
            }
            return response;
        }
    }
}