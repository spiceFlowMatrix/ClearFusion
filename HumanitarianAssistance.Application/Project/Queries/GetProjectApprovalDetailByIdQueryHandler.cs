using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectApprovalDetailByIdQueryHandler : IRequestHandler<GetProjectApprovalDetailByIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetProjectApprovalDetailByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetProjectApprovalDetailByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ApproveProjectDetails projectDetail = await _dbContext.ApproveProjectDetails
                                                                .Include(x => x.ProjectDetail)
                                                                .ThenInclude(x=> x.ProjectProposalDetail)
                                                                .FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId && x.IsDeleted == false);

                if (projectDetail != null)
                {
                    ApproveProjectDetailsModel detail = new ApproveProjectDetailsModel()
                    {
                        ProjectId = projectDetail.ProjectId,
                        CommentText = projectDetail.CommentText,
                        FileName = projectDetail.FileName,
                        FilePath = projectDetail.FilePath,
                        IsApproved = projectDetail.IsApproved,
                        IsProposalRejected = projectDetail.ProjectDetail.ProjectProposalDetail.IsProposalAccept,
                        ApproveProjrctId = projectDetail.ApproveProjrctId
                    };
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                    response.ResponseData = detail;
                }
                else
                {
                    response.StatusCode = StaticResource.notFoundCode;
                    response.Message = "Detail not found";
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