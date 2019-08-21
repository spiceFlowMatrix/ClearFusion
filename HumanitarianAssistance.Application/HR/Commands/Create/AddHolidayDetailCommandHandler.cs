using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddHolidayDetailCommandHandler: BaseModel, IRequest<ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddHolidayDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper= mapper;
        }

        public async Task<ApiResponse> Handle(AddHolidayDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var financialyear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDefault == true);

                if (request.HolidayType == (int)HolidayType.REPEATWEEKLYDAY)
                {
                    List<HolidayWeeklyDetails> holidayweeklylist = new List<HolidayWeeklyDetails>();
                    foreach (var hlist in request.RepeatWeeklyDay)
                    {
                        HolidayWeeklyDetails list = new HolidayWeeklyDetails();
                        list.Day = hlist.Day;
                        list.OfficeId = (int)request.OfficeId;
                        list.FinancialYearId = financialyear.FinancialYearId;
                        list.CreatedById = request.CreatedById;
                        list.CreatedDate = request.CreatedDate;
                        list.IsDeleted = false;
                        holidayweeklylist.Add(list);
                    }
                    await _dbContext.HolidayWeeklyDetails.AddRangeAsync(holidayweeklylist);
                    await _dbContext.SaveChangesAsync();

                    List<HolidayDetails> holidaylist = new List<HolidayDetails>();
                    
                    for (DateTime todaydate = financialyear.StartDate; todaydate <= financialyear.EndDate;)
                    {
                        HolidayDetails holiday = new HolidayDetails();
                        string day = todaydate.DayOfWeek.ToString();
                        foreach (var list in request.RepeatWeeklyDay)
                        {
                            if (list.Day == day)
                            {
                                holiday.HolidayName = "Weekly Off";
                                holiday.Date = todaydate;
                                holiday.FinancialYearId = financialyear.FinancialYearId;
                                holiday.OfficeId = request.OfficeId;
                                holiday.HolidayType = request.HolidayType;
                                holiday.CreatedById = request.CreatedById;
                                holiday.CreatedDate = request.CreatedDate;
                                holidaylist.Add(holiday);
                            }
                        }
                        todaydate = todaydate.AddDays(1);
                    }
                    await _dbContext.HolidayDetails.AddRangeAsync(holidaylist);
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    var existrecord = await _dbContext.HolidayDetails.FirstOrDefaultAsync(x => x.Date.Date == request.Date.Date);

                    if (existrecord == null)
                    {
                        request.FinancialYearId = financialyear.FinancialYearId;
                        request.HolidayType = request.HolidayType;
                        HolidayDetails obj = _mapper.Map<HolidayDetails>(request);
                        obj.IsDeleted = false;
                        await _dbContext.HolidayDetails.AddAsync(obj);
                        await _dbContext.SaveChangesAsync();
                        
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                        response.Message = "Holiday Details already exist for this date.";
                    }
                }
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