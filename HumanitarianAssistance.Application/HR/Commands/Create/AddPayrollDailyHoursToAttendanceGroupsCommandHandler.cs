using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddPayrollDailyHoursToAttendanceGroupsCommandHandler : IRequestHandler<AddPayrollDailyHoursToAttendanceGroupsCommand, bool>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        public AddPayrollDailyHoursToAttendanceGroupsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddPayrollDailyHoursToAttendanceGroupsCommand request, CancellationToken cancellationToken)
        {
            bool success = false;
            TimeSpan hours;
            hours = Convert.ToDateTime(request.OutTime) - Convert.ToDateTime(request.InTime);
            request.Hours = Convert.ToInt32(hours.ToString().Substring(0, 2));

            try
            {
                var financialYear = _dbContext.FinancialYearDetail.FirstOrDefault(x=> x.IsDeleted == false && x.IsDefault == true);
                if(financialYear == null)
                {
                    throw new Exception(StaticResource.FinancialYearNotFound);
                }
                List<string> weeklyOff = _dbContext.HolidayWeeklyDetails.Where(x=> x.IsDeleted == false &&
                                                    x.FinancialYearId ==financialYear.FinancialYearId)
                                                    .Select(x=> x.Day).ToList();
                int weeklyOffDays = ParticularDayInMonth(new DateTime(financialYear.StartDate.Year, request.Date.Month, 1), weeklyOff);
                int monthDays = DateTime.DaysInMonth(request.Date.Year, request.Date.Month);
                if(request.OfficeId == null) 
                {
                    List<int> officeIds = _dbContext.OfficeDetail.Where(x => x.IsDeleted == false).Select(x => x.OfficeId).ToList();
                    foreach (int officeId in officeIds)
                    {
                        var payrollinfo = await _dbContext.PayrollMonthlyHourDetail
                                                    .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                    x.OfficeId == officeId &&
                                                    x.PayrollMonth == request.Date.Month &&
                                                    x.PayrollYear == request.Date.Year &&
                                                    x.AttendanceGroupId== request.AttendanceGroupId);
                        if (payrollinfo == null)
                        {
                            PayrollMonthlyHourDetail obj = new PayrollMonthlyHourDetail();
                            obj.CreatedById = request.CreatedById;
                            obj.CreatedDate = request.CreatedDate;
                            obj.Hours = request.Hours;
                            obj.WorkingTime = ((monthDays-weeklyOffDays) * request.Hours);
                            obj.InTime = request.InTime;
                            obj.OutTime = request.OutTime;
                            obj.PayrollMonth = request.Date.Month;
                            obj.PayrollYear = request.Date.Year;
                            obj.IsDeleted = false;
                            obj.OfficeId = officeId;
                            obj.AttendanceGroupId = request.AttendanceGroupId;
                            await _dbContext.PayrollMonthlyHourDetail.AddAsync(obj);
                        }
                        else
                        {
                            payrollinfo.ModifiedDate = DateTime.UtcNow;
                            payrollinfo.Hours = request.Hours;
                            payrollinfo.WorkingTime= ((monthDays - weeklyOffDays) * request.Hours);
                            payrollinfo.InTime = request.InTime;
                            payrollinfo.OutTime = request.OutTime;
                            payrollinfo.PayrollMonth = request.Date.Month;
                            payrollinfo.PayrollYear = request.Date.Year;
                            payrollinfo.OfficeId = officeId;
                            payrollinfo.AttendanceGroupId = request.AttendanceGroupId;
                        }
                        await _dbContext.SaveChangesAsync();
                    }
                }
                else 
                {
                    var payrollinfo = await _dbContext.PayrollMonthlyHourDetail
                                                    .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                    x.OfficeId == request.OfficeId.Value &&
                                                    x.PayrollMonth == request.Date.Month &&
                                                    x.PayrollYear == request.Date.Year &&
                                                    x.AttendanceGroupId== request.AttendanceGroupId);
                    if (payrollinfo == null)
                    {
                        PayrollMonthlyHourDetail obj = new PayrollMonthlyHourDetail();
                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = request.CreatedDate;
                        obj.Hours = request.Hours;
                        obj.WorkingTime = ((monthDays-weeklyOffDays) * request.Hours);
                        obj.InTime = request.InTime;
                        obj.OutTime = request.OutTime;
                        obj.PayrollMonth = request.Date.Month;
                        obj.PayrollYear = request.Date.Year;
                        obj.IsDeleted = false;
                        obj.OfficeId = request.OfficeId.Value;
                        obj.AttendanceGroupId = request.AttendanceGroupId;
                        await _dbContext.PayrollMonthlyHourDetail.AddAsync(obj);
                    }
                    else
                    {
                        payrollinfo.ModifiedDate = DateTime.UtcNow;
                        payrollinfo.Hours = request.Hours;
                        payrollinfo.WorkingTime= ((monthDays - weeklyOffDays) * request.Hours);
                        payrollinfo.InTime = request.InTime;
                        payrollinfo.OutTime = request.OutTime;
                        payrollinfo.PayrollMonth = request.Date.Month;
                        payrollinfo.PayrollYear = request.Date.Year;
                        payrollinfo.OfficeId = request.OfficeId.Value;
                        payrollinfo.AttendanceGroupId = request.AttendanceGroupId;
                    }
                    await _dbContext.SaveChangesAsync(); 
                }
                success = true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return success;
        }

        private int ParticularDayInMonth(DateTime time, List<string> dayNames)
        {
            int weekDaysInAMonth =0;
            int daysInAMonth = DateTime.DaysInMonth(time.Year, time.Month);
            for(int i=1; i<= daysInAMonth; i++)
            {
                DateTime date = new DateTime(time.Year, time.Month, i);
                string dayName= CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(date.DayOfWeek);
                if(dayNames.Contains(dayName))
                {
                    weekDaysInAMonth++;
                }
            }
            return weekDaysInAMonth;
        }
    }
}