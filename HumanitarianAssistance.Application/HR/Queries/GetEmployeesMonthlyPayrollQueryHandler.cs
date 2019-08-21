using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeesMonthlyPayrollQueryHandler: IRequestHandler<GetEmployeesMonthlyPayrollQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetEmployeesMonthlyPayrollQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetEmployeesMonthlyPayrollQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                ICollection<EmployeeMonthlyAttendance> empPayrollAttendanceList = await _dbContext.EmployeeMonthlyAttendance
                                                                                                  .Include(x => x.EmployeeDetails)
                                                                                                  .Where(x => x.OfficeId == request.OfficeId &&
                                                                                                  x.Month == request.Month && x.Year == request.Year 
                                                                                                  && x.IsDeleted == false && x.IsApproved == false 
                                                                                                  && x.EmployeeDetails.IsDeleted== false)
                                                                                                  .ToListAsync();

                //Note: default 0.045 i.e. (4.5 %)
                double? pensionRate = _dbContext.EmployeePensionRate.FirstOrDefault(x => x.IsDefault == true && x.IsDeleted == false)?.PensionRate;

                List<EmployeeMonthlyPayrollModel> payrollFinal = new List<EmployeeMonthlyPayrollModel>();

                foreach (EmployeeMonthlyAttendance payrollAttendance in empPayrollAttendanceList)
                {
                    List<EmployeePayrollModel> payrollDetail = new List<EmployeePayrollModel>();

                    var payroll = await _dbContext.EmployeePayroll.Include(x => x.SalaryHeadDetails).Where(x => x.EmployeeID == payrollAttendance.EmployeeId && x.IsDeleted == false).ToListAsync();

                    if (payroll.Any(x => x.AccountNo == null))
                    {
                        throw new Exception($"Payroll details not set for Employee code: {payrollAttendance.EmployeeDetails.EmployeeCode}");
                    }

                    payrollDetail = payroll.Select(x => new EmployeePayrollModel
                    {
                        PayrollId = x.PayrollId,
                        CurrencyId = x.CurrencyId ?? 0,
                        EmployeeId = x.EmployeeID,
                        HeadTypeId = x.SalaryHeadDetails.HeadTypeId,
                        IsDeleted = x.IsDeleted,
                        MonthlyAmount = x.MonthlyAmount ?? 0,
                        PaymentType = 2, //hourly
                        PensionRate = pensionRate != null ? pensionRate : DefaultValues.DefaultPensionRate,
                        SalaryHeadId = x.SalaryHeadId ?? 0,
                        SalaryHeadType = x.SalaryHeadDetails.HeadTypeId == (int)SalaryHeadType.ALLOWANCE ? "Allowance" : x.SalaryHeadDetails.HeadTypeId == (int)SalaryHeadType.DEDUCTION ? "Deduction" : x.SalaryHeadDetails.HeadTypeId == (int)SalaryHeadType.GENERAL ? "General" : "",
                        SalaryHead = x.SalaryHeadDetails.HeadName,
                        AccountNo = x.AccountNo,
                        TransactionTypeId = x.TransactionTypeId
                    }).OrderBy(x => x.TransactionTypeId).ThenBy(x => x.SalaryHeadType).ToList();

                    if (payrollDetail.Count > 0)
                    {
                        if (payrollAttendance.GrossSalary == 0 || payrollAttendance.GrossSalary == null)
                        {
                            int iCurrencyId = payrollDetail.FirstOrDefault(x => x.HeadTypeId == 3).CurrencyId;

                            EmployeeMonthlyPayrollModel obj = new EmployeeMonthlyPayrollModel();

                            obj.EmployeeId = payrollAttendance.EmployeeId.Value;
                            obj.EmployeeCode = payrollAttendance.EmployeeDetails.EmployeeCode;
                            obj.EmployeeName = payrollAttendance.EmployeeDetails.EmployeeName;
                            obj.PaymentType = 2;
                            obj.AbsentDays = payrollAttendance.AbsentHours == null ? 0 : payrollAttendance.AbsentHours.Value;
                            obj.LeaveDays = payrollAttendance.LeaveHours == null ? 0 : payrollAttendance.LeaveHours.Value;
                            obj.PresentDays = payrollAttendance.AttendanceHours == null ? 0 : payrollAttendance.AttendanceHours.Value + Convert.ToInt32(Math.Floor((float)payrollAttendance.AttendanceMinutes / 60f));
                            obj.LeaveHours = payrollAttendance.LeaveHours == null ? 0 : payrollAttendance.LeaveHours.Value;
                            obj.WorkingDays = payrollAttendance.AttendanceHours == null ? 0 : payrollAttendance.AttendanceHours.Value + Convert.ToInt32(Math.Floor((float)payrollAttendance.AttendanceMinutes / 60f));
                            obj.TotalWorkHours = payrollAttendance.TotalDuration == null ? 0 : payrollAttendance.TotalDuration.Value;
                            obj.OverTimeHours = payrollAttendance.OvertimeHours == null ? 0 : payrollAttendance.OvertimeHours.Value + Math.Floor(((float)payrollAttendance.OverTimeMinutes / 60f));
                            obj.IsAdvanceRecovery = payrollAttendance.IsAdvanceRecovery;
                            obj.AdvanceAmount = payrollAttendance.AdvanceAmount;
                            obj.CurrencyId = payrollDetail.FirstOrDefault().CurrencyId;
                            obj.TotalAllowance = payrollDetail.Where(x => x.HeadTypeId == (int)SalaryHeadType.ALLOWANCE).Sum(s => s.MonthlyAmount);
                            obj.TotalDeduction = payrollDetail.Where(x => x.HeadTypeId == (int)SalaryHeadType.DEDUCTION).Sum(s => s.MonthlyAmount);
                            obj.TotalGeneralAmount = payrollDetail.Where(x => x.HeadTypeId == (int)SalaryHeadType.GENERAL).Sum(s => s.MonthlyAmount);

                            if (obj.TotalGeneralAmount == 0)
                            {
                                throw new Exception($"Basic Pay not defined for Employee Code-{payrollAttendance.EmployeeDetails.EmployeeCode}");
                            }

                            double convertMinutesToHours = Math.Round(((double)(payrollAttendance.OverTimeMinutes + payrollAttendance.AttendanceMinutes) / 60d),2);
                            obj.GrossSalary = Math.Round((double)(obj.TotalGeneralAmount * (payrollAttendance.AttendanceHours.Value + obj.LeaveHours + payrollAttendance.OvertimeHours.Value + convertMinutesToHours) + obj.TotalAllowance),2);
                            obj.PensionAmount = Math.Round(((double)(obj.GrossSalary * payrollDetail.FirstOrDefault().PensionRate) / 100),2); // i.e. 4.5 % => 0.045

                            // eliminate hours and only show minutes if minutes is 60 we already added them to overtime hours so minutes = 0

                            decimal overtimeHour = Math.Round((decimal)((float)payrollAttendance.OverTimeMinutes / 60f), 2);

                            obj.OvertimeMinutes = Convert.ToInt32((overtimeHour- Math.Truncate(overtimeHour)) * 60);
                            // eliminate hours and only show minutes if minutes is 60 we already added them to AttendanceHours so minutes = 0

                            decimal attendanceMinutes = Math.Round((decimal)((float)payrollAttendance.AttendanceMinutes / 60f), 2);

                            obj.WorkingMinutes= Convert.ToInt32((attendanceMinutes - Math.Truncate(attendanceMinutes)) * 60); 

                            if (obj.GrossSalary > 5000)
                            {
                                double? dExchangeRate1 = 0.0;
                                ExchangeRateDetail exchangeRateDetail = _dbContext.ExchangeRateDetail.OrderByDescending(x => x.Date).FirstOrDefault(x => x.FromCurrency == iCurrencyId && x.ToCurrency == (int)Currency.AFG);

                                if (exchangeRateDetail == null)
                                {
                                    string currencyCode = _dbContext.CurrencyDetails.FirstOrDefault(x => x.IsDeleted == false && x.CurrencyId == iCurrencyId).CurrencyCode;

                                    throw new Exception($"Exchange Rate Not Defined from {currencyCode} to AFG");
                                }
                                else
                                {
                                    dExchangeRate1 = (double)exchangeRateDetail.Rate;
                                }

                                obj.SalaryTax = obj.SalaryTax == null ? 0 : obj.SalaryTax;
                                obj.SalaryTax = Math.Round(Convert.ToDouble((StaticFunctions.SalaryCalculate(obj.GrossSalary.Value, dExchangeRate1.Value))), 2);
                            }
                            else
                            {
                                obj.SalaryTax = 0;
                            }

                            //Net Salary  = (Gross + Allowances) - Deductions
                            obj.NetSalary = Math.Round((double)(obj.GrossSalary - (obj.TotalDeduction != null ? obj.TotalDeduction : 0) - (obj.SalaryTax != null ? obj.SalaryTax : 0) - payrollAttendance.AdvanceRecoveryAmount - (obj.PensionAmount != null ? obj.PensionAmount : 0)),2);

                            obj.EmployeePayrollList.AddRange(payrollDetail);

                            payrollFinal.Add(obj);

                            Advances xAdvances = await _dbContext.Advances.FirstOrDefaultAsync(x => x.IsDeleted == false && x.IsApproved == true
                                                                           && x.EmployeeId == payrollAttendance.EmployeeId && x.OfficeId == payrollAttendance.OfficeId
                                                                           && x.AdvanceDate < DateTime.Now && x.IsDeducted == false);

                            if (xAdvances != null)
                            {
                                if (xAdvances.RecoveredAmount == 0)
                                {

                                    if (xAdvances.NumberOfInstallments == 0)
                                    {
                                        xAdvances.NumberOfInstallments = 1;
                                    }

                                    obj.AdvanceRecoveryAmount = Math.Round((Convert.ToDouble(xAdvances.AdvanceAmount / xAdvances.NumberOfInstallments ?? 1)), 2);
                                    obj.AdvanceAmount = xAdvances.AdvanceAmount;
                                    obj.IsAdvanceApproved = xAdvances.IsApproved;
                                }
                                else
                                {
                                    Double iBalanceAmount = xAdvances.AdvanceAmount - xAdvances.RecoveredAmount;
                                    obj.AdvanceRecoveryAmount = Math.Round((Convert.ToDouble(iBalanceAmount / xAdvances.NumberOfInstallments)),2);
                                    obj.IsAdvanceApproved = xAdvances.IsApproved;
                                    obj.AdvanceAmount = iBalanceAmount;
                                }
                            }
                            else
                            {
                                obj.AdvanceRecoveryAmount = 0;
                                obj.AdvanceAmount = 0;
                                obj.IsAdvanceApproved = false;
                            }
                        }
                        else
                        {
                            EmployeeMonthlyPayrollModel obj = new EmployeeMonthlyPayrollModel();
                            obj.AbsentDays = payrollAttendance.AbsentHours == null ? 0 : payrollAttendance.AbsentHours.Value;
                            obj.OverTimeHours = payrollAttendance.OvertimeHours;
                            obj.AdvanceAmount = payrollAttendance.AdvanceAmount;
                            obj.AdvanceRecoveryAmount = payrollAttendance.AdvanceRecoveryAmount;
                            obj.CurrencyId = payrollDetail[0].CurrencyId;
                            obj.EmployeeId = payrollAttendance.EmployeeId.Value;
                            obj.EmployeeCode = payrollAttendance.EmployeeDetails.EmployeeCode;
                            obj.EmployeeName = payrollAttendance.EmployeeDetails.EmployeeName;
                            obj.GrossSalary = payrollAttendance.GrossSalary == null ? 0 : payrollAttendance.GrossSalary;
                            obj.IsAdvanceApproved = payrollAttendance.IsAdvanceApproved;
                            obj.IsAdvanceRecovery = payrollAttendance.IsAdvanceRecovery;
                            obj.LeaveDays = payrollAttendance.LeaveHours == null ? 0 : payrollAttendance.LeaveHours.Value;
                            obj.LeaveHours = payrollAttendance.LeaveHours == null ? 0 : payrollAttendance.LeaveHours.Value;
                            obj.NetSalary = payrollAttendance.NetSalary == null ? 0 : payrollAttendance.NetSalary;
                            obj.PaymentType = 2;
                            obj.PensionAmount = payrollAttendance.PensionAmount == null ? 0 : payrollAttendance.PensionAmount;
                            obj.PresentDays = payrollAttendance.AttendanceHours == null ? 0 : payrollAttendance.AttendanceHours.Value;
                            obj.SalaryTax = payrollAttendance.SalaryTax == null ? 0 : payrollAttendance.SalaryTax.Value;
                            obj.TotalAllowance = payrollAttendance.TotalAllowance == null ? 0 : payrollAttendance.TotalAllowance.Value;
                            obj.TotalDeduction = payrollAttendance.TotalDeduction == null ? 0 : payrollAttendance.TotalDeduction.Value;
                            obj.TotalGeneralAmount = payrollAttendance.TotalGeneralAmount == null ? 0 : payrollAttendance.TotalGeneralAmount.Value;
                            obj.WorkingDays = payrollAttendance.AttendanceHours == null ? 0 : payrollAttendance.AttendanceHours.Value;
                            obj.TotalWorkHours = payrollAttendance.TotalDuration == null ? 0 : payrollAttendance.TotalDuration.Value;
                            obj.OvertimeMinutes= payrollAttendance.OverTimeMinutes; // eliminate hours and only show minutes
                            obj.WorkingMinutes= payrollAttendance.AttendanceMinutes; // eliminate hours and only show minutes

                            obj.EmployeePayrollList.AddRange(payrollDetail.Where(x => x.EmployeeId == obj.EmployeeId));
                            payrollFinal.Add(obj);
                        }
                    }
                    else
                    {
                        throw new Exception($"Employee Payroll not defined for Employee Code-{payrollAttendance.EmployeeDetails.EmployeeCode}");
                    }
                }

                response.data.EmployeeMonthlyPayrolllist = payrollFinal;
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