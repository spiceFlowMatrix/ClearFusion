using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetFilteredAttendanceQueryHandler : IRequestHandler<GetFilteredAttendanceQuery, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetFilteredAttendanceQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        async Task<object> IRequestHandler<GetFilteredAttendanceQuery, object>.Handle(GetFilteredAttendanceQuery request, CancellationToken cancellationToken)
        {
            FilteredAttendanceDetailModel attendanceModel = new FilteredAttendanceDetailModel();
            attendanceModel.attendanceList = new List<AttendanceListModel>();
            try
            {
                EmployeeProfessionalDetail officedetails = await _dbContext.EmployeeProfessionalDetail
                                                                    .Include(x => x.OfficeDetail)
                                                                    .FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId && x.IsDeleted == false);

                int officeId;
                if (officedetails != null)
                {
                    officeId = (int)officedetails.OfficeId;
                }
                else
                {
                    throw new Exception("Office Not Found");
                }
                PayrollMonthlyHourDetail payrollDetail = await _dbContext.PayrollMonthlyHourDetail.FirstOrDefaultAsync(x => x.OfficeId == officeId && x.PayrollYear == request.Year && x.PayrollMonth == request.Month && x.IsDeleted == false);

                if (payrollDetail == null)
                {
                    throw new Exception(String.Format(StaticResource.PayrollDailyHoursNotSaved, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(request.Month),
                                                                                                    officedetails.OfficeDetail.OfficeName
                                                                                              ));
                }
                List<EmployeeAttendance> queryResult = await _dbContext.EmployeeAttendance.Where(x => x.EmployeeId == request.EmployeeId &&
                                                                                                      x.Date.Year == request.Year &&
                                                                                                      x.Date.Month == request.Month && x.IsDeleted == false)
                                                                                                    .ToListAsync();

                foreach(var attendance in queryResult) {
                    AttendanceListModel model = new AttendanceListModel()
                    {
                        Date = attendance.Date.ToShortDateString(),
                        InTime = attendance.InTime,
                        OutTime = attendance.OutTime,
                        Attended = (attendance.AttendanceTypeId == 1) ? "Yes" : "No"
                    };
                    attendanceModel.attendanceList.Add(model);
                }
                // int monthDays = DateTime.DaysInMonth(request.Year, request.Month);
                // DateTime date = new DateTime();
                // for (int i = 1; i <= monthDays; i++)
                // {
                //     date = new DateTime(request.Year, request.Month, i);
                //     AttendanceListModel model = new AttendanceListModel()
                //     {
                //         Date = date.ToShortDateString(),
                //         InTime = (queryResult.Select(x => x.Date.Day).Contains(date.Day)) ? (queryResult.Where(x => x.Date.Day == date.Day).Select(x=>x.InTime).FirstOrDefault()) : payrollDetail.InTime,
                //         OutTime = (queryResult.Select(x => x.Date.Day).Contains(date.Day)) ? (queryResult.Where(x => x.Date.Day == date.Day).Select(x=>x.OutTime).FirstOrDefault()) : payrollDetail.OutTime,
                //         Attended = queryResult.Select(x => x.Date.Day).Contains(date.Day) ? "Yes" : "No"
                //     };
                //     attendanceModel.attendanceList.Add(model);
                // }

                attendanceModel.TotalCount = queryResult.Count();
                attendanceModel.attendanceList = attendanceModel.attendanceList.Skip(request.PageSize.Value * request.PageIndex.Value).Take(request.PageSize.Value).ToList();


            }
            catch (Exception ex)
            {
                throw ex;

            }
            return attendanceModel;
        }
    }
}