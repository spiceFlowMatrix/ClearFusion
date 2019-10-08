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
        List<ProjectActivityDetail> activityDetail = new List<ProjectActivityDetail>();

        public async Task<ApiResponse> Handle(AddProjectActivityDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ProjectActivityDetail obj = _mapper.Map<AddProjectActivityDetailCommand, ProjectActivityDetail>(request);
                // add multiple entries if activity is reocurred
                if (obj.Recurring == true)
                {
                    Guid ReoccuredReferenceId = Guid.NewGuid();
                    // Console.WriteLine("New Guid is " + ReoccuredReferenceId.ToString());
                    obj.ReoccuredReferenceId = ReoccuredReferenceId;
                    // Note to update end date value 

                    obj.PlannedEndDate = StaticFunctions.GetRecurrenceEndDate(obj.RecurringCount, obj.RecurrinTypeId, obj.PlannedStartDate);


                    // calulates number bof days to reoccur activity
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


                    // List<ProjectActivityDetail> activityDetail = new List<ProjectActivityDetail>();
                    foreach (var plannedStartDate in dates)
                    {

                        AddProjectActivity(obj);
                        obj.PlannedStartDate = plannedStartDate;
                        // var activityModel = new ProjectActivityDetail
                        // {

                        //     ActivityDescription = obj.ActivityDescription,
                        //     PlannedStartDate = plannedStartDate,
                        //     PlannedEndDate = obj.PlannedEndDate,
                        //     BudgetLineId = obj.BudgetLineId,
                        //     EmployeeID = obj.EmployeeID,
                        //     Recurring = obj.Recurring,
                        //     RecurrinTypeId = obj.RecurrinTypeId,
                        //     RecurringCount = obj.RecurringCount,
                        //     ActualStartDate = obj.ActualStartDate,
                        //     ActualEndDate = obj.ActualEndDate,
                        //     CreatedDate = DateTime.UtcNow,
                        //     IsDeleted = false,
                        //     CreatedById = request.CreatedById,
                        //     ProjectId = request.ProjectId,
                        //     ReoccuredReferenceId = obj.ReoccuredReferenceId,
                        //     CountryId = obj.CountryId,
                        // };

                        // activityDetail.Add(activityModel);
                        // to add country province and district

                    }

                    await _dbContext.ProjectActivityDetail.AddRangeAsync(obj);
                    await _dbContext.SaveChangesAsync();


                    foreach (var activity in activityDetail)
                    {

                        if (request.CountryId != null && request.ProvinceId != null)
                        {
                          AddActivityCountryProvinceDistrict(request.ProvinceId,request.DistrictID);
                          
                          
                                // List<ProjectActivityProvinceDetail> activityProvienceList = new List<ProjectActivityProvinceDetail>();

                                // var districts = _dbContext.DistrictDetail.Where(x => x.IsDeleted == false && request.ProvinceId.Contains(x.ProvinceID.Value)).ToList();

                                // var selectedDistrict = districts.Where(x => request.DistrictID.Contains(x.DistrictID))
                                //                                                  .Select(x => new ProjectActivityProvinceDetail
                                //                                                  {
                                //                                                      DistrictID = x.DistrictID,
                                //                                                      ProvinceId = x.ProvinceID.Value
                                //                                                  }).ToList();

                                // // var provincesWithNoDistrict= selectedDistrict.Where(x => !model.ProvinceId.Contains(x.ProvinceId));

                                // var provincesWithNoDistrict = request.ProvinceId.Where(x => !selectedDistrict.Select(y => y.ProvinceId).Contains(x)).ToList();

                                // foreach (var item in provincesWithNoDistrict)
                                // {
                                //     ProjectActivityProvinceDetail projectActivityProvinceDetail = new ProjectActivityProvinceDetail();
                                //     projectActivityProvinceDetail.ProvinceId = item;
                                //     selectedDistrict.Add(projectActivityProvinceDetail);
                                // }

                                // foreach (var item in selectedDistrict)
                                // {
                                //     item.CountryId = request.CountryId;
                                //     item.ActivityId = activity.ActivityId;
                                //     item.CreatedById = request.CreatedById;
                                //     item.CreatedDate = request.CreatedDate;
                                //     item.IsDeleted = false;
                                // }
                         
                         
                          //  await _dbContext.ProjectActivityProvinceDetail.AddRangeAsync(selectedDistrict);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                }

                else
                {
                    // add activity without reoccurence
                    AddProjectActivity(obj);

                    await _dbContext.ProjectActivityDetail.AddRangeAsync(obj);
                    await _dbContext.SaveChangesAsync();
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

        public List<ProjectActivityDetail> AddProjectActivity(ProjectActivityDetail obj)
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
            };

            activityDetail.Add(activityModel);

            return activityDetail;
        }

        public List<ProjectActivityProvinceDetail> AddActivityCountryProvinceDistrict(IEnumerable<int> ProvinceId,IEnumerable<long?> DistrictID )
        {
            List<ProjectActivityProvinceDetail> activityProvienceList = new List<ProjectActivityProvinceDetail>();

            var districts = _dbContext.DistrictDetail.Where(x => x.IsDeleted == false && ProvinceId.Contains(x.ProvinceID.Value)).ToList();

            var selectedDistrict = districts.Where(x => DistrictID.Contains(x.DistrictID))
                                                             .Select(x => new ProjectActivityProvinceDetail
                                                             {
                                                                 DistrictID = x.DistrictID,
                                                                 ProvinceId = x.ProvinceID.Value
                                                             }).ToList();

            // var provincesWithNoDistrict= selectedDistrict.Where(x => !model.ProvinceId.Contains(x.ProvinceId));

            var provincesWithNoDistrict = ProvinceId.Where(x => !selectedDistrict.Select(y => y.ProvinceId).Contains(x)).ToList();

            foreach (var item in provincesWithNoDistrict)
            {
                ProjectActivityProvinceDetail projectActivityProvinceDetail = new ProjectActivityProvinceDetail();
                projectActivityProvinceDetail.ProvinceId = item;
                selectedDistrict.Add(projectActivityProvinceDetail);
            }

            foreach (var item in selectedDistrict)
            {
              //  item.CountryId = request.CountryId;
                // item.ActivityId = activity.ActivityId;
                // item.CreatedById = request.CreatedById;
                // item.CreatedDate = request.CreatedDate;
                item.IsDeleted = false;
            }
            return activityProvienceList;
        }
    }


}
