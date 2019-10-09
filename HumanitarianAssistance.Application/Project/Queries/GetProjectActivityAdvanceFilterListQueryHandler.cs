using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;
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
                // var spActivityList = await _dbContext.LoadStoredProc("get_project_projectactivitylist_filter")
                //                       .WithSqlParam("project_id", request.ProjectId)
                //                       .WithSqlParam("activity_description", request.ActivityDescription == null ? string.Empty : request.ActivityDescription)
                //                       .WithSqlParam("planned_start_date", request.PlannedStartDate == null ? string.Empty : request.PlannedStartDate.Value.ToString())
                //                       .WithSqlParam("planned_end_date", request.PlannedEndDate == null ? string.Empty : request.PlannedEndDate.Value.ToString())
                //                       .WithSqlParam("actual_start_date", request.ActualStartDate == null ? string.Empty : request.ActualStartDate.Value.ToString())
                //                       .WithSqlParam("actual_end_date", request.ActualEndDate == null ? string.Empty : request.ActualEndDate.Value.ToString())

                //                       .WithSqlParam("assignee_id", request.AssigneeId)
                //                       .WithSqlParam("budget_line_id", request.BudgetLineId)
                //                       .WithSqlParam("planning", request.Planning)
                //                       .WithSqlParam("implementations", request.Implementation)
                //                       .WithSqlParam("completed", request.Completed)

                //                       .WithSqlParam("progress_range_min", request.ProgressRangeMin)
                //                       .WithSqlParam("progress_range_max", request.ProgressRangeMax)
                //                       .WithSqlParam("sleepage_min", request.SleepageMin)
                //                       .WithSqlParam("sleepage_max", request.SleepageMax)

                //                       .WithSqlParam("duration_min", request.DurationMin)
                //                       .WithSqlParam("duration_max", request.DurationMax)

                //                       .WithSqlParam("late_start", request.LateStart)
                //                       .WithSqlParam("late_end", request.LateEnd)
                //                       .WithSqlParam("on_schedule", request.OnSchedule)
                //                       .WithSqlParam("country_id", request.CountryId)

                //                       .ExecuteStoredProc<SPProjectActivityDetail>();

                // List<SPProjectActivityDetail> spnewActivityList = new List<SPProjectActivityDetail>();
                // //get recurring activity details
                // foreach (var item in spActivityList)
                // {
                //     var dates = new List<DateTime>();

                //     if (item.Recurring == true)
                //     {
                //         // calculation for month
                //         int dayInMonth = DateTime.DaysInMonth(item.PlannedStartDate.Year, item.PlannedStartDate.Month);

                //         // calculation for year
                //         int daysInYear = 0;

                //         // calculation for quarter (3 months)
                //         int daysInMonth = 0;
                //         int numberOfDaysInQuater = 0;
                //         int month = item.PlannedStartDate.Month;
                //         int year = item.PlannedStartDate.Year;


                //         if (item.RecurrinTypeId == (int)TimeInterval.Daily)//daily
                //         {

                //             for (var dt = item.PlannedStartDate.Date; dt <= item.PlannedEndDate.Date; dt = dt.AddDays(1))
                //             {
                //                 dates.Add(dt);
                //             }
                //         }
                //         else if (item.RecurrinTypeId == (int)TimeInterval.Weekly)// weekly
                //         {

                //             for (var dt = item.PlannedStartDate.Date; dt <= item.PlannedEndDate.Date; dt = dt.AddDays(7))
                //             {
                //                 dates.Add(dt);
                //             }
                //         }
                //         else if (item.RecurrinTypeId == (int)TimeInterval.Monthly)// monthly
                //         {

                //             for (var dt = item.PlannedStartDate.Date; dt <= item.PlannedEndDate.Date; dt = dt.AddDays(dayInMonth))
                //             {
                //                 dayInMonth = DateTime.DaysInMonth(dt.Year, dt.Month);
                //                 dates.Add(dt);

                //             }

                //         }
                //         else if (item.RecurrinTypeId == (int)TimeInterval.Yearly) //yearly
                //         {

                //             for (var dt = item.PlannedStartDate.Date; dt <= item.PlannedEndDate.Date; dt = dt.AddDays(daysInYear))
                //             {
                //                 // Year + 1 to add correct days in next upcoming year 
                //                 daysInYear = DateTime.IsLeapYear(dt.Year + 1) ? 366 : 365;
                //                 dates.Add(dt);

                //             }

                //         }
                //         else if (item.RecurrinTypeId == (int)TimeInterval.Quarterly) // quarterly
                //         {

                //             for (var dt = item.PlannedStartDate.Date; dt <= item.PlannedEndDate.Date; dt = dt.AddDays(numberOfDaysInQuater))
                //             {
                //                 numberOfDaysInQuater = 0;
                //                 for (int i = 0; i < (3); i++)
                //                 {
                //                     daysInMonth = DateTime.DaysInMonth(year, month);
                //                     numberOfDaysInQuater = numberOfDaysInQuater + daysInMonth;
                //                     if (month >= 12)
                //                     {
                //                         year = year + 1;
                //                         month = 0;
                //                         //   numberOfDaysInQuater = 0 + daysInMonth;

                //                     }
                //                     month = month + 1;
                //                 }

                //                 dates.Add(dt);

                //             }


                //         }


                //     }

                //     foreach (var date in dates)
                //     {
                //         var activityList1 = new SPProjectActivityDetail
                //         {
                //             ActivityId = item.ActivityId,
                //             ActivityName = item.ActivityName,
                //             ActivityDescription = item.ActivityDescription,
                //             PlannedStartDate = date.Date,
                //             PlannedEndDate = item.PlannedEndDate,
                //             BudgetLineId = item.BudgetLineId,
                //             BudgetName = item.BudgetName,
                //             EmployeeID = item.EmployeeID,
                //             EmployeeName = item.EmployeeName,
                //             StatusId = item.StatusId,
                //             StatusName = item.StatusName,
                //             Recurring = item.Recurring,
                //             RecurringCount = item.RecurringCount,
                //             RecurrinTypeId = item.RecurrinTypeId,
                //             CountryId = item.CountryId,
                //             Progress = Math.Round(item.Progress, 2),
                //             Sleepage = item.Sleepage
                //         };
                //         if (item.PlannedStartDate.Date != date.Date)
                //         {
                //             spnewActivityList.Add(activityList1);

                //         }
                //     }

                // }

                // // Merge two list record into 
                // spActivityList.AddRange(spnewActivityList);



                // var activityList = spActivityList.Select(x => new ProjectActivityModel
                // {
                //     ActivityId = x.ActivityId,
                //     ActivityName = x.ActivityName,
                //     ActivityDescription = x.ActivityDescription,
                //     PlannedStartDate = x.PlannedStartDate,
                //     PlannedEndDate = x.PlannedEndDate,
                //     BudgetLineId = x.BudgetLineId,
                //     BudgetName = x.BudgetName,
                //     EmployeeID = x.EmployeeID,
                //     EmployeeName = x.EmployeeName,
                //     StatusId = x.StatusId,
                //     StatusName = x.StatusName,
                //     Recurring = x.Recurring,
                //     RecurringCount = x.RecurringCount,
                //     RecurrinTypeId = x.RecurrinTypeId,
                //     CountryId = x.CountryId,
                //     Progress = Math.Round(x.Progress, 2),
                //     Slippage = x.Sleepage
                // }).OrderBy(y => y.PlannedStartDate)
                // .ToList();

                // response.data.ProjectActivityList = activityList;
                // response.data.TotalCount = activityList.Count();
                // response.StatusCode = StaticResource.successStatusCode;
                // response.Message = "Success";
           
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
                                      .WithSqlParam("country_id", request.CountryId)

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
                    CountryId = x.CountryId,
                    Progress = Math.Round(x.Progress, 2),
                    Slippage = x.Sleepage
                }).OrderBy(y => y.PlannedStartDate)
                .ToList();

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

        public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }
    }



}
