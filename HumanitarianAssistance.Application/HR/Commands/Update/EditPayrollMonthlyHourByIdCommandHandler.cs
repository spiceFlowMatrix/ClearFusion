using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditPayrollMonthlyHourByIdCommandHandler : IRequestHandler<EditPayrollMonthlyHourByIdCommand, bool>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public EditPayrollMonthlyHourByIdCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(EditPayrollMonthlyHourByIdCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                var financialYear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.IsDefault == true);
                if(financialYear == null)
                {
                    throw new Exception(StaticResource.FinancialYearNotFound);
                }
                List<string> weeklyOff = _dbContext.HolidayWeeklyDetails.Where(x=> x.IsDeleted == false &&
                                                    x.FinancialYearId ==financialYear.FinancialYearId)
                                                    .Select(x=> x.Day).ToList();
                int weeklyOffDays = ParticularDayInMonth(new DateTime(financialYear.StartDate.Year, request.Date.Month, 1), weeklyOff);
                int monthDays = DateTime.DaysInMonth(request.Date.Year, request.Date.Month);

                var payrollmonthlyinfo = await _dbContext.PayrollMonthlyHourDetail
                                                         .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                     x.PayrollMonthlyHourID == request.PayrollMonthlyHourId
                                                                    );
                if (payrollmonthlyinfo != null)
                {
                    TimeSpan hours;

                    hours = Convert.ToDateTime(request.OutTime) - Convert.ToDateTime(request.InTime);
                    request.Hours = Convert.ToInt32(hours.ToString().Substring(0, 2));

                    payrollmonthlyinfo.OfficeId = request.OfficeId;
                    payrollmonthlyinfo.PayrollMonth = request.Date.Month;
                    payrollmonthlyinfo.PayrollYear = request.Date.Year;
                    payrollmonthlyinfo.Hours = Convert.ToInt32(hours.ToString().Substring(0, 2));
                    payrollmonthlyinfo.WorkingTime = ((monthDays-weeklyOffDays) * request.Hours);
                    payrollmonthlyinfo.InTime = request.InTime;
                    payrollmonthlyinfo.OutTime = request.OutTime;
                    payrollmonthlyinfo.ModifiedById = request.ModifiedById;
                    payrollmonthlyinfo.ModifiedDate = request.ModifiedDate;
                    payrollmonthlyinfo.IsDeleted = false;
                    payrollmonthlyinfo.AttendanceGroupId = request.AttendanceGroupId;

                    await _dbContext.SaveChangesAsync();

                    success = true;
                }
                else
                {
                    throw new Exception("Record not found to update");
                }
            }
            catch (Exception ex)
            {
                throw ex;
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