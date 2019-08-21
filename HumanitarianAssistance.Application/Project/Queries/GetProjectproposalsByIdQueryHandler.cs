using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Project.Models;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectproposalsByIdQueryHandler : IRequestHandler<GetProjectproposalsByIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetProjectproposalsByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetProjectproposalsByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            ProjectProposalModel obj = new ProjectProposalModel();
            try
            {
                ProjectProposalDetail detail = await _dbContext.ProjectProposalDetail.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId &&
                                                                                                               x.IsDeleted == false);
                ApproveProjectDetails Projectdetail = await _dbContext.ApproveProjectDetails.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId &&
                                                                                                                      x.IsDeleted == false);
                if (detail != null)
                {

                    obj.ProjectProposaldetailId = detail.ProjectProposaldetailId;
                    obj.FolderName = detail.FolderName;
                    obj.FolderId = detail.FolderId;
                    obj.ProposalFileName = detail.ProposalFileName;
                    obj.ProjectId = detail.ProjectId;
                    obj.ProposalFileId = detail.ProposalFileId;
                    obj.EDIFileName = detail.EDIFileName;
                    obj.EdiFileId = detail.EdiFileId;
                    obj.BudgetFileName = detail.BudgetFileName;
                    obj.BudgetFileId = detail.BudgetFileId;
                    obj.ConceptFileName = detail.ConceptFileName;
                    obj.ConceptFileId = detail.ConceptFileId;
                    obj.PresentationFileName = detail.PresentationFileName;
                    obj.ProposalWebLink = detail.ProposalWebLink;
                    obj.EDIFileWebLink = detail.EDIFileWebLink;
                    obj.BudgetFileWebLink = detail.BudgetFileWebLink;
                    obj.ConceptFileWebLink = detail.ConceptFileWebLink;
                    obj.PresentationFileWebLink = detail.PresentationFileWebLink;
                    obj.ProposalExtType = detail.ProposalExtType;
                    obj.EDIFileExtType = detail.EDIFileExtType;
                    obj.BudgetFileExtType = detail.BudgetFileExtType;
                    obj.ConceptFileExtType = detail.ConceptFileExtType;
                    obj.PresentationExtType = detail.PresentationExtType;
                    obj.ProposalStartDate = detail.ProposalStartDate;
                    obj.ProposalBudget = detail.ProposalBudget;
                    obj.ProposalDueDate = detail.ProposalDueDate;
                    obj.ProjectAssignTo = detail.ProjectAssignTo;
                    obj.IsProposalAccept = detail.IsProposalAccept;
                    obj.CurrencyId = detail.CurrencyId;
                    obj.UserId = detail.UserId;
                    obj.IsApproved = Projectdetail?.IsApproved;
                    response.data.ProjectProposalModel = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {

                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.NoDataFound;
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
