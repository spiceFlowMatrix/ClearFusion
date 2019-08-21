using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class MonthlyEmployeeAttendanceReportQueryHandler: IRequestHandler<MonthlyEmployeeAttendanceReportQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public MonthlyEmployeeAttendanceReportQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(MonthlyEmployeeAttendanceReportQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                TimeSpan officeintime, officeouttime;
                TimeSpan intime, outtime;
                List<MonthlyEmployeeAttendanceModel> empmonthlyattendancelist = new List<MonthlyEmployeeAttendanceModel>();

                EmployeeProfessionalDetail employeeProfessionalDetail = await _dbContext.EmployeeProfessionalDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.EmployeeId == request.employeeid);

                var payrolltimelist = await _dbContext.PayrollMonthlyHourDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.OfficeId == request.OfficeId && x.PayrollMonth == request.month && x.AttendanceGroupId== employeeProfessionalDetail.AttendanceGroupId);

                if (payrolltimelist == null)
                {
                    throw new Exception("Attendance not found for selected month");
                }

                string time = Convert.ToDateTime(payrolltimelist.InTime).ToString("hh:mm");
                officeintime = Convert.ToDateTime(time).TimeOfDay;

                time = Convert.ToDateTime(payrolltimelist.OutTime).ToString("hh:mm");
                officeouttime = Convert.ToDateTime(time).TimeOfDay;

                var emplist = await _dbContext.EmployeeAttendance.Where(x => x.EmployeeId == request.employeeid && x.Date.Year == request.year && x.Date.Month == request.month).ToListAsync();
                List<EmployeeAttendance> empattencancelist = null;
                int monthdays = DateTime.DaysInMonth(request.year, request.month);

                var holidaylist = await _dbContext.HolidayDetails.Where(x => x.Date.Year == request.year && x.Date.Month == request.month && x.OfficeId == request.OfficeId).ToListAsync();
                
                List<HolidayDetails> holiday = null;

                for (int i = 1; i <= monthdays; i++)
                {
                    empattencancelist = emplist.Where(x => x.Date.Day == i).ToList();
                    MonthlyEmployeeAttendanceModel model = new MonthlyEmployeeAttendanceModel();
                    if (empattencancelist.Count > 0)
                    {
                        model.Date = i + "/" + request.month + "/" + request.year;
                        model.InTime = empattencancelist[0].AttendanceTypeId == (int)AttendanceType.P ? Convert.ToDateTime(empattencancelist[0].InTime).ToString("HH:mm") : "NA";
                        model.OutTime = model.InTime != "NA" ? Convert.ToDateTime(empattencancelist[0].OutTime).ToString("HH:mm") : "NA";
                        model.AttendanceType = empattencancelist[0].AttendanceTypeId == (int)AttendanceType.P ? "P" : empattencancelist[0].AttendanceTypeId == (int)AttendanceType.A ? "A" : empattencancelist[0].AttendanceTypeId == (int)AttendanceType.L ? "L" : empattencancelist[0].AttendanceTypeId == (int)AttendanceType.H ? "H" : "NA";
                        model.Hours = empattencancelist[0].TotalWorkTime + "" + "h";
                        model.OverTimeHours = empattencancelist[0].HoverTimeHours.ToString() + "" + "h";
                        if (empattencancelist[0].AttendanceTypeId == (int)AttendanceType.P)
                        {
                            time = Convert.ToDateTime(empattencancelist[0].InTime).ToString("HH:mm");
                            intime = Convert.ToDateTime(time).TimeOfDay;
                            if (officeintime >= intime)
                            {
                                model.LateArrival = "NA";
                            }
                            else
                            {
                                model.LateArrival = (intime - officeintime).ToString();
                            }

                            time = Convert.ToDateTime(empattencancelist[0].OutTime).ToString("HH:mm");
                            outtime = Convert.ToDateTime(time).TimeOfDay;
                            if (officeouttime >= outtime)
                                model.EarlyOut = (officeouttime - outtime).ToString();
                            else
                                model.EarlyOut = "NA";
                        }
                        else
                        {
                            model.LateArrival = "NA";
                            model.EarlyOut = "NA";
                        }
                    }
                    else
                    {
                        holiday = holidaylist.Where(x => x.Date.Day == i).ToList();
                        if (holiday.Count > 0)
                        {
                            model.Date = i + "/" + request.month + "/" + request.year;
                            model.InTime = "NA";
                            model.OutTime = "NA";
                            model.AttendanceType = holiday[0].HolidayName;
                            model.Hours = "NA";
                            model.OverTimeHours = "NA";
                            model.LateArrival = "NA";
                            model.EarlyOut = "NA";
                        }
                        else
                        {
                            model.Date = i + "/" + request.month + "/" + request.year;
                            model.InTime = "NA";
                            model.OutTime = "NA";
                            model.AttendanceType = "NA";
                            model.Hours = "NA";
                            model.OverTimeHours = "NA";
                            model.LateArrival = "NA";
                            model.EarlyOut = "NA";
                        }
                    }
                    empmonthlyattendancelist.Add(model);
                }
                response.data.MonthlyEmployeeAttendanceList = empmonthlyattendancelist;
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
    }
}