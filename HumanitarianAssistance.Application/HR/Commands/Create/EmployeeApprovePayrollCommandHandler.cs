using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class EmployeeApprovePayrollCommandHandler: IRequestHandler<EmployeeApprovePayrollCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public EmployeeApprovePayrollCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(EmployeeApprovePayrollCommand request, CancellationToken cancellationToken)
        {
           ApiResponse response = new ApiResponse();

            try
            {
                foreach (EmployeeMonthlyPayrollModel finalPayroll in request.EmployeeMonthlyPayroll)
                {
                    EmployeeMonthlyAttendance xEmployeeMonthlyAttendance =      await _dbContext.EmployeeMonthlyAttendance
                                                                                                .FirstOrDefaultAsync(x => x.EmployeeId == finalPayroll.EmployeeId && x.IsDeleted == false &&
                                                                                                x.Month == finalPayroll.Month && x.Year == finalPayroll.Year);

                    if (xEmployeeMonthlyAttendance != null)
                    {
                        xEmployeeMonthlyAttendance.IsApproved = true;
                        xEmployeeMonthlyAttendance.AdvanceAmount = finalPayroll.AdvanceAmount ?? 0;
                        xEmployeeMonthlyAttendance.AdvanceRecoveryAmount = finalPayroll.AdvanceRecoveryAmount ?? 0;
                        xEmployeeMonthlyAttendance.IsAdvanceApproved = finalPayroll.IsAdvanceApproved;
                        xEmployeeMonthlyAttendance.IsAdvanceRecovery = finalPayroll.IsAdvanceRecovery;
                        xEmployeeMonthlyAttendance.ModifiedById= request.ModifiedById;
                        xEmployeeMonthlyAttendance.ModifiedDate= request.ModifiedDate;
                        
                        _dbContext.EmployeeMonthlyAttendance.Update(xEmployeeMonthlyAttendance);
                        await _dbContext.SaveChangesAsync();
                    }

                    EmployeePaymentTypes EmployeeApprovedPayroll = new EmployeePaymentTypes();

                    EmployeeApprovedPayroll.IsDeleted = false;
                    EmployeeApprovedPayroll.Absent = finalPayroll.AbsentDays;
                    EmployeeApprovedPayroll.AdvanceAmount = finalPayroll.AdvanceAmount;
                    EmployeeApprovedPayroll.AdvanceRecoveryAmount = finalPayroll.IsAdvanceRecovery? finalPayroll.AdvanceRecoveryAmount:0;
                    EmployeeApprovedPayroll.Attendance = finalPayroll.PresentDays;
                    EmployeeApprovedPayroll.BasicPay = Convert.ToSingle(finalPayroll.TotalGeneralAmount);
                    EmployeeApprovedPayroll.CurrencyId = finalPayroll.CurrencyId;
                    EmployeeApprovedPayroll.EmployeeID = finalPayroll.EmployeeId;
                    EmployeeApprovedPayroll.GrossSalary = finalPayroll.GrossSalary;
                    EmployeeApprovedPayroll.HourlyRate = finalPayroll.HourlyRate;
                    EmployeeApprovedPayroll.IsAdvanceApproved = finalPayroll.IsAdvanceApproved;
                    EmployeeApprovedPayroll.IsAdvanceRecovery = finalPayroll.IsAdvanceRecovery;
                    EmployeeApprovedPayroll.IsApproved = true;
                    EmployeeApprovedPayroll.LeaveDays = finalPayroll.LeaveHours;
                    EmployeeApprovedPayroll.NetSalary = finalPayroll.NetSalary;
                    EmployeeApprovedPayroll.OfficeId = finalPayroll.OfficeId;
                    EmployeeApprovedPayroll.OverTimeHours = finalPayroll.OverTimeHours;
                    EmployeeApprovedPayroll.PaymentType = finalPayroll.PaymentType;
                    EmployeeApprovedPayroll.PayrollMonth = finalPayroll.Month;
                    EmployeeApprovedPayroll.PayrollYear = finalPayroll.Year;
                    EmployeeApprovedPayroll.PensionAmount = finalPayroll.PensionAmount;
                    EmployeeApprovedPayroll.PensionRate = finalPayroll.PensionRate;
                    EmployeeApprovedPayroll.SalaryTax = finalPayroll.SalaryTax;
                    EmployeeApprovedPayroll.TotalAllowance = finalPayroll.TotalAllowance;
                    EmployeeApprovedPayroll.TotalDeduction = finalPayroll.TotalDeduction;
                    EmployeeApprovedPayroll.TotalDuration = finalPayroll.TotalWorkHours;
                    EmployeeApprovedPayroll.TotalGeneralAmount = finalPayroll.NetSalary;
                    EmployeeApprovedPayroll.PaymentDate = new DateTime(finalPayroll.Year, finalPayroll.Month, DateTime.Now.Day);
                    
                    EmployeeApprovedPayroll.CreatedById= request.CreatedById;
                    EmployeeApprovedPayroll.CreatedDate= request.CreatedDate;

                    await _dbContext.EmployeePaymentTypes.AddAsync(EmployeeApprovedPayroll);
                    await _dbContext.SaveChangesAsync();

                    if (finalPayroll.EmployeePayrollList.Count > 0)
                    {
                        foreach (var payroll in finalPayroll.EmployeePayrollList)
                        {
                            EmployeePayrollMonth employeePayrollMonth = new EmployeePayrollMonth();
                            employeePayrollMonth.CurrencyId = payroll.CurrencyId;
                            employeePayrollMonth.Date = new DateTime(finalPayroll.Year, finalPayroll.Month, DateTime.Now.Day);
                            employeePayrollMonth.EmployeeID = finalPayroll.EmployeeId;
                            employeePayrollMonth.IsDeleted = false;
                            employeePayrollMonth.MonthlyAmount = payroll.MonthlyAmount;
                            employeePayrollMonth.SalaryHeadId = payroll.SalaryHeadId;
                            employeePayrollMonth.HeadTypeId = payroll.HeadTypeId;
                            employeePayrollMonth.PaymentType = payroll.PaymentType;
                            employeePayrollMonth.AccountNo = payroll.AccountNo;
                            employeePayrollMonth.TransactionTypeId = payroll.TransactionTypeId;
                            employeePayrollMonth.CreatedById= request.CreatedById;
                            employeePayrollMonth.CreatedDate= request.CreatedDate;
                            await _dbContext.EmployeePayrollMonth.AddAsync(employeePayrollMonth);
                            await _dbContext.SaveChangesAsync();
                        }
                    }

                    if (finalPayroll.IsAdvanceRecovery)
                    {
                        Advances xAdvances = await _dbContext.Advances.FirstOrDefaultAsync(x => x.IsDeleted == false && x.IsApproved == true
                                                                        && x.EmployeeId == finalPayroll.EmployeeId && x.OfficeId == finalPayroll.OfficeId
                                                                        && x.AdvanceDate < DateTime.Now && x.IsDeducted == false);

                        if (xAdvances != null && finalPayroll.IsAdvanceRecovery)
                        {
                            if (xAdvances.RecoveredAmount != xAdvances.AdvanceAmount)
                            {
                                xAdvances.RecoveredAmount += finalPayroll.AdvanceRecoveryAmount.Value;
                                xAdvances.IsAdvanceRecovery = finalPayroll.IsAdvanceRecovery;
                                xAdvances.DeductedDate = DateTime.Now;
                                xAdvances.NumberOfInstallments -= 1;

                                if (xAdvances.RecoveredAmount == xAdvances.AdvanceAmount)
                                {
                                    xAdvances.IsAdvanceRecovery = false;
                                    xAdvances.IsDeducted = true;
                                }

                                EmployeeApprovedPayroll.AdvanceId = xAdvances.AdvancesId;
                                EmployeeApprovedPayroll.ModifiedById= request.ModifiedById;
                                EmployeeApprovedPayroll.ModifiedDate= request.ModifiedDate;

                                _dbContext.EmployeePaymentTypes.Update(EmployeeApprovedPayroll);
                                await _dbContext.SaveChangesAsync();
                            }

                             xAdvances.ModifiedById= request.ModifiedById;
                             xAdvances.ModifiedDate= request.ModifiedDate;

                             _dbContext.Advances.Update(xAdvances);
                             await _dbContext.SaveChangesAsync();
                        }
                    }
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