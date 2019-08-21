using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectActivityAdvanceFilterListQueryHandler : IRequestHandler<GetProjectActivityAdvanceFilterListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetProjectActivityAdvanceFilterListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetProjectActivityAdvanceFilterListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var spActivityList = await _dbContext.LoadStoredProc("get_project_projectactivitylist_filter")
                                      .WithSqlParam("project_id", request.ProjectId)
                                      .WithSqlParam("activity_description", request.ActivityDescription == null ? string.Empty : request.ActivityDescription)
                                      .WithSqlParam("planned_start_date", request.PlannedStartDate == null ? string.Empty : request.PlannedStartDate.Value.ToString())
                                      .WithSqlParam("planned_end_date", request.PlannedEndDate == null ? string.Empty : request.PlannedEndDate.Value.ToString())
                                      .WithSqlParam("actual_start_date", request.ActualStartDate == null ? string.Empty : request.ActualStartDate.Value.ToString())
                                      .WithSqlParam("actual_end_date", request.ActualEndDate == null ? string.Empty : request.ActualEndDate.Value.ToString())

                                      .WithSqlParam("assignee_id", request.AssigneeId)
                                      .WithSqlParam("budget_line_id", request.BudgetLineId)
                                      .WithSqlParam("planning", request.Planning)
                                      .WithSqlParam("implementations", request.Implementation)
                                      .WithSqlParam("completed", request.Completed)

                                      .WithSqlParam("progress_range_min", request.ProgressRangeMin)
                                      .WithSqlParam("progress_range_max", request.ProgressRangeMax)
                                      .WithSqlParam("sleepage_min", request.SleepageMin)
                                      .WithSqlParam("sleepage_max", request.SleepageMax)

                                      .WithSqlParam("duration_min", request.DurationMin)
                                      .WithSqlParam("duration_max", request.DurationMax)

                                      .WithSqlParam("late_start", request.LateStart)
                                      .WithSqlParam("late_end", request.LateEnd)
                                      .WithSqlParam("on_schedule", request.OnSchedule)

                                      .ExecuteStoredProc<SPProjectActivityDetail>();

                var activityList = spActivityList.Select(x => new ProjectActivityModel
                {
                    ActivityId = x.ActivityId,
                    ActivityName = x.ActivityName,
                    ActivityDescription = x.ActivityDescription,
                    PlannedStartDate = x.PlannedStartDate,
                    PlannedEndDate = x.PlannedEndDate,
                    BudgetLineId = x.BudgetLineId,
                    BudgetName = x.BudgetName,
                    EmployeeID = x.EmployeeID,
                    EmployeeName = x.EmployeeName,
                    StatusId = x.StatusId,
                    StatusName = x.StatusName,
                    Recurring = x.Recurring,
                    RecurringCount = x.RecurringCount,
                    RecurrinTypeId = x.RecurrinTypeId,

                    Progress = Math.Round(x.Progress, 2),
                    Slippage = x.Sleepage
                }).ToList();

                response.data.ProjectActivityList = activityList;
                response.data.TotalCount = activityList.Count();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }

}
