using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
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
    public class AddEditProjectproposalsCommandHandler : IRequestHandler<AddEditProjectproposalsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditProjectproposalsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditProjectproposalsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            ProjectProposalDetail details = new ProjectProposalDetail();
            try
            {
                details = await _dbContext.ProjectProposalDetail.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId && x.IsDeleted == false);

                if (details == null)
                {
                    details = new ProjectProposalDetail
                    {
                        ProposalDueDate = request.ProposalDueDate,
                        ProjectAssignTo = request.UserId,
                        IsProposalAccept = request.IsProposalAccept,
                        ProjectId = request.ProjectId.Value,
                        CurrencyId = request.CurrencyId,
                        UserId = request.UserId,
                        IsDeleted = false,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.UtcNow
                    };
                    await _dbContext.ProjectProposalDetail.AddAsync(details);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    details.ProposalBudget = request.ProposalBudget;
                    details.ProposalDueDate = request.ProposalDueDate;
                    details.ProjectAssignTo = request.UserId;
                    details.IsProposalAccept = request.IsProposalAccept;
                    details.ProjectId = request.ProjectId.Value;
                    details.CurrencyId = request.CurrencyId;
                    details.UserId = request.UserId;
                    details.ModifiedById = request.ModifiedById;
                    details.ModifiedDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();

                    // Note: check proposal is accepted then make false entry for isApproved
                    if (details.IsProposalAccept == true)
                    {
                        ApproveProjectDetails obj = await _dbContext.ApproveProjectDetails.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId &&
                                                                                                                            x.IsDeleted == false);
                        if (obj != null)
                        {
                            obj.IsApproved = obj.IsApproved == false ? null : obj.IsApproved;
                            _dbContext.ApproveProjectDetails.Update(obj);
                            await _dbContext.SaveChangesAsync();

                        }

                    }
                }
                response.data.ProjectProposalDetail = details;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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
