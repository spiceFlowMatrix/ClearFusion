using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllProjectFilterListQueryHandler : IRequestHandler<GetAllProjectFilterListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllProjectFilterListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllProjectFilterListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            string projectCodeValue = null;
            string projectIdValue = null;
            string descriptionValue = null;
            string projectNameValue = null;

            if (!string.IsNullOrEmpty(request.FilterValue))
            {
                projectCodeValue = request.ProjectCodeFlag ? request.FilterValue.ToLower().Trim() : null;
                projectIdValue = request.ProjectIdFlag ? request.FilterValue.ToLower().Trim() : null;
                descriptionValue = request.DescriptionFlag ? request.FilterValue.ToLower().Trim() : null;
                projectNameValue = request.ProjectNameFlag ? request.FilterValue.ToLower().Trim() : null;

            }

            try
            {

                int totalCount = await _dbContext.ProjectDetail
                                       .Where(v => v.IsDeleted == false &&
                                               !string.IsNullOrEmpty(request.FilterValue) ? (
                                               v.ProjectId.ToString().Trim().Contains(projectIdValue) ||
                                               v.ProjectCode.Trim().ToLower().Contains(projectCodeValue) ||
                                               v.ProjectDescription.Trim().ToLower().Contains(descriptionValue) ||
                                               v.ProjectName.Trim().ToLower().Contains(projectNameValue)
                                               ) : true
                                       )
                                      .AsNoTracking()
                                      .CountAsync();

                var ProjectList = await _dbContext.ProjectDetail

                                      .Where(v => v.IsDeleted == false &&
                                                 !string.IsNullOrEmpty(request.FilterValue) ? (
                                                 v.ProjectId.ToString().Trim().Contains(projectIdValue) ||
                                                 v.ProjectCode.Trim().ToLower().Contains(projectCodeValue) ||
                                                 v.ProjectDescription.Trim().ToLower().Contains(descriptionValue) ||
                                                 v.ProjectName.Trim().ToLower().Contains(projectNameValue)
                                                   ) : true
                                          )
                                          .OrderByDescending(x => x.ProjectId)
                                          .Select(x => new ProjectDetailModel
                                          {
                                              ProjectId = x.ProjectId,
                                              ProjectCode = x.ProjectCode,
                                              ProjectName = x.ProjectName,
                                              ProjectDescription = x.ProjectDescription,
                                              IsWin = _dbContext.WinProjectDetails.Where(y => y.ProjectId == x.ProjectId).Select(y => y.IsWin).FirstOrDefault(),
                                              IsCriteriaEvaluationSubmit = x.IsCriteriaEvaluationSubmit,
                                              ProjectPhase = x.ProjectPhaseDetailsId == x.ProjectPhaseDetails.ProjectPhaseDetailsId ? x.ProjectPhaseDetails.ProjectPhase.ToString() : "",
                                              TotalDaysinHours = x.EndDate == null ? (Convert.ToString(Math.Round(DateTime.UtcNow.Subtract(x.StartDate.Value).TotalHours, 0) + ":" + DateTime.UtcNow.Subtract(x.StartDate.Value).Minutes)) : (Convert.ToString(Math.Round(x.EndDate.Value.Subtract(x.StartDate.Value).TotalHours, 0) + ":" + x.EndDate.Value.Subtract(x.StartDate.Value).Minutes))
                                          })
                                          .Skip(request.pageSize.Value * request.pageIndex.Value)
                                          .Take(request.pageSize.Value)
                                          .ToListAsync();
                response.data.ProjectDetailModel = ProjectList;
                response.data.TotalCount = totalCount;

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