using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeAppliedLeaveHoursQueryHandler : IRequestHandler<GetEmployeeAppliedLeaveHoursQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetEmployeeAppliedLeaveHoursQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetEmployeeAppliedLeaveHoursQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {
                FinancialYearDetail financialYear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.IsDefault == true);

                if (financialYear == null)
                {
                    throw new Exception(StaticResource.FinancialYearNotFound);
                }

                EmployeeDetail employee = await _dbContext.EmployeeDetail
                                                          .Include(x => x.EmployeeProfessionalDetail)
                                                          .ThenInclude(x => x.AttendanceGroupMaster)
                                                          .Include(x => x.EmployeeProfessionalDetail)
                                                          .ThenInclude(x => x.OfficeDetail)
                                                          .FirstOrDefaultAsync(x => x.IsDeleted == false && x.EmployeeID == request.EmployeeId);

                PayrollMonthlyHourDetail officeHours = await _dbContext.PayrollMonthlyHourDetail
                                                                 .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                 x.PayrollYear == request.StartDate.Year && x.PayrollMonth == request.StartDate.Month &&
                                                                 x.AttendanceGroupId == employee.EmployeeProfessionalDetail.AttendanceGroupId
                                                                 && x.OfficeId == employee.EmployeeProfessionalDetail.OfficeId);



                if (officeHours == null)
                {
                    throw new Exception(string.Format(StaticResource.PayrollDailyHoursNotSetForAttendanceGroup,
                    employee.EmployeeProfessionalDetail.OfficeDetail.OfficeName, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(request.StartDate.Month), request.StartDate.Year,
                    employee.EmployeeProfessionalDetail.AttendanceGroupMaster.Name
                    ));
                }

                List<HolidayWeeklyDetails> weeklyDetails = await _dbContext.HolidayWeeklyDetails.Where(x => x.IsDeleted == false).ToListAsync();

                List<HolidayDetails> holidays = await _dbContext.HolidayDetails.Where(x => x.IsDeleted == false && x.FinancialYearId == financialYear.FinancialYearId
                                                                && x.OfficeId == employee.EmployeeProfessionalDetail.OfficeId).ToListAsync();

                List<DateTime> leaveDays = new List<DateTime>();
                int days = request.EndDate.Subtract(request.StartDate).Days + 1;

                //If both months of start date and end date is same
                if (request.EndDate.Month == request.StartDate.Month)
                {
                    for (int i = 0; i < days; i++)
                    {
                        DateTime date = new DateTime(financialYear.StartDate.Year, request.StartDate.Month, request.StartDate.Day + i);
                        leaveDays.Add(date);
                    }
                }
                else //If both months of start date and end date are not same
                {
                    int daysInStartDateMonth = DateTime.DaysInMonth(financialYear.StartDate.Year, request.StartDate.Month);
                    DateTime maxDate = new DateTime(financialYear.StartDate.Year, request.StartDate.Month, daysInStartDateMonth);

                    int endDateDay = 0;

                    for (int i = 0; i < days; i++)
                    {
                        DateTime date = new DateTime(financialYear.StartDate.Year, request.StartDate.Month, request.StartDate.Day + i);

                        if(date.Date != maxDate.Date)
                        {
                            leaveDays.Add(date);
                        }
                        else
                        {
                            DateTime endDate = new DateTime(financialYear.StartDate.Year, request.EndDate.Month, endDateDay + 1);
                            leaveDays.Add(date);
                            endDateDay++;
                        }
                    }
                }

                int leaveDaysCount =0;

                foreach (var leaveDay in leaveDays)
                {
                 string dayName= CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(leaveDay.DayOfWeek);

                 // If applied leave day is not a weekday
                 if(!weeklyDetails.Select(x=> x.Day).Contains(dayName))
                 {
                     if(!holidays.Select(x=> x.Date.Date).Contains(leaveDay.Date))
                     {
                         leaveDaysCount++;
                     }
                 }
                    
                }

                int hours = officeHours.OutTime.Value.Subtract(officeHours.InTime.Value).Hours;
                int appliedLeaveHour = leaveDaysCount * hours;

                response.Add("AppliedHours", appliedLeaveHour);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}