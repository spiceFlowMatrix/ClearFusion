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
    public class GetApprovedPayrollByMonthQueryHandler: IRequestHandler<GetApprovedPayrollByMonthQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetApprovedPayrollByMonthQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetApprovedPayrollByMonthQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            
            try
            {
                List<EmployeePaymentTypes> userdetail = await _dbContext
                                                                        .EmployeePaymentTypes
                                                                        .Where(x => x.PayrollMonth == request.Month && 
                                                                        x.PayrollYear == request.Year && 
                                                                        x.OfficeId == request.OfficeId && 
                                                                        x.IsDeleted == false)
                                                                        .Include(x => x.EmployeeDetail).ToListAsync();


                List<EmployeeMonthlyPayrollModel> userList = new List<EmployeeMonthlyPayrollModel>();

                foreach (EmployeePaymentTypes item in userdetail)
                {
                    EmployeeMonthlyPayrollModel obj = new EmployeeMonthlyPayrollModel()
                    {
                        EmployeeId = Convert.ToInt32(item.EmployeeID),
                        EmployeeName = item.EmployeeDetail?.EmployeeName,
                        PaymentType = Convert.ToInt32(item.PaymentType),
                        WorkingDays = item.Attendance,
                        PresentDays = item.Attendance,
                        AbsentDays = item.Absent,
                        LeaveHours = item.LeaveDays == null ? 0 : item.LeaveDays.Value,
                        TotalWorkHours = item.TotalDuration,
                        HourlyRate = item.BasicPay,
                        TotalGeneralAmount = item.BasicPay,
                        TotalAllowance = item.TotalAllowance,
                        TotalDeduction = item.TotalDeduction,
                        GrossSalary = item.GrossSalary,
                        OverTimeHours = item.OverTimeHours,
                        PensionRate = item.PensionRate,
                        SalaryTax = item.SalaryTax,
                        PensionAmount = Math.Round(Convert.ToDouble(item.PensionAmount), 2),
                        NetSalary = item.NetSalary,
                        AdvanceAmount = item.AdvanceAmount,
                        IsAdvanceApproved = item.IsAdvanceApproved == null ? false : item.IsAdvanceApproved.Value,
                        AdvanceRecoveryAmount = item.AdvanceRecoveryAmount ?? 0,
                        IsAdvanceRecovery = item.IsAdvanceRecovery == true ? false : true,
                        CurrencyCode = item.CurrencyCode,
                        CurrencyId = item.CurrencyId,
                        EmployeeCode = item.EmployeeDetail.EmployeeCode

                    };

                    obj.EmployeePayrollList = new List<EmployeePayrollModel>();



                    List<EmployeePayrollModel> EmployeePayrollMonthList = await _dbContext.EmployeePayrollMonth
                                                                                .Include(x => x.SalaryHeadDetails)
                                                                                .Where(x => x.EmployeeID == item.EmployeeID &&
                                                                                            x.Date.Year == item.PayrollYear &&
                                                                                            x.Date.Month == item.PayrollMonth && x.IsDeleted == false)
                                                                                .Select(x => new EmployeePayrollModel
                                                                                {
                                                                                    CurrencyId = x.CurrencyId,
                                                                                    EmployeeId = x.EmployeeID,
                                                                                    MonthlyAmount = x.MonthlyAmount,
                                                                                    PayrollId = x.MonthlyPayrollId,
                                                                                    SalaryHeadId = x.SalaryHeadId,
                                                                                    PensionRate = item.PensionRate,
                                                                                    HeadTypeId = x.HeadTypeId == null ? 0 : x.HeadTypeId.Value,
                                                                                    SalaryHeadType = x.SalaryHeadDetails.HeadTypeId == (int)SalaryHeadType.ALLOWANCE ? "Allowance" : x.SalaryHeadDetails.HeadTypeId == (int)SalaryHeadType.DEDUCTION ? "Deduction" : x.SalaryHeadDetails.HeadTypeId == (int)SalaryHeadType.GENERAL ? "General" : "",
                                                                                    SalaryHead = x.SalaryHeadDetails.HeadName,
                                                                                    AccountNo = x.AccountNo,
                                                                                    TransactionTypeId = x.TransactionTypeId
                                                                                }).OrderBy(x => x.TransactionTypeId).ThenBy(x => x.SalaryHeadType).ToListAsync();
                                                                                
                    obj.EmployeePayrollList.AddRange(EmployeePayrollMonthList);

                    userList.Add(obj);
                }

                response.data.EmployeeMonthlyPayrollApprovedList = userList;
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