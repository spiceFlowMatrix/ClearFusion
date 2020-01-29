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
                List<EmployeeAttendance> empPayrollAttendance = await _dbContext.EmployeeAttendance
                                                                                                  .Include(x => x.EmployeeDetails)
                                                                                                  .Include(x => x.EmployeeDetails.EmployeeProfessionalDetail)
                                                                                                  .Where(x => x.EmployeeId == request.EmployeeId &&
                                                                                                  x.InTime.Value.Month == request.Month && x.InTime.Value.Year == DateTime.UtcNow.Year
                                                                                                  && x.IsDeleted == false && x.EmployeeDetails.IsDeleted == false).ToListAsync();

                if (!empPayrollAttendance.Any())
                {
                    throw new Exception(StaticResource.AttendanceNotFound);
                }

                var payroll = _dbContext.EmployeePayrollInfoDetail.FirstOrDefault(x => x.IsDeleted == false && x.EmployeeId == request.EmployeeId &&
                                                                                   x.Month == request.Month && x.Year == DateTime.UtcNow.Year);

                if (payroll == null)
                {
                    object model = calculateEmployeePayroll(empPayrollAttendance, request, payroll);
                    response.Add("payroll", model);
                }
                else
                {
                    object model = FetchEmployeePayroll(empPayrollAttendance, request, payroll);
                    response.Add("payroll", model);
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

        public object calculateEmployeePayroll(List<EmployeeAttendance> empPayrollAttendance, GetEmployeeMonthlyPayrollQuery request, EmployeePayrollInfoDetail payroll)
        {
            PayrollModel model = new PayrollModel();

            try
            {
                EmployeeBasicSalaryDetail basicPay = _dbContext.EmployeeBasicSalaryDetail.FirstOrDefault(x => x.IsDeleted == false && x.EmployeeId == request.EmployeeId);

                double dBasicPayPerhour = basicPay.BasicSalary / DateTime.DaysInMonth(DateTime.UtcNow.Year, request.Month);

                if (basicPay == null)
                {
                    throw new Exception("Basic pay not set for Employee");
                }

                EmployeeDetail employeeDetail = _dbContext.EmployeeDetail
                                                          .Include(x => x.EmployeeProfessionalDetail)
                                                          .FirstOrDefault(x => x.IsDeleted == false && x.EmployeeID == request.EmployeeId);

                PayrollMonthlyHourDetail payrollHours = _dbContext.PayrollMonthlyHourDetail
                                                                                    .FirstOrDefault(x => x.IsDeleted == false && x.OfficeId == employeeDetail.EmployeeProfessionalDetail.OfficeId
                                                                                    && x.PayrollMonth == request.Month && x.PayrollYear == DateTime.UtcNow.Year && x.AttendanceGroupId == employeeDetail.EmployeeProfessionalDetail.AttendanceGroupId);

                if (payrollHours == null)
                {
                    throw new Exception(StaticResource.PayrollDailyHoursNotSet);
                }

                //Note: default 0.045 i.e. (4.5 %)
                var pension = _dbContext.EmployeePensionRate.FirstOrDefault(x => x.IsDefault == true && x.IsDeleted == false);

                if (pension == null)
                {
                    throw new Exception("Pension rate not set");
                }

                //EmployeePayroll payrollBasicSalaryAndCurrency = _dbContext.EmployeePayroll.FirstOrDefault(x => x.HeadTypeId == (int)SalaryHeadType.GENERAL);

                model.Status = "Salary Unapproved";
                model.IsSalaryApproved = false;

                var bonusAndFines = _dbContext.EmployeeBonusFineSalaryHead.Where(x => x.EmployeeId == request.EmployeeId
                                                                            && x.Month == request.Month && x.Year == DateTime.UtcNow.Year).ToList();

                double totalBonus = bonusAndFines.Where(x => x.TransactionTypeId == (int)TransactionType.Debit).Select(x => x.Amount).DefaultIfEmpty(0).Sum();
                double totalFine = bonusAndFines.Where(x => x.TransactionTypeId == (int)TransactionType.Credit).Select(x => x.Amount).DefaultIfEmpty(0).Sum();
                double dSalaryTax = 0;
                double dPension = 0;

                int workTimeHours = empPayrollAttendance.Select(x => Convert.ToInt32(x.TotalWorkTime)).DefaultIfEmpty(0).Sum();
                int workTimeMinutes = empPayrollAttendance.Select(x => x.WorkTimeMinutes).DefaultIfEmpty(0).Sum();
                int? overtimehours = empPayrollAttendance.Select(x => x.HoverTimeHours).DefaultIfEmpty(0).Sum();
                int overTimeMinutes = empPayrollAttendance.Select(x => x.OverTimeMinutes).DefaultIfEmpty(0).Sum();

                double convertMinutesToHours = ((double)(workTimeMinutes + overTimeMinutes) / 60d);
                model.GrossSalary = Math.Round((double)(dBasicPayPerhour * (workTimeHours + overtimehours + convertMinutesToHours) + totalBonus), 2);
                AccumulatedPayrollHeads pensionHead = new AccumulatedPayrollHeads
                {
                    Id = (int)AccumulatedSalaryHead.Pension,
                    Amount = dPension = Math.Round(((double)(model.GrossSalary * pension.PensionRate) / 100), 2), // i.e. 4.5 % => 0.045
                    PayrollHeadName = "Pension",
                    TransactionType = (int)TransactionType.Credit
                };

                model.AccumulatedPayrollHeadList.Add(pensionHead);

                AccumulatedPayrollHeads grossSalary = new AccumulatedPayrollHeads
                {
                    Id = (int)AccumulatedSalaryHead.GrossSalary,
                    Amount = model.GrossSalary,
                    PayrollHeadName = "Gross Salary",
                    TransactionType = (int)TransactionType.Credit
                };

                model.AccumulatedPayrollHeadList.Add(grossSalary);

                if (model.GrossSalary > 5000)
                {
                    double exchangeRate = getExchangeRate(basicPay.CurrencyId);

                    AccumulatedPayrollHeads salaryTax = new AccumulatedPayrollHeads
                    {
                        Id = (int)AccumulatedSalaryHead.SalaryTax,
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
                        Id = (int)AccumulatedSalaryHead.SalaryTax,
                        Amount = dSalaryTax = 0,
                        PayrollHeadName = "Salary Tax",
                        TransactionType = (int)TransactionType.Credit
                    };

                    model.AccumulatedPayrollHeadList.Add(salaryTax);
                }

                AdvanceHistoryDetail xAdvances = _dbContext.AdvanceHistoryDetail.FirstOrDefault(x => x.IsDeleted == false
                                                                           && x.EmployeeId == request.EmployeeId && x.PaymentDate.Month == request.Month);

                double advanceAmount = xAdvances == null ? 0 : xAdvances.InstallmentPaid;

                AccumulatedPayrollHeads advance = new AccumulatedPayrollHeads();
                advance.Id = (int)AccumulatedSalaryHead.AdvanceRecovery;
                advance.Amount = advanceAmount;
                advance.PayrollHeadName = "Advance Recovery";
                advance.TransactionType = (int)TransactionType.Credit;

                //Net Salary  = (Gross + Allowances) - Deductions
                model.NetSalary = Math.Round((double)(model.GrossSalary - totalFine - dSalaryTax - dPension - advanceAmount), 2);
                model.SalaryPaid = model.NetSalary;

                model.AccumulatedPayrollHeadList.Add(advance);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return model;
        }

        public object FetchEmployeePayroll(List<EmployeeAttendance> empPayrollAttendance, GetEmployeeMonthlyPayrollQuery request, EmployeePayrollInfoDetail payroll)
        {
            PayrollModel model = new PayrollModel();

            try
            {
                model.GrossSalary = payroll.GrossSalary;
                model.NetSalary = payroll.NetSalary;
                model.SalaryPaid = model.NetSalary;
                model.Status = "Salary Approved";
                model.IsSalaryApproved = true;

                List<AccumulatedSalaryHeadDetail> salaryHeads = _dbContext.AccumulatedSalaryHeadDetail.Where(x => x.IsDeleted == false &&
                                                                x.Month == request.Month &&
                                                                x.Year == DateTime.UtcNow.Year && x.EmployeeId == request.EmployeeId).ToList();

                foreach (var item in salaryHeads)
                {
                    SavedAccumulatedPayrollHeads salaryHead = new SavedAccumulatedPayrollHeads
                    {
                        Id = item.SalaryComponentId,
                        SalaryAllowance = item.SalaryAllowance,
                        SalaryDeduction = item.SalaryDeduction,
                        PayrollHeadName = ((AccumulatedSalaryHead)item.SalaryComponentId).ToString()
                    };
                    model.SavedAccumulatedPayrollHeadList.Add(salaryHead);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return model;
        }
    }
}