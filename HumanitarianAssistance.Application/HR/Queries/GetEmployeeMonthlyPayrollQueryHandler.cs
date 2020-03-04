using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
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
                FinancialYearDetail financialYear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.IsDefault == true);

                if(financialYear == null)
                {
                    throw new Exception(StaticResource.FinancialYearNotFound);
                }

                List<EmployeeAttendance> empPayrollAttendance = await _dbContext.EmployeeAttendance
                                                                                                  .Include(x => x.EmployeeDetails)
                                                                                                  .Include(x => x.EmployeeDetails.EmployeeProfessionalDetail)
                                                                                                  .Where(x => x.EmployeeId == request.EmployeeId &&
                                                                                                  x.Date.Month == request.Month && x.Date.Year == financialYear.StartDate.Year
                                                                                                  && x.IsDeleted == false && x.EmployeeDetails.IsDeleted == false && x.AttendanceTypeId == (int)AttendanceType.P).ToListAsync();

                if (!empPayrollAttendance.Any())
                {
                    throw new Exception(StaticResource.AttendanceNotFound);
                }

                var payroll = _dbContext.EmployeePayrollInfoDetail.FirstOrDefault(x => x.IsDeleted == false && x.EmployeeId == request.EmployeeId &&
                                                                                   x.Month == request.Month && x.Year == financialYear.StartDate.Year);

                if (payroll == null)
                {
                    object model = calculateEmployeePayroll(empPayrollAttendance, request, payroll, _dbContext);
                    response.Add("payroll", model);
                }
                else
                {
                    object model = FetchEmployeePayroll(empPayrollAttendance, request, payroll, _dbContext);
                    response.Add("payroll", model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        public static double getExchangeRate(int? currencyId, HumanitarianAssistanceDbContext _dbContext)
        {
            ExchangeRateDetail exchangeRateDetail = _dbContext.ExchangeRateDetail.OrderByDescending(x => x.Date).FirstOrDefault(x => x.FromCurrency == currencyId && x.ToCurrency == (int)Currency.AFG);

            if (exchangeRateDetail == null)
            {
                string currencyCode = _dbContext.CurrencyDetails.FirstOrDefault(x => x.IsDeleted == false && x.CurrencyId == currencyId).CurrencyCode;

                throw new Exception(StaticResource.ExchagneRateNotDefined);
            }

            return (double)exchangeRateDetail.Rate;
        }

        public static PayrollModel calculateEmployeePayroll(List<EmployeeAttendance> empPayrollAttendance, GetEmployeeMonthlyPayrollQuery request, EmployeePayrollInfoDetail payroll, HumanitarianAssistanceDbContext _dbContext)
        {
            PayrollModel model = new PayrollModel();
            List<AccumulatedPayrollHeads> accumulatedSalaryHeads = new List<AccumulatedPayrollHeads>();

            try
            {
                EmployeeBasicSalaryDetail basicPay = _dbContext.EmployeeBasicSalaryDetail.FirstOrDefault(x => x.IsDeleted == false && x.EmployeeId == request.EmployeeId);

                if (basicPay == null)
                {
                    throw new Exception("Basic pay not set for Employee");
                }


                EmployeeDetail employeeDetail = _dbContext.EmployeeDetail
                                                          .Include(x => x.EmployeeProfessionalDetail)
                                                          .FirstOrDefault(x => x.IsDeleted == false && x.EmployeeID == request.EmployeeId);

                                                          
                FinancialYearDetail financialYear = _dbContext.FinancialYearDetail.FirstOrDefault(x=> x.IsDeleted == false && x.IsDefault == true);

                if(!employeeDetail.EmployeeProfessionalDetail.AttendanceGroupId.HasValue)
                {
                    throw new Exception(StaticResource.AttendanceGroupNotSet + employeeDetail.EmployeeCode);
                }

                if(financialYear == null)
                {
                    throw new Exception(StaticResource.FinancialYearNotFound);
                }

                PayrollMonthlyHourDetail payrollHours = _dbContext.PayrollMonthlyHourDetail
                                                                  .FirstOrDefault(x => x.IsDeleted == false && x.OfficeId == employeeDetail.EmployeeProfessionalDetail.OfficeId
                                                                    && x.PayrollMonth == request.Month && x.PayrollYear == financialYear.StartDate.Year &&
                                                                    x.AttendanceGroupId == employeeDetail.EmployeeProfessionalDetail.AttendanceGroupId);

                if (payrollHours == null)
                {
                    throw new Exception(StaticResource.PayrollDailyHoursNotSet);
                }


                

                List<string> weeklyOff = _dbContext.HolidayWeeklyDetails.Where(x=> x.IsDeleted == false &&
                                                        x.FinancialYearId ==financialYear.FinancialYearId)
                                                        .Select(x=> x.Day).ToList();
                
                int weeklyOffDays = ParticularDayInMonth(new DateTime(financialYear.StartDate.Year, request.Month, 1), weeklyOff);

                int payableDaysInAMonth = DateTime.DaysInMonth(DateTime.UtcNow.Year, request.Month) -weeklyOffDays;


                int workingHours = payrollHours.OutTime.Value.Subtract(payrollHours.InTime.Value).Hours;

                double dBasicPayPerhour = basicPay.BasicSalary / (payableDaysInAMonth * workingHours);
                model.HourlyRate = dBasicPayPerhour;

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
                                                                            && x.Month == request.Month && x.Year == DateTime.UtcNow.Year && x.IsDeleted == false).ToList();

                double totalBonus = bonusAndFines.Where(x => x.TransactionTypeId == (int)TransactionType.Debit).Select(x => x.Amount).DefaultIfEmpty(0).Sum();
                double totalFine = bonusAndFines.Where(x => x.TransactionTypeId == (int)TransactionType.Credit).Select(x => x.Amount).DefaultIfEmpty(0).Sum();
                double dSalaryTax = 0;
                double dPension = 0;

                int workTimeHours = empPayrollAttendance.Select(x => Convert.ToInt32(x.TotalWorkTime)).DefaultIfEmpty(0).Sum();
                int workTimeMinutes = empPayrollAttendance.Select(x => x.WorkTimeMinutes).DefaultIfEmpty(0).Sum();
                int? overtimehours = empPayrollAttendance.Select(x => x.HoverTimeHours).DefaultIfEmpty(0).Sum();
                int overTimeMinutes = empPayrollAttendance.Select(x => x.OverTimeMinutes).DefaultIfEmpty(0).Sum();

                double convertMinutesToHours = ((double)(workTimeMinutes + overTimeMinutes) / 60d);
                model.GrossSalary = Math.Round((double)((dBasicPayPerhour * (workTimeHours + overtimehours + convertMinutesToHours) + totalBonus - totalFine) - (weeklyOffDays * dBasicPayPerhour *workingHours)), 2);

                //Calculate pension
                dPension = Math.Round(((double)(model.GrossSalary * pension.PensionRate) / 100), 2); // i.e. 4.5 % => 0.045


                if (model.GrossSalary > 5000)
                {

                    if (basicPay.CurrencyId != null)
                    {
                        double exchangeRate = GetEmployeeMonthlyPayrollQueryHandler.getExchangeRate(basicPay.CurrencyId, _dbContext);
                        dSalaryTax = Math.Round(Convert.ToDouble((StaticFunctions.SalaryCalculate(model.GrossSalary, exchangeRate))), 2);
                    }
                    else 
                    {
                        throw new Exception(StaticResource.EmployeePayrollCurrencyNotSet);
                    }

                }
                else
                {
                    dSalaryTax = 0;
                }

                //Add Accumulated Salary Head for Salary Tax
                GetEmployeeMonthlyPayrollQueryHandler.AddEmployeeSalaryHeads((int)AccumulatedSalaryHead.SalaryTax, dSalaryTax, "Salary Tax", (int)TransactionType.Credit, ref accumulatedSalaryHeads);
                //Add Accumulated Salary Head for pension
                GetEmployeeMonthlyPayrollQueryHandler.AddEmployeeSalaryHeads((int)AccumulatedSalaryHead.Pension, dPension, "Pension", (int)TransactionType.Credit, ref accumulatedSalaryHeads);


                AdvanceHistoryDetail xAdvanceHistory = _dbContext.AdvanceHistoryDetail.FirstOrDefault(x => x.IsDeleted == false
                                                                           && x.EmployeeId == request.EmployeeId && x.PaymentDate.Month == request.Month);

                double advanceAmount = xAdvanceHistory == null ? 0 : xAdvanceHistory.InstallmentPaid;

                //Add Accumulated Salary Head for Advance Amount
                GetEmployeeMonthlyPayrollQueryHandler.AddEmployeeSalaryHeads((int)AccumulatedSalaryHead.AdvanceRecovery, advanceAmount, "Advance Recovery", (int)TransactionType.Credit, ref accumulatedSalaryHeads);
                //Add Accumulated Salary Head for Capacity Building
                GetEmployeeMonthlyPayrollQueryHandler.AddEmployeeSalaryHeads((int)AccumulatedSalaryHead.CapacityBuilding, basicPay.CapacityBuildingAmount, "Capacity Building", (int)TransactionType.Credit, ref accumulatedSalaryHeads);
                //Add Accumulated Salary Head for Security
                GetEmployeeMonthlyPayrollQueryHandler.AddEmployeeSalaryHeads((int)AccumulatedSalaryHead.Security, basicPay.SecurityAmount, "Security", (int)TransactionType.Credit, ref accumulatedSalaryHeads);
                //Add Accumulated Salary Head for Gross Salary
                GetEmployeeMonthlyPayrollQueryHandler.AddEmployeeSalaryHeads((int)AccumulatedSalaryHead.GrossSalary, model.GrossSalary, "Gross Salary", (int)TransactionType.Credit, ref accumulatedSalaryHeads);

                //Net Salary  = (Gross + Allowances) - Deductions
                model.NetSalary = Math.Round((double)(model.GrossSalary  - dSalaryTax - dPension - advanceAmount - basicPay.SecurityAmount - basicPay.CapacityBuildingAmount), 2);
                model.SalaryPaid = model.NetSalary;

                model.AccumulatedPayrollHeadList = accumulatedSalaryHeads;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return model;
        }

        public static void AddEmployeeSalaryHeads(int id, double amount, string headName, int transactionType, ref List<AccumulatedPayrollHeads> payrollHeads)
        {

            AccumulatedPayrollHeads accumulatedSalaryHead = new AccumulatedPayrollHeads
            {
                Id = id,
                Amount = amount,
                PayrollHeadName = headName,
                TransactionType = transactionType
            };

            payrollHeads.Add(accumulatedSalaryHead);
        }

        public static PayrollModel FetchEmployeePayroll(List<EmployeeAttendance> empPayrollAttendance, GetEmployeeMonthlyPayrollQuery request, EmployeePayrollInfoDetail payroll, HumanitarianAssistanceDbContext _dbContext)
        {
            PayrollModel model = new PayrollModel();

            try
            {
                EmployeeDetail employeeDetail = _dbContext.EmployeeDetail
                                                          .Include(x => x.EmployeeProfessionalDetail)
                                                          .FirstOrDefault(x => x.IsDeleted == false && x.EmployeeID == request.EmployeeId);

                EmployeeBasicSalaryDetail basicPay = _dbContext.EmployeeBasicSalaryDetail.FirstOrDefault(x => x.IsDeleted == false && x.EmployeeId == request.EmployeeId);

                model.GrossSalary = payroll.GrossSalary;
                model.NetSalary = payroll.NetSalary;
                model.SalaryPaid = model.NetSalary;
                model.HourlyRate = payroll.HourlyRate;
                model.Status = "Salary Approved";
                model.IsSalaryApproved = true;

                // PayrollMonthlyHourDetail payrollHours = _dbContext.PayrollMonthlyHourDetail
                //                                                                     .FirstOrDefault(x => x.IsDeleted == false && x.OfficeId == employeeDetail.EmployeeProfessionalDetail.OfficeId
                //                                                                     && x.PayrollMonth == request.Month && x.PayrollYear == DateTime.UtcNow.Year && x.AttendanceGroupId == employeeDetail.EmployeeProfessionalDetail.AttendanceGroupId);

                // if (payrollHours == null)
                // {
                //     throw new Exception(StaticResource.PayrollDailyHoursNotSet);
                // }

                // int workingHours = payrollHours.OutTime.Value.Subtract(payrollHours.InTime.Value).Hours;

                //  double dBasicPayPerhour = Math.Round(basicPay.BasicSalary / (DateTime.DaysInMonth(DateTime.UtcNow.Year, request.Month) * workingHours), 2);
                //  model.HourlyRate = dBasicPayPerhour;

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


        public static int ParticularDayInMonth(DateTime time, List<string> dayNames)
        {
            int weekDaysInAMonth =0;

            int daysInAMonth = DateTime.DaysInMonth(time.Year, time.Month);

            for(int i=1; i<= daysInAMonth; i++)
            {
                DateTime date = new DateTime(time.Year, time.Month, i);
                string dayName= CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(date.DayOfWeek);
                if(dayNames.Contains(dayName))
                {
                    weekDaysInAMonth++;
                }
            }
            return weekDaysInAMonth;
        }
    }
}