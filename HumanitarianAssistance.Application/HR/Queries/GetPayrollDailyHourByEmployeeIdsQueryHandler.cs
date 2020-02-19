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

                var payrollDetail = await _dbContext.PayrollMonthlyHourDetail.Where(x => request.OfficeIds.Contains(x.OfficeId) && x.PayrollYear == request.FromDate.Year && x.PayrollMonth == request.FromDate.Month && x.IsDeleted == false)
                                                        .GroupBy(x=>x.OfficeId)
                                                        .Select(x=> new {
                                                            OfficeId = x.Key,
                                                            PayrollList = x.ToList()
                                                        }).ToListAsync();

                List<PayrollExceptionModel> exceptionObj = new List<PayrollExceptionModel>();
                var OfficeList = await _dbContext.OfficeDetail.Where(x=>x.IsDeleted==false).ToListAsync();
                foreach (var officePayroll in payrollDetail)
                {
                    var AttendanceGroupWithNoInOutTimeId = empDetails.Where(x=>x.EmployeeProfessionalDetail.OfficeId == officePayroll.OfficeId).Select(x => (x.EmployeeProfessionalDetail.AttendanceGroupId)).Except(officePayroll.PayrollList.Select(x=>x.AttendanceGroupId)).ToList();
                    if(AttendanceGroupWithNoInOutTimeId.Any()) 
                    {
                        var AttendanceGroupWithNoInOutTimeName = empDetails.Where(x=>AttendanceGroupWithNoInOutTimeId.Contains(x.EmployeeProfessionalDetail.AttendanceGroupId) && x.EmployeeProfessionalDetail.OfficeId == officePayroll.OfficeId).Select(x=>x.EmployeeProfessionalDetail.AttendanceGroupMaster.Name).Distinct().ToList();
                        exceptionObj.Add(new PayrollExceptionModel{
                            Office = OfficeList.Where(x=>x.OfficeId == officePayroll.OfficeId).Select(x=>x.OfficeName).FirstOrDefault(),
                            AttendanceGroup = String.Join(",", AttendanceGroupWithNoInOutTimeName)
                        });
                        // throw new Exception(String.Format(StaticResource.PayrollDailyHoursNotSetForAttendanceGroup, 
                        // await _dbContext.OfficeDetail.Where(x=>x.IsDeleted==false && x.OfficeId == request.OfficeId).Select(x=>x.OfficeName).FirstOrDefaultAsync(),
                        // CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(request.FromDate.Month),
                        // (request.FromDate.Year),
                        // String.Join(",", AttendanceGroupWithNoInOutTimeName)
                        // ));
                    }
                }
                
                string exceptionMsg = "Payroll Daily Hours not set for";
                foreach (var ex in exceptionObj)
                {            
                    exceptionMsg = exceptionMsg + "(Office: " + ex.Office + ", Attendance Group: " + ex.AttendanceGroup + "), " ;
                }

                if(exceptionObj.Any()) 
                { 
                    throw new Exception(String.Format(exceptionMsg + "for Month {0} Year {1}", 
                    CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(request.FromDate.Month),
                    (request.FromDate.Year)
                    ));
                }

                // if (payrollDetail.Count() == 0)
                // {
                //     throw new Exception(String.Format(StaticResource.PayrollDailyHoursNotSaved, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(request.FromDate.Month),
                //                                                                                 await _dbContext.OfficeDetail.Where(x=>x.IsDeleted==false && x.OfficeId == request.OfficeId).Select(x=>x.OfficeName).FirstOrDefaultAsync()
                //                                                                               ));
                // }

                // var AttendanceGroupWithNoInOutTimeId = empDetails.Select(x => (x.EmployeeProfessionalDetail.AttendanceGroupId)).Except(payrollDetail.Select(x=>x.AttendanceGroupId)).ToList();
                // if(AttendanceGroupWithNoInOutTimeId.Any()) 
                // {
                //     var AttendanceGroupWithNoInOutTimeName = empDetails.Where(x=>AttendanceGroupWithNoInOutTimeId.Contains(x.EmployeeProfessionalDetail.AttendanceGroupId)).Select(x=>x.EmployeeProfessionalDetail.AttendanceGroupMaster.Name).ToList();
                //     throw new Exception(String.Format(StaticResource.PayrollDailyHoursNotSetForAttendanceGroup, 
                //     await _dbContext.OfficeDetail.Where(x=>x.IsDeleted==false && x.OfficeId == request.OfficeId).Select(x=>x.OfficeName).FirstOrDefaultAsync(),
                //     CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(request.FromDate.Month),
                //     (request.FromDate.Year),
                //     String.Join(",", AttendanceGroupWithNoInOutTimeName)
                //     ));
                // }

                var employeeRecord = empDetails.Select(x=> new ExistingAttendanceDetailModel{
                    EmployeeID = x.EmployeeID,
                    AttendanceGroupId = x.EmployeeProfessionalDetail.AttendanceGroupId,
                    AttendanceGroupName = x.EmployeeProfessionalDetail.AttendanceGroupMaster.Name,
                    InTime = payrollDetail.Where(y=> y.OfficeId == x.EmployeeProfessionalDetail.OfficeId)
                    .Select(y=>y.PayrollList.Where(p => p.AttendanceGroupId == x.EmployeeProfessionalDetail.AttendanceGroupId)
                    .Select(z => z.InTime).FirstOrDefault()).FirstOrDefault(),
                    OutTime = payrollDetail.Where(y=> y.OfficeId == x.EmployeeProfessionalDetail.OfficeId)
                    .Select(y=>y.PayrollList.Where(p => p.AttendanceGroupId == x.EmployeeProfessionalDetail.AttendanceGroupId)
                    .Select(z => z.OutTime).FirstOrDefault()).FirstOrDefault(),
                    // InTime = payrollDetail.Where(y=>y.AttendanceGroupId == x.EmployeeProfessionalDetail.AttendanceGroupId).Select(y=> y.InTime).FirstOrDefault(),
                    // OutTime = payrollDetail.Where(y=>y.AttendanceGroupId == x.EmployeeProfessionalDetail.AttendanceGroupId).Select(y=> y.OutTime).FirstOrDefault(),
                    AttendanceType = (int)AttendanceType.P,
                    OfficeId = (x.EmployeeProfessionalDetail.OfficeId != null) ? x.EmployeeProfessionalDetail.OfficeId.Value : 0
                }).ToList();

                var existingAttendanceDetail = await _dbContext.EmployeeAttendance.Where(x=>x.IsDeleted == false && employeeRecord.Select(y=>y.EmployeeID).Contains(x.EmployeeId) && x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date).ToListAsync();
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