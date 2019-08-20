using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeAttendanceQueryHandler : IRequestHandler<GetEmployeeAttendanceQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly  IMapper _mapper;

        public GetEmployeeAttendanceQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<ApiResponse> Handle(GetEmployeeAttendanceQuery request, CancellationToken cancellationToken)
        {
             ApiResponse response = new ApiResponse();

            try
            {
                var officedetails = await _dbContext.EmployeeProfessionalDetail.FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId);

                int officeid = (int)officedetails.OfficeId;

                var financialyear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDefault == true);
                
                var holidayList = await _dbContext.HolidayDetails
                                             .Where(x => x.IsDeleted == false && x.OfficeId == officeid &&
                                                    x.Date.Year == request.Year && x.Date.Month == request.Month)
                                             .OrderBy(x => x.Date)
                                             .ToListAsync();

                var queryResult = await _dbContext.EmployeeAttendance.Where(x => x.EmployeeId == request.EmployeeId && x.Date.Year == request.Year && x.Date.Month == request.Month).ToListAsync();

                foreach (var item in queryResult)
                {
                    var attendanceFound = await _dbContext.HolidayDetails.FirstOrDefaultAsync(x => x.Date.Date.Month == item.Date.Date.Month && x.Date.Date.Year == item.Date.Date.Year && x.Date.Date.Day == item.Date.Date.Day);
                    
                    if (attendanceFound != null)
                    {
                        TimeSpan? timeDifference;
                        timeDifference = item.OutTime - item.InTime;
                        item.HoverTimeHours = timeDifference.Value.Hours;
                    }
                }

                var attendancelist = queryResult.Select(x => new DisplayEmployeeAttendanceModel
                {
                    attendanceId = x.AttendanceId,

                    employeeID = x.EmployeeId,
                    OverTimeHours = x.HoverTimeHours,
                    text = x.AttendanceTypeId == (int)AttendanceType.P ? "P" : x.AttendanceTypeId == (int)AttendanceType.A ? "A" : x.AttendanceTypeId == (int)AttendanceType.L ? "L" : "",
                    startDate = x.AttendanceTypeId == (int)AttendanceType.P ? x.InTime?.ToString() : x.InTime.Value.ToString("MM/dd/yyyy h:mm tt"),
                    endDate = x.AttendanceTypeId == (int)AttendanceType.P ? x.OutTime.ToString() : x.OutTime?.ToString("MM/dd/yyyy h:mm tt")
                }).ToList();

                response.data.DisEmployeeAttendanceList = attendancelist;


                foreach (var hlist in holidayList)
                {
                    DisplayEmployeeAttendanceModel obj = new DisplayEmployeeAttendanceModel();
                    obj.attendanceId = 0;
                    obj.employeeID = request.EmployeeId;
                    obj.OverTimeHours = 0;
                    obj.text = "H";
                    obj.startDate = hlist.Date.ToString("MM/dd/yyyy h:mm tt");
                    obj.endDate = hlist.Date.ToString("MM/dd/yyyy h:mm tt");
                    response.data.DisEmployeeAttendanceList.Add(obj);
                }

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