using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddProjectActivityDetailCommandHandler : IRequestHandler<AddProjectActivityDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddProjectActivityDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        List<ProjectActivityProvinceDetail> activityProvienceList = new List<ProjectActivityProvinceDetail>();

        public async Task<ApiResponse> Handle(AddProjectActivityDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<ProjectActivityDetail> activityDetail = new List<ProjectActivityDetail>();

                ProjectActivityDetail obj = _mapper.Map<AddProjectActivityDetailCommand, ProjectActivityDetail>(request);

                // add multiple entries if activity is reocurred
                if (obj.Recurring == true)
                {
                    //Note to add master activity reference in each reocuured activity
                    Guid ReoccuredReferenceId = Guid.NewGuid();
                    obj.ReoccuredReferenceId = ReoccuredReferenceId;
                    // Note to update end date value 

                    obj.PlannedEndDate = StaticFunctions.GetRecurrenceEndDate(obj.RecurringCount, obj.RecurrinTypeId, obj.PlannedStartDate);

                    #region  // Note calulates number of days to reoccur activity //
                    var dates = new List<DateTime>();

                    // calculation for month
                    int dayInMonth = DateTime.DaysInMonth(obj.PlannedStartDate.Value.Year, obj.PlannedStartDate.Value.Month);

                    // calculation for year
                    int daysInYear = 0;

                    // calculation for quarter (3 months)
                    int daysInMonth = 0;
                    int numberOfDaysInQuater = 0;
                    int month = obj.PlannedStartDate.Value.Month;
                    int year = obj.PlannedStartDate.Value.Year;


                    if (obj.RecurrinTypeId == (int)TimeInterval.Daily)//daily
                    {

                        for (var dt = obj.PlannedStartDate.Value.Date; dt <= obj.PlannedEndDate.Value.Date; dt = dt.AddDays(1))
                        {
                            dates.Add(dt);
                        }
                    }
                    else if (obj.RecurrinTypeId == (int)TimeInterval.Weekly)// weekly
                    {

                        for (var dt = obj.PlannedStartDate.Value.Date; dt <= obj.PlannedEndDate.Value.Date; dt = dt.AddDays(7))
                        {
                            dates.Add(dt);
                        }
                    }
                    else if (obj.RecurrinTypeId == (int)TimeInterval.Monthly)// monthly
                    {

                        for (var dt = obj.PlannedStartDate.Value.Date; dt <= obj.PlannedEndDate.Value.Date; dt = dt.AddDays(dayInMonth))
                        {
                            dayInMonth = DateTime.DaysInMonth(dt.Year, dt.Month);
                            dates.Add(dt);

                        }

                    }
                    else if (obj.RecurrinTypeId == (int)TimeInterval.Yearly) //yearly
                    {

                        for (var dt = obj.PlannedStartDate.Value.Date; dt <= obj.PlannedEndDate.Value.Date; dt = dt.AddDays(daysInYear))
                        {
                            // Year + 1 to add correct days in next upcoming year 
                            daysInYear = DateTime.IsLeapYear(dt.Year + 1) ? 366 : 365;
                            dates.Add(dt);

                        }

                    }
                    else if (obj.RecurrinTypeId == (int)TimeInterval.Quarterly) // quarterly
                    {

                        for (var dt = obj.PlannedStartDate.Value.Date; dt <= obj.PlannedEndDate.Value.Date; dt = dt.AddDays(numberOfDaysInQuater))
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
                        obj.PlannedStartDate = plannedStartDate.Date;
                        AddProjectActivity(obj, ref activityDetail);

                    }

                    await _dbContext.ProjectActivityDetail.AddRangeAsync(activityDetail);
                    await _dbContext.SaveChangesAsync();


                    foreach (var activity in activityDetail)
                    {
                        request.ActivityId = activity.ActivityId;
                        AddActivityProvinceDistrict(request);
                        await _dbContext.ProjectActivityProvinceDetail.AddRangeAsync(activityProvienceList);
                        await _dbContext.SaveChangesAsync();
                    }
                }
                // Note add activity without reoccurence
                else
                {
                    AddProjectActivity(obj, ref activityDetail);
                    await _dbContext.ProjectActivityDetail.AddRangeAsync(activityDetail);
                    await _dbContext.SaveChangesAsync();
                    if (activityDetail.Any())
                    {
                        request.ActivityId = activityDetail.FirstOrDefault().ActivityId;
                        // add province and multi district 
                        AddActivityProvinceDistrict(request);
                        await _dbContext.ProjectActivityProvinceDetail.AddRangeAsync(activityProvienceList);
                        await _dbContext.SaveChangesAsync();
                    }


                }
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
        // note:  Common Add Project activty with or without recurrence

        public void AddProjectActivity(ProjectActivityDetail obj, ref List<ProjectActivityDetail> activityDetail)
        {
            var activityModel = new ProjectActivityDetail
            {

                ActivityDescription = obj.ActivityDescription,
                PlannedStartDate = obj.PlannedStartDate,
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
                ActivityName = obj.ActivityName,
                StatusId= obj.StatusId,
            };

        activityDetail.Add(activityModel);
        }

    // note:  Common Add Project Province and district with or with recurrence
    public void AddActivityProvinceDistrict(AddProjectActivityDetailCommand request)
    {
        if (request.CountryId != null && request.ProvinceId != null)
        {

            var districts = _dbContext.DistrictDetail.Where(x => x.IsDeleted == false && request.ProvinceId.Contains(x.ProvinceID.Value)).ToList();

            activityProvienceList = districts.Where(x => request.DistrictID.Contains(x.DistrictID))
                                                            .Select(x => new ProjectActivityProvinceDetail
                                                            {
                                                                DistrictID = x.DistrictID,
                                                                ProvinceId = x.ProvinceID.Value
                                                            }).ToList();


            var provincesWithNoDistrict = request.ProvinceId.Where(x => !activityProvienceList.Select(y => y.ProvinceId).Contains(x)).ToList();

            foreach (var item in provincesWithNoDistrict)
            {
                ProjectActivityProvinceDetail projectActivityProvinceDetail = new ProjectActivityProvinceDetail();
                projectActivityProvinceDetail.ProvinceId = item;
                activityProvienceList.Add(projectActivityProvinceDetail);
            }

            foreach (var item in activityProvienceList)
            {
                item.CountryId = request.CountryId;
                item.ActivityId = request.ActivityId;
                item.CreatedById = request.CreatedById;
                item.CreatedDate = request.CreatedDate;
                item.IsDeleted = false;
            }

        }
    }
}


}
