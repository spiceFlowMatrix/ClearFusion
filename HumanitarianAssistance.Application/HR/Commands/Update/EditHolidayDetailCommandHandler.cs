using System;
using System.Collections.Generic;
using System.Linq;
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

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditHolidayDetailCommandHandler: IRequestHandler<EditHolidayDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public EditHolidayDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(EditHolidayDetailCommand request, CancellationToken cancellationToken)
        {
             ApiResponse response = new ApiResponse();
            try
            {
                var financialyear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDefault == true);
                
                if (request.HolidayType == (int)HolidayType.REPEATWEEKLYDAY)
                {
                    var existrecord = await _dbContext.HolidayWeeklyDetails.Where(x => x.IsDeleted == false && x.OfficeId == request.OfficeId && x.FinancialYearId == financialyear.FinancialYearId).ToListAsync();
                    _dbContext.RemoveRange(existrecord);
                    await _dbContext.SaveChangesAsync();

                    List<HolidayWeeklyDetails> holidayweeklylist = new List<HolidayWeeklyDetails>();

                    foreach (var hweeklylist in request.RepeatWeeklyDay)
                    {
                        HolidayWeeklyDetails list = new HolidayWeeklyDetails();
                        list.Day = hweeklylist.Day;
                        list.OfficeId = (int)request.OfficeId;
                        list.FinancialYearId = financialyear.FinancialYearId;
                        list.ModifiedById = request.ModifiedById;
                        list.ModifiedDate = request.ModifiedDate;
                        list.IsDeleted = false;
                        holidayweeklylist.Add(list);
                    }
                    await _dbContext.HolidayWeeklyDetails.AddRangeAsync(holidayweeklylist);
                    await _dbContext.SaveChangesAsync();

                    List<HolidayDetails> holidaylist = new List<HolidayDetails>();

                    var hlist = await _dbContext.HolidayDetails.Where(x => x.IsDeleted == false && x.FinancialYearId == financialyear.FinancialYearId && x.OfficeId == request.OfficeId && x.HolidayType == (int)HolidayType.REPEATWEEKLYDAY).ToListAsync();
                    
                    foreach (var h in hlist)
                    {
                        h.IsDeleted = true;
                        h.ModifiedById = request.ModifiedById;
                        h.ModifiedDate = request.ModifiedDate;
                        holidaylist.Add(h);
                    }
                    //_uow.GetDbContext().HolidayDetails.UpdateRange(holidaylist);
                    _dbContext.RemoveRange(holidaylist);
                    await _dbContext.SaveChangesAsync();

                    List<HolidayDetails> holidaylist1 = new List<HolidayDetails>();
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
                                holidaylist1.Add(holiday);
                            }
                        }
                        todaydate = todaydate.AddDays(1);
                    }
                    await _dbContext.HolidayDetails.AddRangeAsync(holidaylist1);
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    var existrecord = await _dbContext.HolidayDetails.FirstOrDefaultAsync(x => x.HolidayId == request.HolidayId);
                    
                    if (existrecord != null)
                    {
                        existrecord.HolidayName = request.HolidayName;
                        existrecord.Remarks = request.Remarks;
                        existrecord.ModifiedById = request.ModifiedById;
                        existrecord.ModifiedDate = request.ModifiedDate;
                        existrecord.IsDeleted = false;
                        _dbContext.HolidayDetails.Update(existrecord);
                        await _dbContext.SaveChangesAsync();
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
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