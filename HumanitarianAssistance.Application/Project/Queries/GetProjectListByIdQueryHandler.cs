using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Linq;
using HumanitarianAssistance.Application.Project.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectListByIdQueryHandler: IRequestHandler<GetProjectListByIdQuery, ApiResponse>
    {
         private HumanitarianAssistanceDbContext _dbContext;

        public GetProjectListByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {

            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(GetProjectListByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var ProjectDetail = await (from obj in _dbContext.ProjectDetail
                                     join win in _dbContext.WinProjectDetails on obj.ProjectId equals win.ProjectId into p
                                     from c in p.DefaultIfEmpty()
                                     join approve in _dbContext.ApproveProjectDetails on obj.ProjectId equals approve.ProjectId into z
                                     from approve in z.DefaultIfEmpty()
                                     join Proposal in _dbContext.ProjectProposalDetail on obj.ProjectId equals Proposal.ProjectId into pr
                                     from Proposal in pr.DefaultIfEmpty()
                                     join phase in _dbContext.ProjectPhaseDetails on obj.ProjectPhaseDetailsId equals phase.ProjectPhaseDetailsId
                                     select new ProjectDetailModel
                                     {
                                         ProjectId = obj.ProjectId,
                                         ProjectCode = obj.ProjectCode,
                                         DirectorId = obj.DirectorId,
                                         ReviewerId = obj.ReviewerId,
                                         ProjectName = obj.ProjectName,
                                         ProjectDescription = obj.ProjectDescription,
                                         ProjectPhaseDetailsId = phase.ProjectPhaseDetailsId,
                                         IsWin = c.IsWin,
                                         IsApproved = approve.IsApproved,
                                         IsProposalSubmit = Proposal.IsProposalAccept,
                                         IsCriteriaEvaluationSubmit = obj.IsCriteriaEvaluationSubmit,
                                         IsDelete = obj.IsDeleted
                                         //IsProposalComplate = obj.IsProposalComplate,
                                     }).FirstOrDefaultAsync(x => x.ProjectId == request.Id && x.IsDelete == false);


                response.data.ProjectDetailModel1 = ProjectDetail;
                response.StatusCode = 200;
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