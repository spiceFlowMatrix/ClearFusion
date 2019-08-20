using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using HumanitarianAssistance.Application.Project.Models;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectSubActivityDetailsQueryHandler : IRequestHandler<GetProjectSubActivityDetailsQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetProjectSubActivityDetailsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetProjectSubActivityDetailsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var projectActivityDetails = await _dbContext.ProjectActivityDetail
                                          .Include(p => p.ProjectSubActivityList)
                                          .FirstOrDefaultAsync(v => v.IsDeleted == false &&
                                                      v.ActivityId == request.projectId
                                          );

                List<ProjectSubActivityListModel> activityDetaillist = new List<ProjectSubActivityListModel>();

                activityDetaillist = projectActivityDetails.ProjectSubActivityList.Select(b => new ProjectSubActivityListModel
                {
                    ActivityId = b.ActivityId,
                    BudgetLineId = b.BudgetLineId,
                    EmployeeID = b.EmployeeID,
                    PlannedStartDate = b.PlannedStartDate,
                    PlannedEndDate = b.PlannedEndDate,
                    Recurring = b.Recurring,
                    RecurrinTypeId = b.RecurrinTypeId,
                    IsCompleted = b.IsCompleted,
                    ActivityDescription = b.ActivityDescription,
                    ChallengesAndSolutions = b.ChallengesAndSolutions,
                    Target = b.Target,
                    Achieved = b.Achieved,
                    ActualStartDate = b.ActualStartDate,
                    ActualEndDate = b.ActualEndDate,
                    StatusId = b.StatusId,
                    SubActivityTitle = b.SubActivityTitle
                }).OrderByDescending(x => x.ActivityId)
                  .ToList();
                response.data.ProjectSubActivityListModel = activityDetaillist;
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
