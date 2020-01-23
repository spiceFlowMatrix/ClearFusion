using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeMonthlyPayrollQueryHandler : IRequestHandler<GetEmployeeMonthlyPayrollQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetEmployeeMonthlyPayrollQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetEmployeeMonthlyPayrollQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {
                EmployeeMonthlyAttendance empPayrollAttendance = await _dbContext.EmployeeMonthlyAttendance
                                                                                                  .Include(x => x.EmployeeDetails)
                                                                                                  .Include(x => x.EmployeeDetails.EmployeeProfessionalDetail)
                                                                                                  .FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId &&
                                                                                                  x.Month == request.Month && x.Year == DateTime.UtcNow.Year
                                                                                                  && x.IsDeleted == false && x.EmployeeDetails.IsDeleted == false);

                if (empPayrollAttendance == null)
                {
                    throw new Exception(StaticResource.AttendanceNotFound);
                }

                if(!empPayrollAttendance.IsApproved)
                {
                    object model = calculateEmployeePayroll(empPayrollAttendance, request);
                    response.Add("payroll", model);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        public double getExchangeRate(int? currencyId)
        {
            ExchangeRateDetail exchangeRateDetail = _dbContext.ExchangeRateDetail.OrderByDescending(x => x.Date).FirstOrDefault(x => x.FromCurrency == currencyId && x.ToCurrency == (int)Currency.AFG);

            if (exchangeRateDetail == null)
            {
                string currencyCode = _dbContext.CurrencyDetails.FirstOrDefault(x => x.IsDeleted == false && x.CurrencyId == currencyId).CurrencyCode;

                throw new Exception(StaticResource.ExchagneRateNotDefined);
            }

            return (double)exchangeRateDetail.Rate;
        }

        public object calculateEmployeePayroll(EmployeeMonthlyAttendance empPayrollAttendance, GetEmployeeMonthlyPayrollQuery request)
        {
            PayrollModel model = new PayrollModel();

             try
             {
                 PayrollMonthlyHourDetail payrollHours = _dbContext.PayrollMonthlyHourDetail
                                                                                     .FirstOrDefault(x => x.IsDeleted == false && x.OfficeId == empPayrollAttendance.EmployeeDetails.EmployeeProfessionalDetail.OfficeId
                                                                                     && x.PayrollMonth == request.Month && x.PayrollYear == DateTime.UtcNow.Year && x.AttendanceGroupId == empPayrollAttendance.EmployeeDetails.EmployeeProfessionalDetail.AttendanceGroupId);

                if (payrollHours == null)
                {
                    throw new Exception(StaticResource.PayrollDailyHoursNotSet);
                }

                //Note: default 0.045 i.e. (4.5 %)
                double? pensionRate = _dbContext.EmployeePensionRate.FirstOrDefault(x => x.IsDefault == true && x.IsDeleted == false)?.PensionRate;

                EmployeePayroll payrollBasicSalaryAndCurrency = _dbContext.EmployeePayroll.FirstOrDefault(x => x.HeadTypeId == (int)SalaryHeadType.GENERAL);

                if (payrollBasicSalaryAndCurrency == null)
                {
                    throw new Exception(StaticResource.BasicPayNotSet + empPayrollAttendance.EmployeeDetails.EmployeeCode);
                }

                model.isSalaryApproved = empPayrollAttendance.IsApproved;

                var bonusAndFines = _dbContext.EmployeeBonusFineSalaryHead.Where(x => x.EmployeeId == request.EmployeeId
                                                                            && x.Month == request.Month && x.Year == DateTime.UtcNow.Year).ToList();

                double totalBonus = bonusAndFines.Where(x => x.TransactionTypeId == (int)TransactionType.Debit).Select(x => x.Amount).DefaultIfEmpty(0).Sum();
                double totalFine = bonusAndFines.Where(x => x.TransactionTypeId == (int)TransactionType.Credit).Select(x => x.Amount).DefaultIfEmpty(0).Sum();
                double dSalaryTax = 0;
                double dPension = 0;

                double convertMinutesToHours = ((double)(empPayrollAttendance.OverTimeMinutes + empPayrollAttendance.AttendanceMinutes) / 60d);
                model.GrossSalary = Math.Round((double)(payrollBasicSalaryAndCurrency.MonthlyAmount * (empPayrollAttendance.AttendanceHours.Value + (empPayrollAttendance.LeaveHours ?? 0) + (empPayrollAttendance.OvertimeHours != null ? empPayrollAttendance.OvertimeHours.Value : 0) + convertMinutesToHours) + totalBonus), 2);
                AccumulatedPayrollHeads pension = new AccumulatedPayrollHeads
                {
                    Id= (int)AccumulatedSalaryHead.Pension,
                    Amount = dPension = Math.Round(((double)(model.GrossSalary * pensionRate) / 100), 2), // i.e. 4.5 % => 0.045
                    PayrollHeadName = "Pension",
                    TransactionType = (int)TransactionType.Credit
                };

                model.AccumulatedPayrollHeadList.Add(pension);

                AccumulatedPayrollHeads grossSalary = new AccumulatedPayrollHeads
                {
                    Id= (int)AccumulatedSalaryHead.GrossSalary,
                    Amount = model.GrossSalary,
                    PayrollHeadName = "Gross Salary",
                    TransactionType = (int)TransactionType.Credit
                };

                model.AccumulatedPayrollHeadList.Add(grossSalary);

                if (model.GrossSalary > 5000)
                {
                    double exchangeRate = getExchangeRate(payrollBasicSalaryAndCurrency.CurrencyId);

                    AccumulatedPayrollHeads salaryTax = new AccumulatedPayrollHeads
                    {
                        Id= (int)AccumulatedSalaryHead.SalaryTax,
                        Amount = dSalaryTax = Math.Round(Convert.ToDouble((StaticFunctions.SalaryCalculate(model.GrossSalary, exchangeRate))), 2),
                        PayrollHeadName = "Salary Tax",
                        TransactionType = (int)TransactionType.Credit
                    };

                    model.AccumulatedPayrollHeadList.Add(salaryTax);
                }
                else
                {
                    AccumulatedPayrollHeads salaryTax = new AccumulatedPayrollHeads
                    {
                        Id= (int)AccumulatedSalaryHead.SalaryTax,
                        Amount = dSalaryTax = 0,
                        PayrollHeadName = "Salary Tax",
                        TransactionType = (int)TransactionType.Credit
                    };

                    model.AccumulatedPayrollHeadList.Add(salaryTax);
                }

                //Net Salary  = (Gross + Allowances) - Deductions
                model.NetSalary = Math.Round((double)(model.GrossSalary - totalFine - dSalaryTax - empPayrollAttendance.AdvanceRecoveryAmount - dPension), 2);
                model.SalaryPaid = model.NetSalary;

                Advances xAdvances = _dbContext.Advances.FirstOrDefault(x => x.IsDeleted == false && x.IsApproved == true
                                                                           && x.EmployeeId == request.EmployeeId && x.OfficeId == empPayrollAttendance.EmployeeDetails.EmployeeProfessionalDetail.OfficeId
                                                                           && x.AdvanceDate.Date < DateTime.Now.Date && x.IsDeducted == false);

                AccumulatedPayrollHeads advance = new AccumulatedPayrollHeads();
                if (xAdvances != null)
                {
                    if (xAdvances.RecoveredAmount == 0)
                    {

                        if (xAdvances.NumberOfInstallments == 0)
                        {
                            xAdvances.NumberOfInstallments = 1;
                        }

                        advance.Id= (int)AccumulatedSalaryHead.AdvanceRecovery;
                        advance.Amount = Math.Round((Convert.ToDouble(xAdvances.AdvanceAmount / xAdvances.NumberOfInstallments ?? 1)), 2);
                        advance.PayrollHeadName = "Advance Recovery";
                        advance.TransactionType = (int)TransactionType.Credit;
                    }
                    else
                    {
                        Double iBalanceAmount = xAdvances.AdvanceAmount - xAdvances.RecoveredAmount;
                        advance.Id= (int)AccumulatedSalaryHead.AdvanceRecovery;
                        advance.Amount = Math.Round((Convert.ToDouble(iBalanceAmount / xAdvances.NumberOfInstallments)), 2);
                        advance.PayrollHeadName = "Advance Recovery";
                        advance.TransactionType = (int)TransactionType.Credit;
                    }
                }
                else
                {
                    advance.Id= (int)AccumulatedSalaryHead.AdvanceRecovery;
                    advance.Amount = 0;
                    advance.PayrollHeadName = "Advance Recovery";
                    advance.TransactionType = (int)TransactionType.Credit;
                }

                model.AccumulatedPayrollHeadList.Add(advance);
             }
             catch (Exception ex)
             {
                 throw ex;
             }

             return model;
        }

        public object FetchEmployeePayroll(EmployeeMonthlyAttendance empPayrollAttendance, GetEmployeeMonthlyPayrollQuery request)
        {
            PayrollModel model = new PayrollModel();

            try
            {
                model.GrossSalary = (double)empPayrollAttendance.GrossSalary;
                model.NetSalary = (double)empPayrollAttendance.NetSalary;
                model.SalaryPaid = model.NetSalary;
                model.Status = "Salary Approved";

                List<AccumulatedSalaryHeadDetail> salaryHeads = _dbContext.AccumulatedSalaryHeadDetail.Where(x=> x.IsDeleted == false 
                                                                && x.Month == request.Month &&
                                                                x.Year == DateTime.UtcNow.Year && x.EmployeeId == request.EmployeeId)
                model.AccumulatedPayrollHeadList.
            }
            catch (System.Exception)
            {
                
                throw;
            }

        }
    }
}