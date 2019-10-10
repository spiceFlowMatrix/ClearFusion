using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditProjectActivityDetailCommandHandler : IRequestHandler<EditProjectActivityDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditProjectActivityDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        List<ProjectActivityProvinceDetail> activityProvienceList = new List<ProjectActivityProvinceDetail>();

        public async Task<ApiResponse> Handle(EditProjectActivityDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<ProjectActivityDetail> activityDetail = new List<ProjectActivityDetail>();
                // Case 1:  update  all recurred record with recurrence
                var projectactivityList = await _dbContext.ProjectActivityDetail.Where(x => x.ReoccuredReferenceId == request.ReoccuredReferenceId &&
                                                                                            x.ReoccuredReferenceId != null &&
                                                                                            x.IsDeleted == false).ToListAsync();


                List<ProjectActivityDetail> activityList = new List<ProjectActivityDetail>();
                // Update all the entries of activity list  having same Guid  id 
                if (projectactivityList.Any())
                {

                    foreach (var element in projectactivityList)
                    {
                        element.ActivityDescription = request.ActivityDescription;
                        // element.PlannedStartDate = element.PlannedStartDate;
                        // element.PlannedEndDate = element.PlannedEndDate;
                        element.BudgetLineId = request.BudgetLineId;
                        element.EmployeeID = request.EmployeeID;
                        element.Recurring = element.Recurring;
                        element.RecurrinTypeId = element.RecurrinTypeId;
                        element.RecurringCount = element.RecurringCount;
                        element.ActualStartDate = request.ActualStartDate;
                        element.ActualEndDate = request.ActualEndDate;
                        element.IsDeleted = false;
                        element.CreatedById = request.CreatedById;
                        element.ProjectId = request.ProjectId;
                        element.ReoccuredReferenceId = request.ReoccuredReferenceId;
                        element.CountryId = request.CountryId;
                        element.StatusId = request.StatusId;
                        element.ModifiedById = request.ModifiedById;
                        element.ModifiedDate = DateTime.UtcNow;
                        activityList.Add(element);

                    }

                    _dbContext.ProjectActivityDetail.UpdateRange(activityList);
                    await _dbContext.SaveChangesAsync();
                    foreach (var itm in projectactivityList)
                    {
                        // note to edit province and district details ***
                        request.ActivityId = itm.ActivityId;
                        editActivityProvinceDistrict(request);

                    }
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";

                }


                // Case 2: if update without recurrence and add new recurrence
                else
                {

                    var projectactivityDetail = await _dbContext.ProjectActivityDetail.FirstOrDefaultAsync(x => x.ActivityId == request.ActivityId && x.ReoccuredReferenceId == null && x.IsDeleted == false);
                    if (projectactivityDetail != null)
                    {
                        _mapper.Map(request, projectactivityDetail);
                        //  add new reoccurence activity
                        if (request.Recurring == true)
                        {
                            // when reoccuring count value is 0 then set Planned end date as Planned start date 

                            if (request.RecurringCount == 0)
                            {
                                projectactivityDetail.PlannedEndDate = request.PlannedStartDate;
                            }
                            else
                            {
                                projectactivityDetail.PlannedEndDate = StaticFunctions.GetRecurrenceEndDate(request.RecurringCount, request.RecurrinTypeId, request.PlannedStartDate);

                                Guid ReoccuredReferenceId = Guid.NewGuid();
                                projectactivityDetail.ReoccuredReferenceId = ReoccuredReferenceId;
                                // Note to update end date value 

                                projectactivityDetail.PlannedEndDate = StaticFunctions.GetRecurrenceEndDate(projectactivityDetail.RecurringCount, projectactivityDetail.RecurrinTypeId, projectactivityDetail.PlannedStartDate);

                                #region  // Note calulates number of days to reoccur activity //
                                var dates = new List<DateTime>();

                                // calculation for month
                                int dayInMonth = DateTime.DaysInMonth(projectactivityDetail.PlannedStartDate.Value.Year, projectactivityDetail.PlannedStartDate.Value.Month);

                                // calculation for year
                                int daysInYear = 0;

                                // calculation for quarter (3 months)
                                int daysInMonth = 0;
                                int numberOfDaysInQuater = 0;
                                int month = projectactivityDetail.PlannedStartDate.Value.Month;
                                int year = projectactivityDetail.PlannedStartDate.Value.Year;


                                if (projectactivityDetail.RecurrinTypeId == (int)TimeInterval.Daily)//daily
                                {

                                    for (var dt = projectactivityDetail.PlannedStartDate.Value.Date; dt <= projectactivityDetail.PlannedEndDate.Value.Date; dt = dt.AddDays(1))
                                    {
                                        dates.Add(dt);
                                    }
                                }
                                else if (projectactivityDetail.RecurrinTypeId == (int)TimeInterval.Weekly)// weekly
                                {

                                    for (var dt = projectactivityDetail.PlannedStartDate.Value.Date; dt <= projectactivityDetail.PlannedEndDate.Value.Date; dt = dt.AddDays(7))
                                    {
                                        dates.Add(dt);
                                    }
                                }
                                else if (projectactivityDetail.RecurrinTypeId == (int)TimeInterval.Monthly)// monthly
                                {

                                    for (var dt = projectactivityDetail.PlannedStartDate.Value.Date; dt <= projectactivityDetail.PlannedEndDate.Value.Date; dt = dt.AddDays(dayInMonth))
                                    {
                                        dayInMonth = DateTime.DaysInMonth(dt.Year, dt.Month);
                                        dates.Add(dt);

                                    }

                                }
                                else if (projectactivityDetail.RecurrinTypeId == (int)TimeInterval.Yearly) //yearly
                                {

                                    for (var dt = projectactivityDetail.PlannedStartDate.Value.Date; dt <= projectactivityDetail.PlannedEndDate.Value.Date; dt = dt.AddDays(daysInYear))
                                    {
                                        // Year + 1 to add correct days in next upcoming year 
                                        daysInYear = DateTime.IsLeapYear(dt.Year + 1) ? 366 : 365;
                                        dates.Add(dt);

                                    }

                                }
                                else if (projectactivityDetail.RecurrinTypeId == (int)TimeInterval.Quarterly) // quarterly
                                {

                                    for (var dt = projectactivityDetail.PlannedStartDate.Value.Date; dt <= projectactivityDetail.PlannedEndDate.Value.Date; dt = dt.AddDays(numberOfDaysInQuater))
                                    {
                                        numberOfDaysInQuater = 0;
                                        for (int i = 0; i < (3); i++)
                                        {
                                            daysInMonth = DateTime.DaysInMonth(year, month);
                                            numberOfDaysInQuater = numberOfDaysInQuater + daysInMonth;
                                            if (month >= 12)
                                            {
                                                year = year + 1;
                                                month = 0;
                                                //   numberOfDaysInQuater = 0 + daysInMonth;

                                            }
                                            month = month + 1;
                                        }

                                        dates.Add(dt);

                                    }


                                }

                                #endregion

                                // Note Add all reocuured actiivty with updated plannedStart date
                                foreach (var plannedStartDate in dates)
                                {

                                    // note add new reoccurence of existing activity
                                    if (projectactivityDetail.PlannedStartDate.Value.Date != plannedStartDate.Date)
                                    {
                                        // projectactivityDetail.PlannedStartDate  = plannedStartDate;
                                        AddProjectActivity(projectactivityDetail, ref activityDetail, plannedStartDate);
                                    }

                                    else
                                    {
                                        //  note , if existing parent activity have changes in province and district then update  
                                        request.ActivityId = projectactivityDetail.ActivityId;
                                        projectactivityDetail.ModifiedById = request.ModifiedById;
                                        projectactivityDetail.ModifiedDate = DateTime.UtcNow;
                                        editActivityProvinceDistrict(request);
                                        await _dbContext.SaveChangesAsync();
                                    }
                                }

                                await _dbContext.ProjectActivityDetail.AddRangeAsync(activityDetail);
                                await _dbContext.SaveChangesAsync();
                                // add country , province and district for each new reoccured activity
                                foreach (var activity in activityDetail)
                                {
                                    request.ActivityId = activity.ActivityId;
                                    editActivityProvinceDistrict(request);
                                    await _dbContext.ProjectActivityProvinceDetail.AddRangeAsync(activityProvienceList);
                                    await _dbContext.SaveChangesAsync();
                                }

                            }
                        }
                        else
                        {
                            // case :3 edit  activity without reocurrence
                            projectactivityDetail.ModifiedDate = request.ModifiedDate;
                            projectactivityDetail.ModifiedById = request.ModifiedById;
                            projectactivityDetail.IsDeleted = false;

                            await _dbContext.SaveChangesAsync();
                            // note To add country province and district for activity without reocurrence with  new reocuurence
                            request.ActivityId = projectactivityDetail.ActivityId;
                            editActivityProvinceDistrict(request);

                        }

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.ActivityNotFound;
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

        public void editActivityProvinceDistrict(EditProjectActivityDetailCommand request)
        {
            if (request.CountryId != null)
            {
                var projectActivityProvinceDetailExist = _dbContext.ProjectActivityProvinceDetail
                                                                            .Where(x => x.ActivityId == request.ActivityId &&
                                                                                        x.IsDeleted == false)
                                                                            .ToList();

                if (projectActivityProvinceDetailExist.Any())
                {
                    _dbContext.ProjectActivityProvinceDetail.RemoveRange(projectActivityProvinceDetailExist);
                    _dbContext.SaveChanges();
                }



                var districts = _dbContext.DistrictDetail.Where(x => x.IsDeleted == false &&
                                                                          request.ProvinceId.Contains(x.ProvinceID.Value))
                                                                .ToList();

                var selectedDistrict = districts.Where(x => request.DistrictID.Contains(x.DistrictID))
                                                                 .Select(x => new ProjectActivityProvinceDetail
                                                                 {
                                                                     DistrictID = x.DistrictID,
                                                                     ProvinceId = x.ProvinceID.Value
                                                                 }).ToList();
                var provincesWithNoDistrict = request.ProvinceId.Where(x => !selectedDistrict.Select(y => y.ProvinceId).Contains(x)).ToList();

                foreach (var item in provincesWithNoDistrict)
                {
                    ProjectActivityProvinceDetail projectActivityProvince = new ProjectActivityProvinceDetail();
                    projectActivityProvince.ProvinceId = item;
                    selectedDistrict.Add(projectActivityProvince);
                }

                foreach (var item in selectedDistrict)
                {
                    item.ActivityId = request.ActivityId;
                    item.ModifiedById = request.ModifiedById;
                    item.ModifiedDate = request.ModifiedDate;
                    item.IsDeleted = false;
                }
                _dbContext.ProjectActivityProvinceDetail.UpdateRange(selectedDistrict);
                _dbContext.SaveChanges();

            }
        }

        public void AddProjectActivity(ProjectActivityDetail obj, ref List<ProjectActivityDetail> activityDetail, DateTime plannedStartDate)
        {
            var activityModel = new ProjectActivityDetail
            {

                ActivityDescription = obj.ActivityDescription,
                PlannedStartDate = plannedStartDate,
                PlannedEndDate = obj.PlannedEndDate,
                BudgetLineId = obj.BudgetLineId,
                EmployeeID = obj.EmployeeID,
                Recurring = obj.Recurring,
                RecurrinTypeId = obj.RecurrinTypeId,
                RecurringCount = obj.RecurringCount,
                ActualStartDate = obj.ActualStartDate,
                ActualEndDate = obj.ActualEndDate,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
                CreatedById = obj.CreatedById,
                ProjectId = obj.ProjectId,
                ReoccuredReferenceId = obj.ReoccuredReferenceId,
                CountryId = obj.CountryId,
                StatusId= obj.StatusId,
            };

            activityDetail.Add(activityModel);
        }

    }
}
