using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetPayrollDailyHourByEmployeeIdsQueryHandler : IRequestHandler<GetPayrollDailyHourByEmployeeIdsQuery, object>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetPayrollDailyHourByEmployeeIdsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetPayrollDailyHourByEmployeeIdsQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> result= new Dictionary<string, object>();
            try
            {
                var empDetails = await _dbContext.EmployeeDetail.Include(x=>x.EmployeeProfessionalDetail)
                                .ThenInclude(x=>x.AttendanceGroupMaster)
                                .Where(x=>x.IsDeleted == false && request.EmpIds.Contains(x.EmployeeID) && x.EmployeeProfessionalDetail.AttendanceGroupId != null)
                                .ToListAsync();

                if(empDetails.Count() != request.EmpIds.Length) 
                {
                    throw new Exception("Some Employees don't have Attendance Group assigned to them!");
                }

                var payrollDetail = await _dbContext.PayrollMonthlyHourDetail.Where(x => x.OfficeId == request.OfficeId && x.PayrollYear == request.Date.Year && x.PayrollMonth == request.Date.Month && x.IsDeleted == false)
                                                        .ToListAsync();

                if (payrollDetail.Count() == 0)
                {
                    throw new Exception(String.Format(StaticResource.PayrollDailyHoursNotSaved, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(request.Date.Month),
                                                                                                await _dbContext.OfficeDetail.Where(x=>x.IsDeleted==false && x.OfficeId == request.OfficeId).Select(x=>x.OfficeName).FirstOrDefaultAsync()
                                                                                              ));
                }

                var AttendanceGroupWithNoInOutTimeId = empDetails.Select(x => (x.EmployeeProfessionalDetail.AttendanceGroupId)).Except(payrollDetail.Select(x=>x.AttendanceGroupId)).ToList();
                if(AttendanceGroupWithNoInOutTimeId.Any()) 
                {
                    var AttendanceGroupWithNoInOutTimeName = empDetails.Where(x=>AttendanceGroupWithNoInOutTimeId.Contains(x.EmployeeProfessionalDetail.AttendanceGroupId)).Select(x=>x.EmployeeProfessionalDetail.AttendanceGroupMaster.Name).ToList();
                    throw new Exception(String.Format(StaticResource.PayrollDailyHoursNotSetForAttendanceGroup, 
                    await _dbContext.OfficeDetail.Where(x=>x.IsDeleted==false && x.OfficeId == request.OfficeId).Select(x=>x.OfficeName).FirstOrDefaultAsync(),
                    CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(request.Date.Month),
                    (request.Date.Year),
                    String.Join(",", AttendanceGroupWithNoInOutTimeName)
                    ));
                }

                var employeeRecord = empDetails.Select(x=> new ExistingAttendanceDetailModel{
                    EmployeeID = x.EmployeeID,
                    AttendanceGroupId = x.EmployeeProfessionalDetail.AttendanceGroupId,
                    AttendanceGroupName = x.EmployeeProfessionalDetail.AttendanceGroupMaster.Name,
                    InTime = payrollDetail.Where(y=>y.AttendanceGroupId == x.EmployeeProfessionalDetail.AttendanceGroupId).Select(y=> y.InTime).FirstOrDefault(),
                    OutTime = payrollDetail.Where(y=>y.AttendanceGroupId == x.EmployeeProfessionalDetail.AttendanceGroupId).Select(y=> y.OutTime).FirstOrDefault(),
                    AttendanceType = (int)AttendanceType.P
                }).ToList();

                var existingAttendanceDetail = await _dbContext.EmployeeAttendance.Where(x=>x.IsDeleted == false && employeeRecord.Select(y=>y.EmployeeID).Contains(x.EmployeeId) && x.Date.Date == request.Date.Date).ToListAsync();
                foreach(var attendance in existingAttendanceDetail)
                {
                    ExistingAttendanceDetailModel emp = employeeRecord.FirstOrDefault(x=>x.EmployeeID == attendance.EmployeeId);
                    if(emp != null)
                    {
                        emp.InTime = attendance.InTime;
                        emp.OutTime = attendance.OutTime;
                        emp.AttendanceType = attendance.AttendanceTypeId;
                    }
                }
                result.Add("EmployeeAttendanceGroupDetail", employeeRecord);
            }
            catch(Exception ex)
            { 
                throw ex;
            }
            return result;
        }
    }
}