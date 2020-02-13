using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetPayrollAdministrationDetailByIdQueryHandler : IRequestHandler<GetPayrollAdministrationDetailByIdQuery, object>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetPayrollAdministrationDetailByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetPayrollAdministrationDetailByIdQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> result= new Dictionary<string, object>();
            List<PayrollAdministrationModel> returnList = new List<PayrollAdministrationModel>();
            try
            {
                FinancialYearDetail financialYear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.IsDefault == true);

                if(financialYear == null)
                {
                    throw new Exception(StaticResource.FinancialYearNotFound);
                }

                var empDetails = await _dbContext.EmployeeDetail.Include(x=>x.EmployeeProfessionalDetail)
                                .ThenInclude(x=>x.AttendanceGroupMaster)
                                .Where(x=>x.IsDeleted == false && request.EmpIds.Contains(x.EmployeeID) && x.EmployeeProfessionalDetail.AttendanceGroupId != null)
                                .ToListAsync();

                if(empDetails.Count() != request.EmpIds.Count()) 
                {
                    throw new Exception("Some Employees don't have Attendance Group assigned to them!");
                }

                var payrollDetail = await _dbContext.PayrollMonthlyHourDetail.Where(x => x.OfficeId == request.OfficeId && x.PayrollYear == financialYear.StartDate.Year && x.PayrollMonth == request.Month && x.IsDeleted == false)
                                                        .ToListAsync();

                var AttendanceGroupWithNoInOutTimeId = empDetails.Select(x => (x.EmployeeProfessionalDetail.AttendanceGroupId)).Except(payrollDetail.Select(x=>x.AttendanceGroupId)).ToList();
                if(AttendanceGroupWithNoInOutTimeId.Any()) 
                {
                    var AttendanceGroupWithNoInOutTimeName = empDetails.Where(x=>AttendanceGroupWithNoInOutTimeId.Contains(x.EmployeeProfessionalDetail.AttendanceGroupId)).Select(x=>x.EmployeeProfessionalDetail.AttendanceGroupMaster.Name).Distinct().ToList();
                    
                    throw new Exception(String.Format(StaticResource.PayrollDailyHoursNotSetForAttendanceGroup, 
                    await _dbContext.OfficeDetail.Where(x=>x.IsDeleted==false && x.OfficeId == request.OfficeId).Select(x=>x.OfficeName).FirstOrDefaultAsync(),
                    CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(request.Month),
                    (financialYear.StartDate.Year),
                    String.Join(",", AttendanceGroupWithNoInOutTimeName)
                    ));
                }

                List<EmployeeAttendance> empPayrollAttendance = await _dbContext.EmployeeAttendance
                                                                .Include(x=>x.EmployeeDetails)
                                                                .Where(x => request.EmpIds.Contains(x.EmployeeId) &&
                                                                    x.Date.Month == request.Month && x.Date.Year == financialYear.StartDate.Year
                                                                    && x.IsDeleted == false && x.EmployeeDetails.IsDeleted == false)
                                                                .ToListAsync();

                var distinctAttendance = empPayrollAttendance.Select(x=>x.EmployeeId)
                                                                .Distinct().ToList();
                if(distinctAttendance.Count() != request.EmpIds.Count())
                {
                    var empWithNoAttendance = request.EmpIds.Except(distinctAttendance).ToList();
                    throw new Exception(String.Format("Employees with Code: {0}, doesnot have their attendance set for Month: {1}!",
                            String.Join(",", empWithNoAttendance.Select(x=>string.Format("E{0}", x))), ((Month)request.Month).ToString()));
                }

                List<EmployeeBasicSalaryDetail> basicPay = await _dbContext.EmployeeBasicSalaryDetail.Where(x => x.IsDeleted == false && request.EmpIds.Contains(x.EmployeeId))
                                                            .Include(x=>x.CurrencyDetails)
                                                            .ToListAsync();

                if (basicPay.Count() != request.EmpIds.Count())
                {
                    throw new Exception(String.Format("Basic pay not set for Employee Code: {0}", String.Join(",", request.EmpIds.Except(basicPay.Select(x=>x.EmployeeId)).Select(x=>string.Format("E{0}", x)).ToList()) ));
                }

                var payrollInfoDetail = await _dbContext.EmployeePayrollInfoDetail.Where(x => x.IsDeleted == false &&
                                                                                   x.Month == request.Month && x.Year == financialYear.StartDate.Year).ToListAsync();
                foreach (var emp in request.EmpIds)
                {   
                    var payroll = payrollInfoDetail.FirstOrDefault(x=>x.EmployeeId == emp);
                    
                    if (payroll == null)
                    {
                        GetEmployeeMonthlyPayrollQuery obj = new GetEmployeeMonthlyPayrollQuery{
                            EmployeeId = emp,
                            Month = request.Month
                        };
                        PayrollModel model = GetEmployeeMonthlyPayrollQueryHandler.calculateEmployeePayroll(empPayrollAttendance.Where(x=>x.EmployeeId == emp).ToList(), obj, payroll, _dbContext);
                        PayrollAdministrationModel returnObj = new PayrollAdministrationModel{
                            EmployeeId = emp,
                            EmployeeName = empDetails.Where(x=>x.EmployeeID == emp).Select(x=> (x.EmployeeCode + '-' + x.EmployeeName)).FirstOrDefault(),
                            Currency = basicPay.Where(x=>x.EmployeeId == emp).Select(x=>x.CurrencyDetails.CurrencyName).FirstOrDefault(),
                            GrossSalary = model.GrossSalary,
                            NETSalary = model.NetSalary,
                            IsApproved = model.IsSalaryApproved,
                            AccumulatedPayrollHeadList = model.AccumulatedPayrollHeadList,
                            SavedAccumulatedPayrollHeadList = model.SavedAccumulatedPayrollHeadList
                        };
                        returnList.Add(returnObj);
                    }
                    else
                    {
                        GetEmployeeMonthlyPayrollQuery obj = new GetEmployeeMonthlyPayrollQuery{
                            EmployeeId = emp,
                            Month = request.Month
                        };
                        PayrollModel model = GetEmployeeMonthlyPayrollQueryHandler.FetchEmployeePayroll(empPayrollAttendance.Where(x=>x.EmployeeId == emp).ToList(), obj, payroll, _dbContext);
                        PayrollAdministrationModel returnObj = new PayrollAdministrationModel{
                            EmployeeId = emp,
                            EmployeeName = empDetails.Where(x=>x.EmployeeID == emp).Select(x=>(x.EmployeeCode + '-' + x.EmployeeName)).FirstOrDefault(),
                            Currency = basicPay.Where(x=>x.EmployeeId == emp).Select(x=>x.CurrencyDetails.CurrencyName).FirstOrDefault(),
                            GrossSalary = model.GrossSalary,
                            NETSalary = model.NetSalary,
                            IsApproved = model.IsSalaryApproved,
                            AccumulatedPayrollHeadList = model.AccumulatedPayrollHeadList,
                            SavedAccumulatedPayrollHeadList = model.SavedAccumulatedPayrollHeadList
                        };
                        returnList.Add(returnObj);
                    }   
                }

                result.Add("RecordCount", returnList.Count());
                result.Add("PayrollAdminList", returnList.OrderBy(x=>x.EmployeeId).Skip(request.PageSize * request.PageIndex).Take(request.PageSize).ToList());


            }
            catch(Exception ex)
            { 
                throw ex;
            }
            return result;
        }

    }
}