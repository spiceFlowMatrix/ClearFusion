using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class DisapproveEmployeeApprovedSalaryCommandHandler : IRequestHandler<DisapproveEmployeeApprovedSalaryCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public DisapproveEmployeeApprovedSalaryCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(DisapproveEmployeeApprovedSalaryCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                foreach (Employees Employee in request.EmployeeList)
                {
                    //Check if salary paymet is done for an approved payroll
                    EmployeeSalaryPaymentHistory employeeSalaryPaymentHistory = _dbContext.EmployeeSalaryPaymentHistory.FirstOrDefault(x => x.EmployeeId == Employee.EmployeeId
                                                                                                                                && x.Year == request.Year && x.Month == request.Month && x.IsSalaryReverse == false
                                                                                                                                && x.IsDeleted == false);
                    //if salary payment is already made
                    if (employeeSalaryPaymentHistory != null)
                    {
                        //Getting the details for salary payment voucher
                        VoucherDetail voucherDetail = _dbContext.VoucherDetail.FirstOrDefault(x => x.IsDeleted == false && x.VoucherNo == employeeSalaryPaymentHistory.VoucherNo);

                        if (voucherDetail != null)
                        {
                            //Reverse the salary payment for the approved payroll of an employee
                            ApiResponse apiResponse = await ReverseEmployeeSalaryVoucher(voucherDetail.VoucherNo, request.ModifiedById);

                            if (apiResponse.StatusCode == 200)
                            {
                                //Update salary reversed flag in table employeesalarypaymenthistory 
                                employeeSalaryPaymentHistory.IsSalaryReverse = true;
                                await _dbContext.SaveChangesAsync();

                                //Retrieving Approved payroll for the month
                                EmployeePaymentTypes employeePaymentTypes = await _dbContext.EmployeePaymentTypes.FirstOrDefaultAsync(x => x.IsDeleted == false && x.PayrollYear == request.Year
                                                                                                                    && x.PayrollMonth == request.Month &&
                                                                                                                    x.EmployeeID == Employee.EmployeeId);

                                employeePaymentTypes.IsDeleted = true;

                                //update approved employee payroll table
                                await _dbContext.SaveChangesAsync();

                                //get employee monthly salary heads 
                                List<EmployeePayrollMonth> EmployeePayrollMonthList = await _dbContext.EmployeePayrollMonth.Where(x => x.IsDeleted == false && x.Date.Month == request.Month && x.Date.Year == request.Year && x.EmployeeID == Employee.EmployeeId).ToListAsync();

                                //set each monthly salary head to isdeleted false
                                EmployeePayrollMonthList.ForEach(x => x.IsDeleted = true);

                                //update employee monthly salary heads 
                                _dbContext.EmployeePayrollMonth.UpdateRange(EmployeePayrollMonthList);
                                await _dbContext.SaveChangesAsync();

                                //Retrieving employee monthly attendance record
                                EmployeeMonthlyAttendance employeeMonthlyAttendance = await _dbContext.EmployeeMonthlyAttendance.FirstOrDefaultAsync(x => x.IsDeleted == false && x.EmployeeId == Employee.EmployeeId
                                                                                                                                    && x.Month == request.Month && x.Year == request.Year);

                                // add advances back to the advances table if present
                                Advances xAdvances = await _dbContext.Advances.OrderByDescending(x => x.AdvanceDate).FirstOrDefaultAsync(x => x.IsDeleted == false && x.IsApproved == true
                                                                            && x.EmployeeId == employeeMonthlyAttendance.EmployeeId && x.OfficeId == employeeMonthlyAttendance.OfficeId
                                                                            && x.AdvanceDate < DateTime.Now);


                                if (xAdvances != null && employeeMonthlyAttendance.AdvanceRecoveryAmount != 0)
                                {
                                    // xAdvances.AdvanceAmount = xAdvances.AdvanceAmount + employeeMonthlyAttendance.AdvanceRecoveryAmount;
                                    if (employeeMonthlyAttendance.IsAdvanceRecovery)
                                    {
                                        xAdvances.RecoveredAmount = xAdvances.RecoveredAmount - employeeMonthlyAttendance.AdvanceRecoveryAmount;
                                        xAdvances.IsDeducted = false;
                                        xAdvances.NumberOfInstallments = xAdvances.NumberOfInstallments + 1;
                                        _dbContext.Advances.Update(xAdvances);
                                        await _dbContext.SaveChangesAsync();
                                    }
                                }

                                //Setting monthly attendance approved to false
                                employeeMonthlyAttendance.IsApproved = false;
                                employeeMonthlyAttendance.IsAdvanceRecovery = false;
                                employeeMonthlyAttendance.AdvanceAmount = 0;
                                employeeMonthlyAttendance.AdvanceRecoveryAmount = 0;
                                employeeMonthlyAttendance.GrossSalary = 0;
                                employeeMonthlyAttendance.NetSalary = 0;
                                employeeMonthlyAttendance.PensionAmount = 0;
                                employeeMonthlyAttendance.SalaryTax = 0;
                                employeeMonthlyAttendance.TotalAllowance = 0;
                                employeeMonthlyAttendance.TotalDeduction = 0;

                                await _dbContext.SaveChangesAsync();
                            }
                        }
                    }
                    else
                    {
                        //Retrieving Approved payroll for the month
                        EmployeePaymentTypes employeePaymentTypes = await _dbContext.EmployeePaymentTypes.FirstOrDefaultAsync(x => x.IsDeleted == false && x.PayrollYear == request.Year
                                                                                                            && x.PayrollMonth == request.Month &&
                                                                                                            x.EmployeeID == Employee.EmployeeId);

                        employeePaymentTypes.IsDeleted = true;

                        //update approved employee payroll table
                        await _dbContext.SaveChangesAsync();

                        //get employee monthly salary heads 
                        List<EmployeePayrollMonth> EmployeePayrollMonthList = await _dbContext.EmployeePayrollMonth.Where(x => x.IsDeleted == false && x.Date.Month == request.Month && x.Date.Year == request.Year && x.EmployeeID == Employee.EmployeeId).ToListAsync();

                        //set each monthly salary head to isdeleted false
                        EmployeePayrollMonthList.ForEach(x => x.IsDeleted = true);

                        //update employee monthly salary heads 
                        _dbContext.EmployeePayrollMonth.UpdateRange(EmployeePayrollMonthList);
                        await _dbContext.SaveChangesAsync();

                        //Retrieving employee monthly attendance record
                        EmployeeMonthlyAttendance employeeMonthlyAttendance = await _dbContext.EmployeeMonthlyAttendance.FirstOrDefaultAsync(x => x.IsDeleted == false && x.EmployeeId == Employee.EmployeeId
                                                                                                                            && x.Month == request.Month && x.Year == request.Year);

                        // add advances back to the advances table if present
                        Advances xAdvances = await _dbContext.Advances.OrderByDescending(x => x.AdvanceDate).FirstOrDefaultAsync(x => x.IsDeleted == false && x.IsApproved == true
                                                                    && x.EmployeeId == employeeMonthlyAttendance.EmployeeId && x.OfficeId == employeeMonthlyAttendance.OfficeId
                                                                    && x.AdvanceDate < DateTime.Now);

                        if (xAdvances != null && employeeMonthlyAttendance.AdvanceRecoveryAmount != 0)
                        {
                            // xAdvances.AdvanceAmount = xAdvances.AdvanceAmount + employeeMonthlyAttendance.AdvanceRecoveryAmount;
                            if (employeeMonthlyAttendance.IsAdvanceRecovery)
                            {
                                xAdvances.RecoveredAmount = xAdvances.RecoveredAmount - employeeMonthlyAttendance.AdvanceRecoveryAmount;
                                xAdvances.NumberOfInstallments = xAdvances.NumberOfInstallments + 1;
                                xAdvances.IsDeducted = false;
                                _dbContext.Advances.Update(xAdvances);
                                await _dbContext.SaveChangesAsync();
                            }
                        }

                        employeeMonthlyAttendance.IsAdvanceRecovery = false;
                        employeeMonthlyAttendance.IsAdvanceApproved = false;
                        employeeMonthlyAttendance.AdvanceAmount = 0;
                        employeeMonthlyAttendance.AdvanceRecoveryAmount = 0;
                        employeeMonthlyAttendance.GrossSalary = 0;
                        employeeMonthlyAttendance.NetSalary = 0;
                        employeeMonthlyAttendance.PensionAmount = 0;
                        employeeMonthlyAttendance.SalaryTax = 0;
                        employeeMonthlyAttendance.TotalAllowance = 0;
                        employeeMonthlyAttendance.TotalDeduction = 0;

                        //Setting monthly attendance approved to false
                        employeeMonthlyAttendance.IsApproved = false;

                        await _dbContext.SaveChangesAsync();

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

        public async Task<ApiResponse> ReverseEmployeeSalaryVoucher(long VoucherNo, string UserId)
        {
            ApiResponse response = new ApiResponse();

            VoucherDetail voucherDetail = new VoucherDetail();

            try
            {
                //Retrieving the list of transactions based on voucher no
                List<VoucherTransactions> voucherTransactionDetails = await _dbContext.VoucherTransactions.Where(x => x.IsDeleted == false
                                                                                 && x.VoucherNo == VoucherNo).ToListAsync();

                //var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.OfficeId == 16).OrderByDescending(x => x.Date).ToListAsync();

                if (voucherTransactionDetails.Any())
                {
                    //looping each transaction and reversing it.
                    foreach (VoucherTransactions transaction in voucherTransactionDetails)
                    {
                        VoucherTransactions reverseTransactions = new VoucherTransactions();

                        reverseTransactions.Debit = transaction.Credit;
                        reverseTransactions.CreditAccount = transaction.DebitAccount;
                        reverseTransactions.DebitAccount = transaction.CreditAccount;
                        reverseTransactions.Credit = transaction.Debit;
                        reverseTransactions.CurrencyId = transaction.CurrencyId;
                        reverseTransactions.FinancialYearId = transaction.FinancialYearId;
                        reverseTransactions.ChartOfAccountNewId = transaction.ChartOfAccountNewId;
                        reverseTransactions.OfficeId = transaction.OfficeId;
                        reverseTransactions.VoucherNo = transaction.VoucherNo;
                        reverseTransactions.IsDeleted = false;
                        reverseTransactions.TransactionDate = voucherDetail.VoucherDate;

                        await _dbContext.VoucherTransactions.AddAsync(reverseTransactions);
                        await _dbContext.SaveChangesAsync();

                        //APIResponse apiResponse = await AddVoucherTransactionConvertedToExchangeRate(reverseTransactions, exchangeRate);
                    }
                }

                //Getting the Salary Payment history record and updating the flag isSalaryReversed to true
                EmployeeSalaryPaymentHistory employeeSalaryPaymentHistory = _dbContext.EmployeeSalaryPaymentHistory.FirstOrDefault(x => x.IsDeleted == false && x.IsSalaryReverse == false
                                                                                                                             && x.VoucherNo == VoucherNo);
                employeeSalaryPaymentHistory.IsSalaryReverse = true;

                await _dbContext.SaveChangesAsync();

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}
