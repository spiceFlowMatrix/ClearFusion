using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Commands.Create
{
    public class GenerateSalaryVoucherCommandHandler : IRequestHandler<GenerateSalaryVoucherCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GenerateSalaryVoucherCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GenerateSalaryVoucherCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var exchangeRates = await _dbContext.ExchangeRateDetail.Where(x => x.IsDeleted == false && x.Date.Date == DateTime.UtcNow.Date && x.OfficeId == request.OfficeId).ToListAsync();
                var financialYear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDefault == true && x.IsDeleted == false);
                var office = await _dbContext.OfficeDetail.FirstOrDefaultAsync(o => o.OfficeId == request.OfficeId); //use OfficeCode
                var employeeDetails = await _dbContext.EmployeeDetail.FirstOrDefaultAsync(x => x.EmployeeID == request.EmployeeId && x.IsDeleted == false);
                var currency = await _dbContext.CurrencyDetails.FirstOrDefaultAsync(x => x.IsDeleted == false && x.CurrencyId == request.CurrencyId);

                List<VoucherTransactions> voucherTransactionsList = new List<VoucherTransactions>();

                //for gross salary= basicpay * totalworkhours
                decimal? grossSalary = request.EmployeePayrollLists.Where(x => x.HeadTypeId == (int)SalaryHeadType.GENERAL).Sum(x => x.MonthlyAmount) * request.PresentHours;

                //total Allowances of an employee over a month
                decimal? totalAllowance = request.EmployeePayrollLists.Where(x => x.HeadTypeId == (int)SalaryHeadType.ALLOWANCE).Sum(x => x.MonthlyAmount);

                //total deductions of an employee over a month
                decimal? totalDeductions = request.EmployeePayrollLists.Where(x => x.HeadTypeId == (int)SalaryHeadType.DEDUCTION).Sum(x => x.MonthlyAmount);

                decimal? totalDeductionsPayrollHeads = request.EmployeePayrollListPrimary.Sum(x => x.Amount);

                //total salary payable to employee in a month
                decimal? totalSalaryOfEmployee = grossSalary + totalAllowance;

                //var exchangeRates= await exchangeRateTask;
                //exchangeRateTask.Dispose();

                if (exchangeRates.Any())
                {
                    // check for voucher to be balanced
                    if ((grossSalary + totalAllowance) == (totalDeductions + totalDeductionsPayrollHeads))
                    {
                        //var financialYear = await financialYearTask;
                        //financialYearTask.Dispose();
                        //var office = await officeCodeTask;
                        //officeCodeTask.Dispose();
                        //var employeeDetails = await employeeDetailsTask;
                        //employeeDetailsTask.Dispose();
                        //var currency = await currencyCodeTask;
                        //currencyCodeTask.Dispose();

                        int voucherCount = _dbContext.VoucherDetail.Where(x => x.VoucherDate.Month == DateTime.UtcNow.Month && x.OfficeId == request.OfficeId && x.VoucherDate.Year == DateTime.UtcNow.Year && x.CurrencyId == request.CurrencyId).Count();

                        // Pattern: Office Code - Currency Code - Month Number - voucher count on selected month - Year
                        string referenceNo = AccountingUtility.GenerateVoucherReferenceCode(DateTime.UtcNow, voucherCount, currency.CurrencyCode, office.OfficeCode);

                        int sameVoucherReferenceNoCount = 0;

                        //Creating Voucher for Voucher transaction
                        VoucherDetail obj = new VoucherDetail
                        {
                            CreatedById = request.CreatedById,
                            CreatedDate = DateTime.UtcNow,
                            IsDeleted = false,
                            FinancialYearId = financialYear.FinancialYearId,
                            VoucherTypeId = (int)VoucherTypes.Journal,
                            Description = AccountingUtility.GetSalaryDescription(employeeDetails.EmployeeCode, employeeDetails.EmployeeName, request.PayrollMonth.Month, totalSalaryOfEmployee),
                            CurrencyId = request.CurrencyId,
                            VoucherDate = DateTime.UtcNow,
                            JournalCode = request.JournalCode,//null for now as per client
                            OfficeId = request.OfficeId,
                            //ReferenceNo= AccountingUtility.GenerateVoucherReferenceCode(DateTime.Now, voucherCount, currency.CurrencyCode, office.OfficeCode)
                        };



                        foreach (SalaryHeadModel salaryhead in request.EmployeePayrollLists)
                        {
                            VoucherTransactions xVoucherTransactions = new VoucherTransactions
                            {

                                //Creating Voucher Transaction for Credit
                                IsDeleted = false,
                                VoucherNo = obj.VoucherNo,
                                FinancialYearId = financialYear.FinancialYearId,
                                CurrencyId = request.CurrencyId,
                                OfficeId = request.OfficeId
                            };

                            try
                            {
                                //Include only salary heads in voucher that contain transaction type ""
                                if (salaryhead.TransactionTypeId != null && salaryhead.TransactionTypeId != 0)
                                {
                                    if (salaryhead.AccountNo != 0)
                                    {
                                        // Include only salary heads in voucher that has transaction type as credit and salary head type is not general
                                        if (salaryhead.TransactionTypeId == (int)TransactionType.Debit && (salaryhead.MonthlyAmount != null && salaryhead.MonthlyAmount != 0) && salaryhead.HeadTypeId != (int)SalaryHeadType.GENERAL)
                                        {
                                            xVoucherTransactions.ChartOfAccountNewId = salaryhead.AccountNo;
                                            xVoucherTransactions.DebitAccount = salaryhead.AccountNo;
                                            xVoucherTransactions.Description = string.Format(StaticResource.SalaryHeadAllowances, salaryhead.HeadName);
                                            xVoucherTransactions.Debit = Convert.ToDouble(salaryhead.MonthlyAmount);
                                            xVoucherTransactions.Credit = 0;

                                            //Note : These values are associated with Voucher and Transactions
                                            xVoucherTransactions.TransactionDate = obj.VoucherDate;
                                            xVoucherTransactions.FinancialYearId = obj.FinancialYearId;
                                            xVoucherTransactions.CurrencyId = obj.CurrencyId;

                                            voucherTransactionsList.Add(xVoucherTransactions);

                                        }//Include only salary heads in voucher that has transaction type as debit and salary head type is not general
                                        else if (salaryhead.TransactionTypeId == (int)TransactionType.Credit && (salaryhead.MonthlyAmount != null && salaryhead.MonthlyAmount != 0) && salaryhead.HeadTypeId != (int)SalaryHeadType.GENERAL)
                                        {
                                            xVoucherTransactions.ChartOfAccountNewId = salaryhead.AccountNo;
                                            xVoucherTransactions.CreditAccount = salaryhead.AccountNo;
                                            xVoucherTransactions.Description = string.Format(StaticResource.SalaryHeadDeductions, salaryhead.HeadName);
                                            xVoucherTransactions.Credit = Convert.ToDouble(salaryhead.MonthlyAmount);
                                            xVoucherTransactions.Debit = 0;

                                            voucherTransactionsList.Add(xVoucherTransactions);
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("Salary head accounts not set");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }
                        }

                        foreach (PayrollHeadModel payrollHead in request.EmployeePayrollListPrimary)
                        {

                            if (payrollHead.AccountNo != null)
                            {
                                VoucherTransactions xVoucherTransactions = new VoucherTransactions();

                                //Creating Voucher Transaction for Credit
                                xVoucherTransactions.IsDeleted = false;
                                //xVoucherTransactions.VoucherNo = obj.VoucherNo;
                                xVoucherTransactions.FinancialYearId = financialYear.FinancialYearId;
                                xVoucherTransactions.CurrencyId = request.CurrencyId;
                                xVoucherTransactions.OfficeId = request.OfficeId;

                                try
                                {
                                    //Include only salary heads in voucher that contain transaction type ""
                                    if (payrollHead.TransactionTypeId != null && payrollHead.TransactionTypeId != 0)
                                    {
                                        //Include only salary heads in voucher that has transaction type as credit
                                        if (payrollHead.TransactionTypeId == (int)TransactionType.Debit && (payrollHead.Amount != null && payrollHead.Amount != 0))
                                        {
                                            xVoucherTransactions.ChartOfAccountNewId = payrollHead.AccountNo;
                                            xVoucherTransactions.DebitAccount = payrollHead.AccountNo;

                                            if (payrollHead.PayrollHeadTypeId != (int)SalaryHeadType.GENERAL)
                                            {
                                                xVoucherTransactions.Description = string.Format(StaticResource.SalaryHeadAllowances, payrollHead.PayrollHeadName);
                                            }
                                            else
                                            {
                                                xVoucherTransactions.Description = payrollHead.PayrollHeadName + "Debited";
                                            }

                                            xVoucherTransactions.Debit = Convert.ToDouble(payrollHead.Amount);
                                            xVoucherTransactions.Credit = 0;
                                            xVoucherTransactions.TransactionDate = obj.VoucherDate;
                                            xVoucherTransactions.FinancialYearId = obj.FinancialYearId;
                                            xVoucherTransactions.CurrencyId = obj.CurrencyId;

                                            voucherTransactionsList.Add(xVoucherTransactions);

                                        }//Include only salary heads in voucher that has transaction type as debit
                                        else if (payrollHead.TransactionTypeId == (int)TransactionType.Credit && (payrollHead.Amount != null && payrollHead.Amount != 0))
                                        {
                                            xVoucherTransactions.ChartOfAccountNewId = Convert.ToInt32(payrollHead.AccountNo);
                                            xVoucherTransactions.CreditAccount = Convert.ToInt32(payrollHead.AccountNo);

                                            if (payrollHead.PayrollHeadTypeId != (int)SalaryHeadType.GENERAL)
                                            {
                                                xVoucherTransactions.Description = string.Format(StaticResource.SalaryHeadDeductions, payrollHead.PayrollHeadName);
                                            }
                                            else
                                            {
                                                xVoucherTransactions.Description = payrollHead.PayrollHeadName + "Credited";
                                            }

                                            xVoucherTransactions.Credit = Convert.ToDouble(payrollHead.Amount);
                                            xVoucherTransactions.Debit = 0;
                                            xVoucherTransactions.TransactionDate = obj.VoucherDate;
                                            xVoucherTransactions.FinancialYearId = obj.FinancialYearId;
                                            xVoucherTransactions.CurrencyId = obj.CurrencyId;

                                            voucherTransactionsList.Add(xVoucherTransactions);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(ex.Message);
                                }
                            }
                            else
                            {
                                throw new Exception("Payroll head accounts not set");
                            }
                        }

                        if (!string.IsNullOrEmpty(referenceNo))
                        {
                            do
                            {
                                sameVoucherReferenceNoCount = await _dbContext.VoucherDetail.Where(x => x.ReferenceNo == referenceNo).CountAsync();

                                if (sameVoucherReferenceNoCount == 0)
                                {
                                    obj.ReferenceNo = referenceNo;
                                }
                                else
                                {
                                    //DO NOT REMOVE: This is used to get the latest voucher and then we will get the count of vouhcer sequence from it
                                    // VoucherDetail voucherDetail = _dbContext.VoucherDetail.OrderByDescending(x => x.VoucherDate).FirstOrDefault(x => x.VoucherDate.Month == filterVoucherDate.Month && x.OfficeId == model.OfficeId && x.VoucherDate.Year == filterVoucherDate.Year);

                                    var refNo = referenceNo.Split('-');
                                    int count = Convert.ToInt32(refNo[3]);
                                    referenceNo = AccountingUtility.GenerateVoucherReferenceCode(DateTime.Now, count, currency.CurrencyCode, office.OfficeCode);
                                }
                            }
                            while (sameVoucherReferenceNoCount != 0);
                        }

                        await _dbContext.VoucherDetail.AddAsync(obj);
                        await _dbContext.SaveChangesAsync();

                        voucherTransactionsList.ForEach(x => x.VoucherNo = obj.VoucherNo);

                        //Creating Voucher transactions for Gross Salary it is being calculated in this method so we need to insert record for it separately
                        if (grossSalary != null && grossSalary != 0)
                        {
                            VoucherTransactions xVoucherTransactions = new VoucherTransactions();
                            //Creating Voucher Transaction for Credit
                            xVoucherTransactions.IsDeleted = false;
                            xVoucherTransactions.VoucherNo = obj.VoucherNo;
                            xVoucherTransactions.FinancialYearId = financialYear.FinancialYearId;
                            xVoucherTransactions.CurrencyId = request.CurrencyId;
                            xVoucherTransactions.OfficeId = request.OfficeId;

                            xVoucherTransactions.ChartOfAccountNewId = request.EmployeePayrollLists.FirstOrDefault(x => x.HeadTypeId == (int)SalaryHeadType.GENERAL).AccountNo;
                            xVoucherTransactions.CreditAccount = xVoucherTransactions.ChartOfAccountNewId;
                            xVoucherTransactions.Description = "Basic Pay Debited";
                            xVoucherTransactions.Debit = Convert.ToDouble(grossSalary);
                            xVoucherTransactions.Credit = 0;
                            xVoucherTransactions.TransactionDate = obj.VoucherDate;
                            xVoucherTransactions.FinancialYearId = obj.FinancialYearId;
                            xVoucherTransactions.CurrencyId = obj.CurrencyId;

                            voucherTransactionsList.Add(xVoucherTransactions);

                            await _dbContext.VoucherTransactions.AddRangeAsync(voucherTransactionsList);
                            await _dbContext.SaveChangesAsync();
                        }

                        //Creating an entry in EmployeeSalaryPaymentHistory Table
                        EmployeeSalaryPaymentHistory employeeSalaryPaymentHistory = new EmployeeSalaryPaymentHistory();
                        employeeSalaryPaymentHistory.CreatedById = request.CreatedById;
                        employeeSalaryPaymentHistory.CreatedDate = DateTime.UtcNow;
                        employeeSalaryPaymentHistory.IsDeleted = false;
                        employeeSalaryPaymentHistory.EmployeeId = request.EmployeeId;
                        employeeSalaryPaymentHistory.VoucherNo = obj.VoucherNo;
                        employeeSalaryPaymentHistory.IsSalaryReverse = false;
                        employeeSalaryPaymentHistory.Year = request.PayrollMonth.Year;
                        employeeSalaryPaymentHistory.Month = request.PayrollMonth.Month;

                        await _dbContext.EmployeeSalaryPaymentHistory.AddAsync(employeeSalaryPaymentHistory);
                        await _dbContext.SaveChangesAsync();

                        response.data.VoucherReferenceNo = obj.ReferenceNo;
                        response.data.VoucherNo = obj.VoucherNo;

                        var user = await _dbContext.UserDetails.FirstOrDefaultAsync(x => x.AspNetUserId == request.CreatedById);

                        LoggerDetailsModel loggerObj = new LoggerDetailsModel();
                        loggerObj.NotificationId = (int)LoggerEnum.VoucherCreated;
                        loggerObj.IsRead = false;
                        loggerObj.UserName = user.FirstName + " " + user.LastName;
                        loggerObj.UserId = request.CreatedById;
                        loggerObj.LoggedDetail = "Voucher " + obj.ReferenceNo + " Created";
                        loggerObj.CreatedDate = DateTime.Now;

                        response.LoggerDetailsModel = loggerObj;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    }
                    else
                    {
                        throw new Exception("Salary Voucher is not balanced");
                    }
                }
                else
                {
                    throw new Exception($"Exchange Rate not Defined for {DateTime.UtcNow.Date.Day}/{DateTime.UtcNow.Date.Month}/{DateTime.UtcNow.Date.Year}");
                }
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