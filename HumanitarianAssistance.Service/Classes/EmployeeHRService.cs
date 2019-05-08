using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Entities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Service.Classes
{
    public class EmployeeHRService : IEmployeeHR
    {

        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        IHostingEnvironment _env;
        public EmployeeHRService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager, IHostingEnvironment env)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
            this._env = env;
        }

        /// <summary>
        /// Adding Employee Attendance Details 
        /// </summary>
        /// <param name="modellist">Contain Employee Attendance Details</param>
        /// <param name="UserId">Contain UserId Updating Attendance</param>
        /// <returns>Success Or Failure</returns>
        public async Task<APIResponse> AddEmployeeAttendanceDetails(List<EmployeeAttendanceModel> modellist, string UserId)
        {

            DateTime OfficeInTime = new DateTime();
            DateTime OfficeOutTime = new DateTime();
            APIResponse response = new APIResponse();
            try
            {
                var existrecord = await Task.Run(() =>
                    _uow.EmployeeAttendanceRepository.FindAllAsync(x => x.Date.Date == modellist[0].Date.Date).Result.ToList()
                );

                //gets the total working hours in a day for an office
                PayrollMonthlyHourDetail payrollMonthlyHourDetail = _uow.PayrollMonthlyHourDetailRepository.FindAll(x => x.OfficeId == modellist.FirstOrDefault().OfficeId && x.PayrollYear == modellist.FirstOrDefault().Date.Year && x.PayrollMonth == modellist.FirstOrDefault().Date.Month).FirstOrDefault();
                int? officeDailyHour = payrollMonthlyHourDetail.Hours;
                int? officeMonthlyHour = payrollMonthlyHourDetail.WorkingTime;


                if (officeDailyHour != null && officeMonthlyHour != null)
                {

                    //int? defaulthours = 0;

                    //uncomment when we have all offices hour detail
                    //var DefaultHourDetail = await _uow.PayrollMonthlyHourDetailRepository.FindAsync(x => x.IsDeleted == false && x.OfficeId == modellist[0].OfficeId);

                    //var DefaultHourDetail = await _uow.PayrollMonthlyHourDetailRepository.FindAsync(x => x.IsDeleted == false);

                    var financiallist = await _uow.FinancialYearDetailRepository.FindAsync(x => x.IsDefault == true);

                    foreach (var list in modellist)
                    {
                        TimeSpan? totalworkhour;
                        TimeSpan? totalovertime;
                        int? overtime = 0, workingHours = 0;

                        var isemprecord = existrecord.Where(x => x.EmployeeId == list.EmployeeId && x.Date.Date == list.Date.Date).ToList();
                        totalworkhour = list.OutTime - list.InTime;

                        if (isemprecord.Count == 0)
                        {

                            if (totalworkhour.ToString() == "00:00:00" || list.AttendanceTypeId == (int)AttendanceType.A)
                            {
                                list.AttendanceTypeId = 2;
                                list.InTime = list.Date;
                                list.OutTime = list.Date;
                                totalworkhour = list.Date.Date - list.Date.Date;
                            }

                            OfficeInTime = new DateTime(list.InTime.Value.Year, list.InTime.Value.Month, list.InTime.Value.Day, payrollMonthlyHourDetail.InTime.Value.Hour, payrollMonthlyHourDetail.InTime.Value.Minute, payrollMonthlyHourDetail.InTime.Value.Second);
                            OfficeOutTime = new DateTime(list.OutTime.Value.Year, list.OutTime.Value.Month, list.OutTime.Value.Day, payrollMonthlyHourDetail.OutTime.Value.Hour, payrollMonthlyHourDetail.OutTime.Value.Minute, payrollMonthlyHourDetail.OutTime.Value.Second);

                            if (list.InTime < OfficeInTime)
                            {
                                totalovertime = OfficeInTime - list.InTime;
                                overtime += totalovertime.Value.Hours;
                                if (list.OutTime <= OfficeOutTime)
                                {
                                    totalworkhour = list.OutTime - OfficeInTime;
                                    workingHours += totalworkhour.Value.Hours;
                                }
                                if (list.OutTime > OfficeOutTime)
                                {
                                    totalovertime = list.OutTime - OfficeOutTime;
                                    overtime += totalovertime.Value.Hours;
                                    totalworkhour = OfficeOutTime - OfficeInTime;
                                    workingHours += totalworkhour.Value.Hours;
                                }

                                list.TotalWorkTime = workingHours.ToString();
                                list.HoverTimeHours = overtime;
                            }

                            else if (list.InTime >= OfficeInTime)
                            {
                                if (list.OutTime <= OfficeOutTime)
                                {
                                    totalworkhour = list.OutTime - list.InTime;
                                    workingHours += totalworkhour.Value.Hours;
                                }
                                if (list.OutTime > OfficeOutTime)
                                {
                                    totalovertime = list.OutTime - OfficeOutTime;
                                    overtime += totalovertime.Value.Hours;
                                    totalworkhour = OfficeOutTime - list.InTime;
                                    workingHours += totalworkhour.Value.Hours;
                                }

                                list.TotalWorkTime = workingHours.ToString();
                                list.HoverTimeHours = overtime;

                            }
                            else
                            {
                                list.TotalWorkTime = workingHours.ToString();
                                list.HoverTimeHours = overtime;
                            }

                            list.FinancialYearId = financiallist.FinancialYearId;
                            list.CreatedById = UserId;
                            list.CreatedDate = DateTime.UtcNow;
                            list.IsDeleted = false;
                            EmployeeAttendance obj = _mapper.Map<EmployeeAttendance>(list);
                            await _uow.EmployeeAttendanceRepository.AddAsyn(obj);
                            await _uow.SaveAsync();
                        }

                        EmployeeMonthlyAttendance xEmployeeMonthlyAttendanceRecord = await _uow.EmployeeMonthlyAttendanceRepository.FindAsync(x => x.EmployeeId == list.EmployeeId && x.Month == list.Date.Month && x.Year == list.Date.Year && x.IsDeleted == false);

                        EmployeeMonthlyAttendance xEmployeeMonthlyAttendance = new EmployeeMonthlyAttendance();

                        //If Employee record is present in monthly attendance table then 
                        if (xEmployeeMonthlyAttendanceRecord != null)
                        {

                            //if Employee is absent without any leave
                            if (totalworkhour.ToString() == "00:00:00" || list.AttendanceTypeId == (int)AttendanceType.A)
                            {
                                xEmployeeMonthlyAttendance.AbsentHours = xEmployeeMonthlyAttendance.AbsentHours == null ? 0 : xEmployeeMonthlyAttendance.AbsentHours;
                                xEmployeeMonthlyAttendance.AbsentHours += officeDailyHour;
                            }

                            //update total attendance hours
                            if (workingHours != 0 && overtime == 0)
                            {
                                xEmployeeMonthlyAttendanceRecord.AttendanceHours += totalworkhour.Value.Hours;

                            }
                            //update total attendance hours and also add overtime hours
                            else if (workingHours != 0 && overtime != 0)
                            {
                                xEmployeeMonthlyAttendanceRecord.AttendanceHours += totalworkhour.Value.Hours;
                                xEmployeeMonthlyAttendanceRecord.OvertimeHours = xEmployeeMonthlyAttendanceRecord.OvertimeHours ?? 0;
                                xEmployeeMonthlyAttendanceRecord.OvertimeHours += overtime;
                            }

                            //updating employee monthly attendance record
                            await _uow.EmployeeMonthlyAttendanceRepository.UpdateAsyn(xEmployeeMonthlyAttendanceRecord);
                            await _uow.SaveAsync();
                        }
                        else// if employee monthly attendance record does not exists then add a record
                        {
                            int monthDays = GetMonthDays(modellist.FirstOrDefault().Date.Month, modellist.FirstOrDefault().Date.Year);

                            //if employee is absent without any leave
                            if (totalworkhour.ToString() == "00:00:00" || list.AttendanceTypeId == (int)AttendanceType.A)
                            {
                                xEmployeeMonthlyAttendance.IsDeleted = false;
                                xEmployeeMonthlyAttendance.EmployeeId = list.EmployeeId;
                                xEmployeeMonthlyAttendance.Month = list.Date.Month;
                                xEmployeeMonthlyAttendance.Year = list.Date.Year;
                                xEmployeeMonthlyAttendance.AttendanceHours = 0;
                                xEmployeeMonthlyAttendance.OvertimeHours = 0;
                                xEmployeeMonthlyAttendance.AbsentHours = officeDailyHour;
                                xEmployeeMonthlyAttendance.OfficeId = list.OfficeId;
                                xEmployeeMonthlyAttendance.TotalDuration = officeMonthlyHour;
                            }
                            else
                            {
                                xEmployeeMonthlyAttendance.IsDeleted = false;
                                xEmployeeMonthlyAttendance.EmployeeId = list.EmployeeId;
                                xEmployeeMonthlyAttendance.Month = list.Date.Month;
                                xEmployeeMonthlyAttendance.Year = list.Date.Year;
                                xEmployeeMonthlyAttendance.AttendanceHours = workingHours;
                                xEmployeeMonthlyAttendance.OvertimeHours = overtime;
                                xEmployeeMonthlyAttendance.OfficeId = list.OfficeId;
                                xEmployeeMonthlyAttendance.TotalDuration = officeMonthlyHour;
                            }

                            Advances xAdvances = await _uow.GetDbContext().Advances.Where(x => x.IsDeleted == false && x.IsApproved == true
                                                                            && x.EmployeeId == list.EmployeeId && x.OfficeId == list.OfficeId && x.IsDeducted == false
                                                                            && x.AdvanceDate <= DateTime.Now).FirstOrDefaultAsync();
                            if (xAdvances != null)
                            {
                                xEmployeeMonthlyAttendance.AdvanceId = xAdvances.AdvancesId;
                                xEmployeeMonthlyAttendance.IsAdvanceApproved = xAdvances.IsApproved;
                                xEmployeeMonthlyAttendance.AdvanceAmount = xAdvances.AdvanceAmount - xAdvances.RecoveredAmount;
                            }

                            await _uow.EmployeeMonthlyAttendanceRepository.AddAsyn(xEmployeeMonthlyAttendance);
                        }
                    }

                    //If Employee is not present then check the leave table and update leave record accordingly
                    List<EmployeeApplyLeave> xEmployeeLeaveDetailList = _uow.GetDbContext().EmployeeApplyLeave
                        .Include(x => x.EmployeeDetails.EmployeeProfessionalDetail)
                                                                    .Where(x => x.FromDate.Date >= modellist.FirstOrDefault().Date.Date &&
                                                                                x.ToDate.Date <= modellist.FirstOrDefault().Date.Date &&
                                                                                x.EmployeeDetails.EmployeeProfessionalDetail.OfficeId == modellist.FirstOrDefault().OfficeId
                                                                                && x.IsDeleted == false)
                                                                    .ToList();

                    //If leave Exists then check the status of leave if approved then add the working hours of the day in attendance hours
                    if (xEmployeeLeaveDetailList.Count != 0)
                    {
                        int monthDays = GetMonthDays(modellist.FirstOrDefault().Date.Month, modellist.FirstOrDefault().Date.Year);

                        foreach (EmployeeApplyLeave xEmployeeApplyLeave in xEmployeeLeaveDetailList)
                        {
                            EmployeeMonthlyAttendance xEmployeeMonthlyAttendanceRecord = await _uow.EmployeeMonthlyAttendanceRepository.FindAsync(x => x.EmployeeId == xEmployeeApplyLeave.EmployeeId && x.Month == modellist.FirstOrDefault().Date.Date.Month && x.Year == modellist.FirstOrDefault().Date.Date.Year && x.IsDeleted == false);

                            if (xEmployeeMonthlyAttendanceRecord == null)
                            {

                                EmployeeMonthlyAttendance xEmployeeMonthlyAttendance = new EmployeeMonthlyAttendance();

                                if (xEmployeeApplyLeave.ApplyLeaveStatusId == 1)
                                {
                                    //remove hardcoded attendance hours once all office hours are available in master table
                                    xEmployeeMonthlyAttendance.IsDeleted = false;
                                    xEmployeeMonthlyAttendance.OfficeId = modellist.FirstOrDefault().OfficeId;
                                    xEmployeeMonthlyAttendance.EmployeeId = xEmployeeApplyLeave.EmployeeId;
                                    xEmployeeMonthlyAttendance.Month = modellist.FirstOrDefault().Date.Month;
                                    xEmployeeMonthlyAttendance.Year = modellist.FirstOrDefault().Date.Year;
                                    xEmployeeMonthlyAttendance.AttendanceHours = xEmployeeMonthlyAttendance.AttendanceHours != null ? xEmployeeMonthlyAttendance.AttendanceHours.Value : 0;
                                    //xEmployeeMonthlyAttendance.AttendanceHours += officeDailyHour;
                                    xEmployeeMonthlyAttendance.LeaveHours += xEmployeeMonthlyAttendance.LeaveHours != null ? xEmployeeMonthlyAttendance.LeaveHours : 0;
                                    xEmployeeMonthlyAttendance.LeaveHours += officeDailyHour;
                                    xEmployeeMonthlyAttendance.TotalDuration = officeMonthlyHour;

                                    Advances xAdvances = await _uow.GetDbContext().Advances.Where(x => x.IsDeleted == false && x.IsApproved == true && x.IsDeducted == false
                                                                               && x.EmployeeId == xEmployeeApplyLeave.EmployeeId && x.OfficeId == modellist.FirstOrDefault().OfficeId
                                                                               && x.AdvanceDate <= DateTime.Now).FirstOrDefaultAsync();
                                    if (xAdvances != null)
                                    {
                                        xEmployeeMonthlyAttendance.AdvanceId = xAdvances.AdvancesId;
                                        xEmployeeMonthlyAttendance.IsAdvanceApproved = xAdvances.IsApproved;
                                        xEmployeeMonthlyAttendance.AdvanceAmount = xAdvances.AdvanceAmount - xAdvances.RecoveredAmount;
                                    }

                                    await _uow.EmployeeMonthlyAttendanceRepository.AddAsyn(xEmployeeMonthlyAttendance);
                                    await _uow.SaveAsync();
                                }
                                else
                                {
                                    xEmployeeMonthlyAttendance.IsDeleted = false;
                                    xEmployeeMonthlyAttendance.OfficeId = modellist[0].OfficeId;
                                    xEmployeeMonthlyAttendance.EmployeeId = xEmployeeApplyLeave.EmployeeId;
                                    xEmployeeMonthlyAttendance.Month = modellist.FirstOrDefault().Date.Month;
                                    xEmployeeMonthlyAttendance.Year = modellist.FirstOrDefault().Date.Year;
                                    xEmployeeMonthlyAttendance.AbsentHours = xEmployeeMonthlyAttendance.AbsentHours == null ? 0 : xEmployeeMonthlyAttendance.AbsentHours;
                                    xEmployeeMonthlyAttendance.AbsentHours += officeDailyHour;
                                    xEmployeeMonthlyAttendance.TotalDuration = officeMonthlyHour;

                                    Advances xAdvances = await _uow.GetDbContext().Advances.Where(x => x.IsDeleted == false && x.IsApproved == true && x.IsDeducted == false
                                                                              && x.EmployeeId == xEmployeeApplyLeave.EmployeeId && x.OfficeId == modellist.FirstOrDefault().OfficeId
                                                                              && x.AdvanceDate < DateTime.Now).FirstOrDefaultAsync();
                                    if (xAdvances != null)
                                    {
                                        xEmployeeMonthlyAttendance.AdvanceId = xAdvances.AdvancesId;
                                        xEmployeeMonthlyAttendance.AdvanceAmount = xAdvances.AdvanceAmount - xAdvances.RecoveredAmount;
                                        xEmployeeMonthlyAttendance.IsAdvanceApproved = xAdvances.IsApproved;
                                    }

                                    await _uow.EmployeeMonthlyAttendanceRepository.AddAsyn(xEmployeeMonthlyAttendance);
                                    await _uow.SaveAsync();
                                }

                            }
                            else//if Employee Monthly Attendance Record is present then add leave hours
                            {
                                if (xEmployeeApplyLeave.ApplyLeaveStatusId == (int)ApplyLeaveStatus.Approve)
                                {
                                    xEmployeeMonthlyAttendanceRecord.LeaveHours = officeDailyHour;
                                    await _uow.EmployeeMonthlyAttendanceRepository.UpdateAsyn(xEmployeeMonthlyAttendanceRecord);
                                    await _uow.SaveAsync();
                                }
                            }

                        }
                    }

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Office Hours Not Set";

                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetAllEmployeeMonthlyPayrollList(int officeid, int currencyid, int month, int year, int paymentType)
        {
            APIResponse response = new APIResponse();
            try
            {
                //NOTE: HeadTypeId -->>
                // 1. Allowances
                // 2. Deductions
                // 3. General(basicPay / hours)

                ICollection<EmployeeMonthlyAttendance> empPayrollAttendanceList = await _uow.GetDbContext().EmployeeMonthlyAttendance.Include(x => x.EmployeeDetails).Where(x => x.OfficeId == officeid && x.Month == month && x.Year == year && x.IsDeleted == false && x.IsApproved == false && x.EmployeeDetails.IsDeleted== false).ToListAsync();

                //Note: default 0.045 i.e. (4.5 %)
                //double? pensionRate = _uow.GetDbContext().EmployeePensionRate.Include(x => x.FinancialYearDetail).FirstOrDefault(x => x.FinancialYearDetail.StartDate.Year == year && x.IsDeleted == false)?.PensionRate;
                double? pensionRate = _uow.GetDbContext().EmployeePensionRate.FirstOrDefault(x => x.IsDefault == true && x.IsDeleted == false)?.PensionRate;

                List<EmployeeMonthlyPayrollModel> payrollFinal = new List<EmployeeMonthlyPayrollModel>();

                foreach (EmployeeMonthlyAttendance payrollAttendance in empPayrollAttendanceList)
                {
                    List<EmployeePayrollModel> payrollDetail = new List<EmployeePayrollModel>();

                    var payroll = await _uow.GetDbContext().EmployeePayroll.Include(x => x.SalaryHeadDetails).Where(x => x.EmployeeID == payrollAttendance.EmployeeId && x.IsDeleted == false).ToListAsync();

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
                            obj.PresentDays = payrollAttendance.AttendanceHours == null ? 0 : payrollAttendance.AttendanceHours.Value;
                            obj.LeaveHours = payrollAttendance.LeaveHours == null ? 0 : payrollAttendance.LeaveHours.Value;
                            obj.WorkingDays = payrollAttendance.AttendanceHours == null ? 0 : payrollAttendance.AttendanceHours.Value;
                            obj.TotalWorkHours = payrollAttendance.TotalDuration == null ? 0 : payrollAttendance.TotalDuration.Value;
                            obj.OverTimeHours = payrollAttendance.OvertimeHours == null ? 0 : payrollAttendance.OvertimeHours.Value;
                            obj.IsAdvanceRecovery = payrollAttendance.IsAdvanceRecovery;
                            obj.AdvanceAmount = payrollAttendance.AdvanceAmount;

                            obj.CurrencyId = payrollDetail.FirstOrDefault().CurrencyId;
                            obj.OverTimeHours = payrollAttendance.OvertimeHours;
                            obj.TotalAllowance = payrollDetail.Where(x => x.HeadTypeId == (int)SalaryHeadType.ALLOWANCE).Sum(s => s.MonthlyAmount);
                            obj.TotalDeduction = payrollDetail.Where(x => x.HeadTypeId == (int)SalaryHeadType.DEDUCTION).Sum(s => s.MonthlyAmount);
                            obj.TotalGeneralAmount = payrollDetail.Where(x => x.HeadTypeId == (int)SalaryHeadType.GENERAL).Sum(s => s.MonthlyAmount);

                            if (obj.TotalGeneralAmount == 0)
                            {
                                throw new Exception($"Basic Pay not defined for Employee Code-{payrollAttendance.EmployeeDetails.EmployeeCode}");
                            }

                            obj.GrossSalary = obj.TotalGeneralAmount * (obj.PresentDays + obj.LeaveHours + obj.OverTimeHours) + obj.TotalAllowance;
                            obj.PensionAmount = (obj.GrossSalary * payrollDetail.FirstOrDefault().PensionRate) / 100; // i.e. 4.5 % => 0.045

                            if (obj.GrossSalary > 5000)
                            {
                                double? dExchangeRate1 = 0.0;
                                ExchangeRateDetail exchangeRateDetail = _uow.GetDbContext().ExchangeRateDetail.OrderByDescending(x => x.Date).FirstOrDefault(x => x.FromCurrency == iCurrencyId && x.ToCurrency == (int)Currency.AFG);

                                if (exchangeRateDetail == null)
                                {
                                    string currencyCode = _uow.CurrencyDetailsRepository.Find(x => x.IsDeleted == false && x.CurrencyId == iCurrencyId).CurrencyCode;

                                    throw new Exception($"Exchange Rate Not Defined from {currencyCode} to AFG");
                                }
                                else
                                {
                                    dExchangeRate1 = (double)exchangeRateDetail.Rate;
                                }

                                obj.SalaryTax = obj.SalaryTax == null ? 0 : obj.SalaryTax;
                                obj.SalaryTax = Math.Round(Convert.ToDouble((SalaryCalculate(obj.GrossSalary.Value, dExchangeRate1.Value))), 2);
                            }
                            else
                            {
                                obj.SalaryTax = 0;
                            }

                            //Net Salary  = (Gross + Allowances) - Deductions
                            obj.NetSalary = obj.GrossSalary - (obj.TotalDeduction != null ? obj.TotalDeduction : 0) - (obj.SalaryTax != null ? obj.SalaryTax : 0) - payrollAttendance.AdvanceRecoveryAmount - (obj.PensionAmount != null ? obj.PensionAmount : 0);

                            obj.employeepayrolllist.AddRange(payrollDetail);

                            payrollFinal.Add(obj);

                            Advances xAdvances = await _uow.GetDbContext().Advances.FirstOrDefaultAsync(x => x.IsDeleted == false && x.IsApproved == true
                                                                           && x.EmployeeId == payrollAttendance.EmployeeId && x.OfficeId == payrollAttendance.OfficeId
                                                                           && x.AdvanceDate < DateTime.Now && x.IsDeducted == false);

                            if (xAdvances != null)
                            {
                                if (xAdvances.RecoveredAmount == 0)
                                {

                                    obj.AdvanceRecoveryAmount = Convert.ToInt64(xAdvances.AdvanceAmount / xAdvances.NumberOfInstallments ?? 1);
                                    obj.AdvanceAmount = xAdvances.AdvanceAmount;
                                    obj.IsAdvanceApproved = xAdvances.IsApproved;
                                }
                                else
                                {
                                    Double iBalanceAmount = xAdvances.AdvanceAmount - xAdvances.RecoveredAmount;
                                    obj.AdvanceRecoveryAmount = Convert.ToInt64(iBalanceAmount / xAdvances.NumberOfInstallments);
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

                            obj.employeepayrolllist.AddRange(payrollDetail.Where(x => x.EmployeeId == obj.EmployeeId));
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

        public async Task<APIResponse> GetAllEmployeeMonthlyPayrollListApproved(int officeid, int month, int year, int paymentType)
        {
            APIResponse response = new APIResponse();
            try
            {
                List<EmployeePaymentTypes> userdetail = await _uow.GetDbContext()
                                                            .EmployeePaymentTypes.Where(x => x.PayrollMonth == month && x.PayrollYear == year && x.OfficeId == officeid && x.IsDeleted == false)
                                                            .Include(x => x.EmployeeDetail).ToListAsync();


                List<EmployeeMonthlyPayrollModelApproved> userList = new List<EmployeeMonthlyPayrollModelApproved>();

                foreach (EmployeePaymentTypes item in userdetail)
                {
                    EmployeeMonthlyPayrollModelApproved obj = new EmployeeMonthlyPayrollModelApproved()
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
                        IsAdvanceApproved = item.IsAdvanceApproved,
                        AdvanceRecoveryAmount = item.AdvanceRecoveryAmount ?? 0,
                        IsAdvanceRecovery = item.IsAdvanceRecovery == true ? false : true,
                        CurrencyCode = item.CurrencyCode,
                        CurrencyId = item.CurrencyId,
                        EmployeeCode = item.EmployeeDetail.EmployeeCode

                    };

                    obj.EmployeePayrollList = new List<EmployeePayrollModel>();



                    List<EmployeePayrollModel> EmployeePayrollMonthList = await _uow.GetDbContext().EmployeePayrollMonth
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

        public async Task<APIResponse> EmployeePaymentTypeReportForSaveOnly(List<EmployeePaymentTypeModel> model, string userid)
        {
            APIResponse response = new APIResponse();
            try
            {
                foreach (EmployeePaymentTypeModel employeePayroll in model)
                {
                    EmployeeMonthlyAttendance employeeMonthlyAttendance = await _uow.GetDbContext().EmployeeMonthlyAttendance.Where(x => x.IsDeleted == false && x.EmployeeId == employeePayroll.EmployeeId && x.Year == employeePayroll.Year && x.Month == employeePayroll.Month && x.OfficeId == employeePayroll.OfficeId).FirstOrDefaultAsync();

                    employeeMonthlyAttendance.HourlyRate = employeePayroll.HourlyRate;
                    employeeMonthlyAttendance.GrossSalary = employeePayroll.GrossSalary;
                    employeeMonthlyAttendance.NetSalary = employeePayroll.NetSalary;
                    employeeMonthlyAttendance.PaymentType = employeePayroll.PaymentType;
                    employeeMonthlyAttendance.PensionAmount = employeePayroll.PensionAmount;
                    employeeMonthlyAttendance.PensionRate = employeePayroll.PensionRate;
                    employeeMonthlyAttendance.SalaryTax = employeePayroll.SalaryTax;
                    employeeMonthlyAttendance.TotalAllowance = employeePayroll.TotalAllowance;
                    employeeMonthlyAttendance.TotalDeduction = employeePayroll.TotalDeduction;
                    employeeMonthlyAttendance.IsAdvanceRecovery = employeePayroll.IsAdvanceRecovery;
                    employeeMonthlyAttendance.IsAdvanceApproved = employeePayroll.IsAdvanceApproved;
                    employeeMonthlyAttendance.AdvanceAmount = employeePayroll.AdvanceAmount == null ? 0 : employeePayroll.AdvanceAmount.Value;
                    employeeMonthlyAttendance.AdvanceRecoveryAmount = employeePayroll.AdvanceRecoveryAmount == null ? 0 : employeePayroll.AdvanceRecoveryAmount.Value;

                    await _uow.EmployeeMonthlyAttendanceRepository.UpdateAsyn(employeeMonthlyAttendance);

                    #region Addition of EmployeePayrollMonth Removed(Moved to approve payroll)

                    //var payrollList = await _uow.GetDbContext().EmployeePayrollMonth.Where(x => x.EmployeeID == employeePayroll.EmployeeId && x.IsDeleted == false && x.Date.Month == employeePayroll.Month && x.Date.Year == employeePayroll.Year).ToListAsync();

                    //if (payrollList.Count > 0)
                    //{
                    //    foreach (var empPayroll in payrollList)
                    //    {
                    //        await _uow.EmployeePayrollMonthRepository.DeleteAsyn(empPayroll);
                    //    }
                    //}

                    //foreach (var payroll in employeePayroll.employeepayrolllist)
                    //{
                    //    EmployeePayrollMonth employeePayrollMonth = new EmployeePayrollMonth();
                    //    employeePayrollMonth.CurrencyId = payroll.CurrencyId;
                    //    employeePayrollMonth.Date = new DateTime(employeePayroll.Year, employeePayroll.Month, DateTime.Now.Day);
                    //    employeePayrollMonth.EmployeeID = employeePayroll.EmployeeId;
                    //    employeePayrollMonth.IsDeleted = false;
                    //    employeePayrollMonth.MonthlyAmount = payroll.MonthlyAmount;
                    //    employeePayrollMonth.SalaryHeadId = payroll.SalaryHeadId;
                    //    employeePayrollMonth.HeadTypeId = payroll.HeadTypeId;
                    //    employeePayrollMonth.PaymentType = payroll.PaymentType;

                    //    await _uow.EmployeePayrollMonthRepository.AddAsyn(employeePayrollMonth);
                    //    await _uow.SaveAsync();
                    //}
                    #endregion
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

        public async Task<APIResponse> EmployeePaymentTypeReport(List<EmployeePaymentTypeModel> model, string userid)
        {
            APIResponse response = new APIResponse();
            try
            {
                foreach (EmployeePaymentTypeModel finalPayroll in model)
                {
                    EmployeeMonthlyAttendance xEmployeeMonthlyAttendance = await _uow.GetDbContext().EmployeeMonthlyAttendance
                                                                                             .FirstOrDefaultAsync(x => x.EmployeeId == finalPayroll.EmployeeId && x.IsDeleted == false &&
                                                                                             x.Month == finalPayroll.Month && x.Year == finalPayroll.Year);

                    if (xEmployeeMonthlyAttendance != null)
                    {
                        xEmployeeMonthlyAttendance.IsApproved = true;
                        await _uow.EmployeeMonthlyAttendanceRepository.UpdateAsyn(xEmployeeMonthlyAttendance);
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

                    _uow.EmployeePaymentTypeRepository.Add(EmployeeApprovedPayroll);

                    if (finalPayroll.employeepayrolllist.Count > 0)
                    {
                        foreach (var payroll in finalPayroll.employeepayrolllist)
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
                            await _uow.EmployeePayrollMonthRepository.AddAsyn(employeePayrollMonth);
                        }
                    }

                    if (finalPayroll.IsAdvanceRecovery)
                    {
                        Advances xAdvances = await _uow.GetDbContext().Advances.Where(x => x.IsDeleted == false && x.IsApproved == true
                                                                        && x.EmployeeId == finalPayroll.EmployeeId && x.OfficeId == finalPayroll.OfficeId
                                                                        && x.AdvanceDate < DateTime.Now && x.IsDeducted == false).FirstOrDefaultAsync();

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

                                await _uow.EmployeePaymentTypeRepository.UpdateAsyn(EmployeeApprovedPayroll);
                            }

                            await _uow.AdvancesRepository.UpdateAsyn(xAdvances);
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

        public async Task<APIResponse> EmployeePensionReport(PensionModel model)
        {
            APIResponse response = new APIResponse();

            EmployeePensionRate pensionRateDetail = _uow.GetDbContext().EmployeePensionRate.FirstOrDefault(x => x.IsDefault == true && x.IsDeleted == false);
            double pensionRate = pensionRateDetail != null ? (double)pensionRateDetail.PensionRate : 4.5;

            try
            {
                EmployeePensionModel epm = new EmployeePensionModel();
                List<EmployeePensionReportModel> lst = new List<EmployeePensionReportModel>();
                var financialYearList = await _uow.FinancialYearDetailRepository.FindAllAsync(x => model.FinancialYearId.Contains(x.FinancialYearId));
                var empList = await _uow.EmployeePaymentTypeRepository.FindAllAsync(x => financialYearList.Select(y=> y.StartDate.Year).Contains(x.PayrollYear.Value) && x.OfficeId == model.OfficeId && x.EmployeeID == model.EmployeeId && x.IsDeleted == false);

                EmployeeDetail xEmployeeDetail = await _uow.EmployeeDetailRepository.FindAsync(x => x.EmployeeID == model.EmployeeId);

                var previousPensionList = await _uow.EmployeePaymentTypeRepository.FindAllAsync(x => x.PayrollYear < financialYearList.OrderByDescending(y=> y.StartDate).FirstOrDefault().StartDate.Year && x.IsDeleted == false);

                //if (previousPensionList.Any())
                //{
                    epm.PreviousPensionRate = previousPensionList.Average(x => x.PensionRate);

                    foreach (var item in previousPensionList)
                    {
                        if (item.CurrencyId == model.CurrencyId)
                        {
                            epm.PreviousPensionDeduction += item.PensionAmount;
                            epm.PreviousProfit += Math.Round(Convert.ToDouble(item.PensionAmount * item.PensionRate), 2);
                            epm.PreviousTotal += Math.Round(Convert.ToDouble((item.PensionAmount * item.PensionRate) + item.PensionAmount), 2);
                        }
                        else
                        {
                            ExchangeRateDetail exchangeRate = await _uow.GetDbContext().ExchangeRateDetail.OrderByDescending(x => x.Date).FirstOrDefaultAsync(x => x.IsDeleted == false && x.FromCurrency == item.CurrencyId && x.ToCurrency == model.CurrencyId);
                            epm.PreviousPensionDeduction += item.PensionAmount * (double)exchangeRate.Rate;
                            epm.PreviousProfit += Math.Round(Convert.ToDouble(item.PensionAmount * pensionRate), 2) * (double)exchangeRate.Rate;
                            epm.PreviousTotal += Math.Round(Convert.ToDouble((item.PensionAmount * pensionRate) + item.PensionAmount), 2) * (double)exchangeRate.Rate;

                        }

                    }
                    foreach (var item in empList)
                    {
                        ExchangeRateDetail exchangeRate = await _uow.GetDbContext().ExchangeRateDetail.OrderByDescending(x => x.Date).FirstOrDefaultAsync(x => x.IsDeleted == false && x.FromCurrency == item.CurrencyId && x.ToCurrency == model.CurrencyId);

                        EmployeePensionReportModel obj = new EmployeePensionReportModel();
                        obj.CurrencyId = item.CurrencyId.Value;
                        obj.Date = new DateTime(item.PayrollYear.Value, item.PayrollMonth.Value, 1);
                        obj.GrossSalary = Math.Round(Convert.ToDouble(item.GrossSalary), 2) * (double)exchangeRate.Rate;
                        obj.PensionRate = pensionRate;
                        obj.PensionDeduction = Math.Round(Convert.ToDouble((item.GrossSalary * pensionRate) / 100), 2) * (double)exchangeRate.Rate;
                        obj.Profit = Math.Round(Convert.ToDouble((obj.PensionDeduction * pensionRate)) / 100, 2);
                        obj.Total = obj.Profit + obj.PensionDeduction;
                        lst.Add(obj);

                    }
                    epm.EmployeePensionReportList = lst.OrderBy(x => x.Date.Date).ToList();
                    epm.PensionTotal = lst.Sum(x => x.Total);
                    epm.PensionProfitTotal = lst.Sum(x => x.Profit);
                    epm.PensionDeductionTotal = Math.Round(Convert.ToDouble(lst.Sum(x => x.GrossSalary * pensionRate / 100)), 2);
                    epm.EmployeeCode = xEmployeeDetail.EmployeeCode;
                    response.data.EmployeePensionModel = epm;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                //}
                //else
                //{
                //    response.StatusCode = StaticResource.successStatusCode;
                //    response.Message = StaticResource.NoPension;

                //}
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> EmployeeSalaryTaxDetails(SalaryTaxViewModel model)
        {
            APIResponse response = new APIResponse();

            try
            {

                var financialYear =  _uow.FinancialYearDetailRepository.FindAll(x => x.IsDeleted == false && model.FinancialYearId.Contains(x.FinancialYearId));

                if (financialYear.Any())
                {
                    List<SalaryTaxReportModel> salaryTaxReportListFinal = new List<SalaryTaxReportModel>();

                    foreach (FinancialYearDetail financialYearDetail in financialYear)
                    {
                        List<SalaryTaxReportModel> salaryTaxReportList = _uow.EmployeePaymentTypeRepository.FindAll(x => x.IsDeleted == false && x.IsApproved == true && x.OfficeId == model.OfficeId && x.EmployeeID == model.EmployeeId && x.PayrollYear == financialYearDetail.StartDate.Date.Year)
                        .Select(x => new SalaryTaxReportModel
                        {
                            Currency = _uow.GetDbContext().CurrencyDetails.Where(o => o.CurrencyId == x.CurrencyId).FirstOrDefault().CurrencyName,
                            CurrencyId = x.CurrencyId,
                            Office = _uow.GetDbContext().OfficeDetail.Where(o => o.OfficeId == x.OfficeId).FirstOrDefault().OfficeName,
                            Date = new DateTime(x.PayrollYear.Value, x.PayrollMonth.Value, 1),
                            TotalTax = x.SalaryTax
                        }).OrderBy(x => x.Date).ToList();

                        foreach (SalaryTaxReportModel item in salaryTaxReportList)
                        {
                            ExchangeRateDetail exchangeRate = await _uow.GetDbContext().ExchangeRateDetail.OrderByDescending(x => x.Date).FirstOrDefaultAsync(x => x.IsDeleted == false && x.FromCurrency == item.CurrencyId && x.ToCurrency == model.CurrencyId);

                            if (item.CurrencyId != model.CurrencyId)
                            {
                                item.TotalTax = item.TotalTax * (double)exchangeRate.Rate;
                            }
                        }

                        if (salaryTaxReportList.Any())
                        {
                            salaryTaxReportListFinal.AddRange(salaryTaxReportList);
                        }
                    }

                    response.data.SalaryTaxReportModelList = salaryTaxReportListFinal;
                }
                else
                    response.data.SalaryTaxReportModelList = null;
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

        /// <summary>
        /// Get All Employee Pension By OfficeId
        /// </summary>
        /// <param name="OfficeId"></param>
        /// <returns>Employees with Total Pension Amount over the years</returns>
        public async Task<APIResponse> GetAllEmployeePension(int OfficeId)
        {
            APIResponse response = new APIResponse();

            try
            {

                List<PensionPaymentModel> PensionPaymentList = new List<PensionPaymentModel>();

                //Get Employees total pension from approved payroll
                var salaryTaxReportList = from eptypes in _uow.GetDbContext().EmployeePaymentTypes.Include(x => x.EmployeeDetail)
                                          join ed in _uow.GetDbContext().EmployeeDetail on eptypes.EmployeeID equals ed.EmployeeID
                                          join epd in _uow.GetDbContext().EmployeeProfessionalDetail on ed.EmployeeID equals epd.EmployeeId
                                          where eptypes.IsDeleted == false && eptypes.OfficeId == OfficeId && eptypes.IsApproved == true &&
                                          epd.EmployeeTypeId != (int)EmployeeTypeStatus.Prospective
                                          group eptypes by eptypes.EmployeeID
                                                                  into eGroup
                                          select new
                                          {
                                              Key = eGroup.Key,
                                              EmployeePension = eGroup.OrderBy(x => x.EmployeeID)
                                          };

                List<PensionPaymentHistory> pensionPaymentList = await _uow.GetDbContext().PensionPaymentHistory.Include(x => x.EmployeeDetail).ThenInclude(x => x.EmployeeProfessionalDetail).Where(x => x.EmployeeDetail.EmployeeProfessionalDetail.OfficeId == OfficeId && x.IsDeleted == false).ToListAsync();

                foreach (var item in salaryTaxReportList)
                {
                    PensionPaymentModel PensionPayment = new PensionPaymentModel();
                    PensionPayment.EmployeeId = item.Key;
                    PensionPayment.EmployeeName = item.EmployeePension.FirstOrDefault().EmployeeDetail.EmployeeName;
                    PensionPayment.EmployeeCode = item.EmployeePension.FirstOrDefault().EmployeeDetail.EmployeeCode;
                    PensionPayment.TotalPensionAmount = Convert.ToDecimal(item.EmployeePension.Sum(x => x.PensionAmount));
                    PensionPayment.PensionAmountPaid = pensionPaymentList.Where(x => x.EmployeeId == item.Key).Sum(x => x.PaymentAmount);
                    PensionPayment.CurrencyId = item.EmployeePension.FirstOrDefault().CurrencyId;
                    PensionPaymentList.Add(PensionPayment);
                }

                var employeesWithNoPension = (from ed in _uow.GetDbContext().EmployeeDetail

                                              join epd in _uow.GetDbContext().EmployeeProfessionalDetail on ed.EmployeeID equals epd.EmployeeId into edr
                                              from edresult in edr.DefaultIfEmpty()
                                              where ed.IsDeleted == false && edresult.OfficeId == OfficeId
                                              && edresult.EmployeeTypeId != (int)EmployeeTypeStatus.Prospective
                                              && !PensionPaymentList.Select(x => x.EmployeeId).Contains(ed.EmployeeID)
                                              select new
                                              {
                                                  EmployeeId = ed.EmployeeID,
                                                  EmployeeName = ed.EmployeeName,
                                                  EmployeeCode = ed.EmployeeCode,
                                              });

                foreach (var item in employeesWithNoPension)
                {
                    PensionPaymentModel PensionPayment = new PensionPaymentModel();
                    PensionPayment.EmployeeId = item.EmployeeId;
                    PensionPayment.EmployeeName = item.EmployeeName;
                    PensionPayment.EmployeeCode = item.EmployeeCode;
                    PensionPayment.TotalPensionAmount = 0;
                    PensionPayment.PensionAmountPaid = 0;
                    PensionPayment.CurrencyId = _uow.GetDbContext().EmployeePayroll.FirstOrDefault(x => x.IsDeleted == false && x.EmployeeID == item.EmployeeId).CurrencyId;
                    PensionPaymentList.Add(PensionPayment);
                }

                response.data.PensionPayment = PensionPaymentList;

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

        /// <summary>
        /// Get Employee Pension History Detail
        /// </summary>
        /// <param name="OfficeId"></param>
        /// <returns>Employee Pension Payment History Details</returns>
        public async Task<APIResponse> GetEmployeePensionHistoryDetail(int EmployeeId, int OfficeId)
        {
            APIResponse response = new APIResponse();

            try
            {

                List<PensionPaymentHistoryModel> PensionPaymentHistoryList = new List<PensionPaymentHistoryModel>();

                //Get Employees total pension from approved payroll
                List<PensionPaymentHistory> pensionPaymentList = await _uow.GetDbContext().PensionPaymentHistory.Include(x => x.EmployeeDetail).ThenInclude(x => x.EmployeeProfessionalDetail)
                                                                                          .Where(x => x.EmployeeDetail.EmployeeProfessionalDetail.OfficeId == OfficeId && x.EmployeeId == EmployeeId && x.IsDeleted == false)
                                                                                          .ToListAsync();

                foreach (var item in pensionPaymentList)
                {
                    PensionPaymentHistoryModel pensionHistory = new PensionPaymentHistoryModel();
                    pensionHistory.Employee = $"{item.EmployeeDetail.EmployeeCode}-{item.EmployeeDetail.EmployeeName}";
                    pensionHistory.PensionPaid = item.PaymentAmount;
                    pensionHistory.PaymentDate = item.PaymentDate;
                    pensionHistory.VoucherNo = item.VoucherNo;
                    pensionHistory.VoucherReferenceNo = item.VoucherReferenceNo;

                    PensionPaymentHistoryList.Add(pensionHistory);
                }

                response.data.PensionPaymentHistory = PensionPaymentHistoryList;

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

        #region "Monthly Payroll Hours"
        public async Task<APIResponse> GetAllPayrollMonthlyHourDetail(PayrollHourFilterModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model != null)
                {
                    List<PayrollMonthlyHourDetailModel> payrollHours = await _uow.GetDbContext().PayrollMonthlyHourDetail
                                                                            .Where(x => x.PayrollYear == model.Year && x.OfficeId == model.OfficeId && x.IsDeleted == false)
                                                                            .Select(x => new PayrollMonthlyHourDetailModel
                                                                            {
                                                                                PayrollMonthlyHourID = x.PayrollMonthlyHourID,
                                                                                OfficeId = x.OfficeId,
                                                                                OfficeName = x.OfficeDetails.OfficeName,
                                                                                PayrollMonth = x.PayrollMonth,
                                                                                PayrollYear = x.PayrollYear,
                                                                                Hours = x.Hours,  //total working hours a Day
                                                                                WorkingTime = x.WorkingTime,   //total working hours for Month
                                                                                InTime = x.InTime,
                                                                                OutTime = x.OutTime
                                                                            }).ToListAsync();

                    response.data.PayrollMonthlyHourList = payrollHours;
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
        public async Task<APIResponse> AddPayrollMonthlyHourDetail(PayrollMonthlyHourDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {


                TimeSpan hours;
                hours = Convert.ToDateTime(model.OutTime) - Convert.ToDateTime(model.InTime);
                model.Hours = Convert.ToInt32(hours.ToString().Substring(0, 2));

                

                if (model.SaveForAllOffice)
                {
                    List<PayrollMonthlyHourDetail> payrollMonthlyHourDetailsAdd = new List<PayrollMonthlyHourDetail>();
                    List<PayrollMonthlyHourDetail> payrollMonthlyHourDetailsUpdate = new List<PayrollMonthlyHourDetail>();

                    List<int> officeIds = _uow.GetDbContext().OfficeDetail.Where(x => x.IsDeleted == false).Select(x => x.OfficeId).ToList();

                    foreach (int officeId in officeIds)
                    {
                        var payrollinfo = await _uow.PayrollMonthlyHourDetailRepository
                                    .FindAsync(x => x.IsDeleted == false &&
                                                    x.OfficeId == officeId &&
                                                    x.PayrollMonth == model.PayrollMonth &&
                                                    x.PayrollYear == model.PayrollYear
                                               );

                        if (payrollinfo == null)
                        {
                            PayrollMonthlyHourDetail obj = new PayrollMonthlyHourDetail();
                            obj.CreatedById = model.CreatedById;
                            obj.CreatedDate = model.CreatedDate;
                            obj.Hours = model.Hours;
                            obj.WorkingTime = model.WorkingTime; //total working hours
                            obj.InTime = model.InTime;
                            obj.OutTime = model.OutTime;
                            obj.PayrollMonth = model.PayrollMonth;
                            obj.PayrollYear = model.PayrollYear;
                            obj.IsDeleted = false;
                            obj.OfficeId = officeId;
                            payrollMonthlyHourDetailsAdd.Add(obj);
                        }
                        else
                        {
                            payrollinfo.ModifiedDate = DateTime.UtcNow;
                            payrollinfo.Hours = model.Hours;
                            payrollinfo.WorkingTime= model.WorkingTime;
                            payrollinfo.InTime = model.InTime;
                            payrollinfo.OutTime = model.OutTime;
                            payrollinfo.PayrollMonth = model.PayrollMonth;
                            payrollinfo.PayrollYear = model.PayrollYear;
                            payrollinfo.OfficeId = officeId;

                            payrollMonthlyHourDetailsUpdate.Add(payrollinfo);
                        }
                    }

                    if (payrollMonthlyHourDetailsAdd.Any())
                    {
                        await _uow.GetDbContext().AddRangeAsync(payrollMonthlyHourDetailsAdd);
                        await _uow.GetDbContext().SaveChangesAsync();
                    }

                    if (payrollMonthlyHourDetailsUpdate.Any())
                    {
                        _uow.GetDbContext().UpdateRange(payrollMonthlyHourDetailsUpdate);
                        await _uow.GetDbContext().SaveChangesAsync();
                    }
                }
                else
                {
                    var payrollinfo = await _uow.PayrollMonthlyHourDetailRepository
                                    .FindAsync(x => x.IsDeleted == false &&
                                                    x.OfficeId == model.OfficeId &&
                                                    x.PayrollMonth == model.PayrollMonth &&
                                                    x.PayrollYear == model.PayrollYear
                                               );

                    if (payrollinfo == null)
                    {
                        PayrollMonthlyHourDetail obj = new PayrollMonthlyHourDetail();
                        obj.CreatedById = model.CreatedById;
                        obj.CreatedDate = model.CreatedDate;
                        obj.Hours = model.Hours;
                        obj.WorkingTime = model.WorkingTime; //total working hours
                        obj.InTime = model.InTime;
                        obj.OutTime = model.OutTime;
                        obj.PayrollMonth = model.PayrollMonth;
                        obj.PayrollYear = model.PayrollYear;
                        obj.IsDeleted = false;
                        obj.OfficeId = model.OfficeId;
                        await _uow.PayrollMonthlyHourDetailRepository.AddAsyn(obj);
                        await _uow.SaveAsync();
                    }
                    else
                    {
                        response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                        response.Message = StaticResource.HoursAlreadySet;
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

        public async Task<APIResponse> EditPayrollMonthlyHourDetail(PayrollMonthlyHourDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {

                var payrollmonthlyinfo = await _uow.PayrollMonthlyHourDetailRepository
                                     .FindAsync(x => x.IsDeleted == false &&
                                                     x.OfficeId == model.OfficeId &&
                                                     x.PayrollMonth == model.PayrollMonth &&
                                                     x.PayrollYear == model.PayrollYear
                                                );
                if (payrollmonthlyinfo != null)
                {
                    TimeSpan hours;
                    hours = Convert.ToDateTime(model.OutTime) - Convert.ToDateTime(model.InTime);

                    payrollmonthlyinfo.OfficeId = model.OfficeId;
                    payrollmonthlyinfo.PayrollMonth = model.PayrollMonth;
                    payrollmonthlyinfo.PayrollYear = model.PayrollYear;
                    payrollmonthlyinfo.Hours = Convert.ToInt32(hours.ToString().Substring(0, 2));
                    payrollmonthlyinfo.WorkingTime = model.WorkingTime;
                    payrollmonthlyinfo.InTime = model.InTime;
                    payrollmonthlyinfo.OutTime = model.OutTime;
                    payrollmonthlyinfo.ModifiedById = model.ModifiedById;
                    payrollmonthlyinfo.ModifiedDate = model.ModifiedDate;
                    payrollmonthlyinfo.IsDeleted = false;

                    await _uow.PayrollMonthlyHourDetailRepository.UpdateAsyn(payrollmonthlyinfo);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> AddEmployeeLeaveDetails(List<AssignLeaveToEmployeeModel> model)
        {
            APIResponse response = new APIResponse();

            var financialYear = _uow.GetDbContext().FinancialYearDetail.FirstOrDefault(x => x.IsDeleted == false && x.IsDefault == true);
            if (financialYear != null)
            {
                try
                {
                    foreach (var item in model)
                    {
                        var assignleavelist = await _uow.AssignLeaveToEmployeeRepository.FindAsync(x => x.IsDeleted == false && x.FinancialYearId == financialYear.FinancialYearId && x.LeaveReasonId == item.LeaveReasonId && x.EmployeeId == item.EmployeeId);

                        if (assignleavelist == null)
                        {
                            AssignLeaveToEmployee obj = _mapper.Map<AssignLeaveToEmployee>(item);

                            obj.FinancialYearId = financialYear.FinancialYearId;
                            obj.IsDeleted = false;

                            await _uow.AssignLeaveToEmployeeRepository.AddAsyn(obj);
                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = "Success";
                        }
                        else
                        {
                            response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                            response.Message = "Leave Reason Type already exist for this financial year.";
                        }

                    }
                }
                catch (Exception ex)
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong + ex.Message;
                }

            }

            return response;
        }

        public async Task<APIResponse> EditEmployeeSalaryAccountDetail(List<EmployeePayrollAccountModel> model, string userid)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existrecord = await _uow.EmployeePayrollAccountHeadRepository.FindAllAsync(x => x.EmployeeId == model.FirstOrDefault().EmployeeId);

                if (existrecord.Any())
                {
                    _uow.GetDbContext().EmployeePayrollAccountHead.RemoveRange(existrecord);
                    await _uow.SaveAsync();
                }

                List<EmployeePayrollAccountHead> employeepayrollAccountlist = new List<EmployeePayrollAccountHead>();

                foreach (var list in model)
                {
                    EmployeePayrollAccountHead obj = new EmployeePayrollAccountHead();
                    obj.EmployeeId = list.EmployeeId;
                    obj.AccountNo = list.AccountNo;
                    obj.Description = list.Description;
                    obj.PayrollHeadName = list.PayrollHeadName;
                    obj.PayrollHeadTypeId = list.PayrollHeadTypeId;
                    obj.TransactionTypeId = list.TransactionTypeId;
                    obj.PayrollHeadId = list.PayrollHeadId;
                    obj.IsDeleted = false;

                    employeepayrollAccountlist.Add(obj);
                }

                await _uow.GetDbContext().EmployeePayrollAccountHead.AddRangeAsync(employeepayrollAccountlist);
                await _uow.SaveAsync();

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

        #endregion

        /// <summary>
        /// Get Primary Salary Heads
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetPrimarySalaryHeads(int EmployeeId)
        {
            APIResponse response = new APIResponse();

            try
            {
                List<PayrollHeadModel> PayrollHeadModelList = await _uow.GetDbContext().EmployeePayrollAccountHead
                                                                                       .Where(x => x.IsDeleted == false && x.EmployeeId == EmployeeId)
                                                                                       .Select(x => new PayrollHeadModel
                                                                                       {
                                                                                           AccountNo = x.AccountNo,
                                                                                           PayrollHeadId = x.PayrollHeadId,
                                                                                           PayrollHeadName = x.PayrollHeadName,
                                                                                           PayrollHeadTypeId = x.PayrollHeadTypeId,
                                                                                           TransactionTypeId = x.TransactionTypeId
                                                                                       }).ToListAsync();

                response.data.PayrollHeadModelList = PayrollHeadModelList;
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

        /// <summary>
        /// Get Languages List Spoken in Organisation
        /// </summary>
        /// <returns>Language List</returns>
        public async Task<APIResponse> GetAllLanguages()
        {
            APIResponse response = new APIResponse();

            try
            {
                List<LanguageDetail> LanguageDetailList = await _uow.GetDbContext().LanguageDetail.Where(x => x.IsDeleted == false).ToListAsync();


                response.data.LanguageDetail = LanguageDetailList;
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

        /// <summary>
        /// Get Job Code For New Job Hiring
        /// </summary>
        /// <returns>Returns Job Code</returns>
        public async Task<APIResponse> GetJobCode(int officeId)
        {
            APIResponse response = new APIResponse();

            try
            {
                JobHiringDetails jobHiringDetails = await _uow.GetDbContext().JobHiringDetails.OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync(x => x.OfficeId == officeId);

                if (jobHiringDetails != null && jobHiringDetails.JobCode != null)
                {
                    //getting the latest Job Code and finding the max number from it
                    int count = Convert.ToInt32(jobHiringDetails.JobCode.Substring(2));

                    response.data.JobCode = "JC" + String.Format("{0:D4}", ++count);
                }
                else
                {
                    response.data.JobCode = "JC" + String.Format("{0:D4}", 1);
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

        #region reusable need to add them in utility file
        public double SalaryCalculate(double grossSalary, double exchangeRate)
        {
            // Compare Gross salary only in AFG

            double salaryTax = 0;
            if (grossSalary < 5000)
            {
                salaryTax = 0;
            }
            else if (grossSalary >= 5000 && grossSalary < 12500)
            {
                salaryTax = (grossSalary * exchangeRate - 5000) * 2 / 100;
            }
            else if (grossSalary >= 12500 && grossSalary < 100000)
            {
                salaryTax = (((grossSalary * exchangeRate - 12500) * 10 / 100) + 150) / exchangeRate;
            }
            else
            {
                salaryTax = ((((grossSalary * exchangeRate) - 100000) * 20 / 100) + 8900) / exchangeRate;
            }

            //Returned in AFG
            return salaryTax;
        }

        //reusable need to add them in utility file
        public int GetMonthDays(int month, int year)
        {
            int monthdays = 0;
            switch (month)
            {
                case 1:
                    monthdays = 31;
                    break;
                case 2:
                    if (year % 4 == 0)
                        monthdays = 29;
                    else
                        monthdays = 28;
                    break;
                case 3:
                    monthdays = 31;
                    break;
                case 4:
                    monthdays = 30;
                    break;
                case 5:
                    monthdays = 31;
                    break;
                case 6:
                    monthdays = 30;
                    break;
                case 7:
                    monthdays = 31;
                    break;
                case 8:
                    monthdays = 31;
                    break;
                case 9:
                    monthdays = 30;
                    break;
                case 10:
                    monthdays = 31;
                    break;
                case 11:
                    monthdays = 30;
                    break;
                case 12:
                    monthdays = 31;
                    break;
            }
            return monthdays;
        }
        #endregion


        #region"Data Transfer Api For Attendance"

        public string TransferDataForAttendance()
        {
            DateTime date = DateTime.Parse(1 + "/15/2018");
            DateTime.Parse(2003 + "-" + 1 + "-" + 25 + ' ' + "03:30:00");

            try
            {
                List<EmployeeAttendance> empAttList = new List<EmployeeAttendance>();

                var financialYearList = _uow.GetDbContext().FinancialYearDetail.Where(x => x.IsDeleted == false).ToList();

                var EmployeeMonthlyAttendanceList = _uow.GetDbContext().EmployeeMonthlyAttendance.Where(x => x.Year == 2008 && x.Month == 1 && x.IsDeleted == false).ToList();

                //Group by Year
                var groupedByYearData = EmployeeMonthlyAttendanceList.GroupBy(x => new { x.Year }).ToList();

                foreach (var itemYear in groupedByYearData)
                {
                    //if (itemYear.Key.Year == 2003)
                    //{
                    //Group by Month
                    var groupedByMonthData = itemYear.GroupBy(x => new { x.Month }).ToList();

                    foreach (var itemMonth in groupedByMonthData)
                    {
                        int days = DateTime.DaysInMonth(itemYear.Key.Year.Value, itemMonth.Key.Month.Value);

                        foreach (var item in itemMonth)
                        {
                            //int AttenCount = 1;
                            //int LeaveHoursCount = 1;
                            //int AbsentCount = 1;


                            int AttCount = item.AttendanceHours.Value;
                            int LeaCount = item.LeaveHours.Value;
                            int AbsCount = item.AbsentHours.Value;

                            int dateCount = 1;


                            // Attendance
                            //int att = 0;
                            while (AttCount > 0)
                            {
                                DateTime dateTime = DateTime.Parse(item.Month.Value + "/" + dateCount + "/" + item.Year.Value);

                                if (AttCount % 8 == 0)
                                {
                                    EmployeeAttendance empAttObj = new EmployeeAttendance();

                                    empAttObj.IsDeleted = false;
                                    empAttObj.EmployeeId = item.EmployeeId.Value;
                                    empAttObj.InTime = DateTime.Parse(item.Year.Value + "-" + item.Month.Value + "-" + dateCount + " " + "03:30:00");
                                    empAttObj.OutTime = DateTime.Parse(item.Year.Value + "-" + item.Month.Value + "-" + dateCount + " " + "11:30:00");
                                    empAttObj.TotalWorkTime = 8.ToString();
                                    empAttObj.HoverTimeHours = 0;
                                    empAttObj.AttendanceTypeId = 1; //P
                                    empAttObj.Date = dateTime;
                                    empAttObj.FinancialYearId = financialYearList.FirstOrDefault(x => x.StartDate.Year == itemYear.Key.Year.Value).FinancialYearId;//item.Year == 2003 ? 

                                    empAttList.Add(empAttObj);
                                    _uow.EmployeeAttendanceRepository.Add(empAttObj);
                                    //att = att + 8;
                                    AttCount = AttCount - 8;
                                }
                                else
                                {
                                    EmployeeAttendance empAttObj = new EmployeeAttendance();

                                    empAttObj.IsDeleted = false;
                                    empAttObj.EmployeeId = item.EmployeeId.Value;
                                    empAttObj.InTime = DateTime.Parse(item.Year.Value + "-" + item.Month.Value + "-" + dateCount + " " + "03:30:00");
                                    int sdsd = empAttObj.InTime.Value.Hour + AttCount % 8;
                                    empAttObj.OutTime = DateTime.Parse(item.Year.Value + "-" + item.Month.Value + "-" + dateCount + " " + "" + sdsd + ":30:00");
                                    empAttObj.TotalWorkTime = (AttCount % 8).ToString();
                                    empAttObj.HoverTimeHours = 0;
                                    empAttObj.AttendanceTypeId = 1; //P
                                    empAttObj.Date = dateTime;
                                    empAttObj.FinancialYearId = financialYearList.FirstOrDefault(x => x.StartDate.Year == itemYear.Key.Year.Value).FinancialYearId;//item.Year == 2003 ? 

                                    empAttList.Add(empAttObj);
                                    _uow.EmployeeAttendanceRepository.Add(empAttObj);
                                    //att = att + (AttCount % 8);
                                    AttCount = AttCount - (AttCount % 8);
                                }

                                dateCount++;
                            }

                            // Leave
                            //int lea = 0;
                            while (LeaCount > 0)
                            {
                                DateTime dateTime = DateTime.Parse(item.Month.Value + "/" + dateCount + "/" + item.Year.Value);

                                if (LeaCount % 8 == 0)
                                {
                                    EmployeeAttendance empAttObj = new EmployeeAttendance();

                                    empAttObj.IsDeleted = false;
                                    empAttObj.EmployeeId = item.EmployeeId.Value;
                                    empAttObj.InTime = DateTime.Parse(item.Year.Value + "-" + item.Month.Value + "-" + dateCount + " " + "03:30:00");
                                    empAttObj.OutTime = DateTime.Parse(item.Year.Value + "-" + item.Month.Value + "-" + dateCount + " " + "11:30:00");
                                    empAttObj.TotalWorkTime = 8.ToString();
                                    empAttObj.HoverTimeHours = 0;
                                    empAttObj.AttendanceTypeId = 3; //L
                                    empAttObj.Date = dateTime;
                                    empAttObj.FinancialYearId = financialYearList.FirstOrDefault(x => x.StartDate.Year == itemYear.Key.Year.Value).FinancialYearId; //item.Year == 2003 ? 

                                    empAttList.Add(empAttObj);
                                    _uow.EmployeeAttendanceRepository.Add(empAttObj);

                                    //lea = lea + 8;
                                    LeaCount = LeaCount - 8;
                                }
                                else
                                {
                                    EmployeeAttendance empAttObj = new EmployeeAttendance();

                                    empAttObj.IsDeleted = false;
                                    empAttObj.EmployeeId = item.EmployeeId.Value;
                                    empAttObj.InTime = DateTime.Parse(item.Year.Value + "-" + item.Month.Value + "-" + dateCount + " " + "03:30:00");
                                    int sdsd = empAttObj.InTime.Value.Hour + AttCount % 8;
                                    empAttObj.OutTime = DateTime.Parse(item.Year.Value + "-" + item.Month.Value + "-" + dateCount + " " + "" + sdsd + ":30:00");
                                    empAttObj.TotalWorkTime = (LeaCount % 8).ToString();
                                    empAttObj.HoverTimeHours = 0;
                                    empAttObj.AttendanceTypeId = 3; //L
                                    empAttObj.Date = dateTime;
                                    empAttObj.FinancialYearId = financialYearList.FirstOrDefault(x => x.StartDate.Year == itemYear.Key.Year.Value).FinancialYearId; //item.Year == 2003 ? 

                                    empAttList.Add(empAttObj);
                                    _uow.EmployeeAttendanceRepository.Add(empAttObj);

                                    LeaCount = LeaCount - (LeaCount % 8);
                                    //lea = lea + (LeaCount % 8);
                                }

                                dateCount++;
                            }

                            // Absent
                            //int abs = 0;
                            while (AbsCount > 0)
                            {
                                DateTime dateTime = DateTime.Parse(item.Month.Value + "/" + dateCount + "/" + item.Year.Value);

                                if (AbsCount % 8 == 0)
                                {
                                    EmployeeAttendance empAttObj = new EmployeeAttendance();

                                    empAttObj.IsDeleted = false;
                                    empAttObj.EmployeeId = item.EmployeeId.Value;
                                    empAttObj.InTime = DateTime.Parse(item.Year.Value + "-" + item.Month.Value + "-" + dateCount + " " + "03:30:00");
                                    empAttObj.OutTime = DateTime.Parse(item.Year.Value + "-" + item.Month.Value + "-" + dateCount + " " + "11:30:00");
                                    empAttObj.TotalWorkTime = 8.ToString();
                                    empAttObj.HoverTimeHours = 0;
                                    empAttObj.AttendanceTypeId = 2; //A
                                    empAttObj.Date = dateTime;
                                    empAttObj.FinancialYearId = financialYearList.FirstOrDefault(x => x.StartDate.Year == itemYear.Key.Year.Value).FinancialYearId; //item.Year == 2003 ? 

                                    empAttList.Add(empAttObj);
                                    _uow.EmployeeAttendanceRepository.Add(empAttObj);
                                    AbsCount = AbsCount - 8;
                                    //abs = abs + 8;
                                }
                                else
                                {
                                    EmployeeAttendance empAttObj = new EmployeeAttendance();

                                    empAttObj.IsDeleted = false;
                                    empAttObj.EmployeeId = item.EmployeeId.Value;
                                    empAttObj.InTime = DateTime.Parse(item.Year.Value + "-" + item.Month.Value + "-" + dateCount + " " + "03:30:00");
                                    int sdsd = empAttObj.InTime.Value.Hour + AttCount % 8;
                                    empAttObj.OutTime = DateTime.Parse(item.Year.Value + "-" + item.Month.Value + "-" + dateCount + " " + "" + sdsd + ":30:00");
                                    empAttObj.TotalWorkTime = (AbsCount % 8).ToString();
                                    empAttObj.HoverTimeHours = 0;
                                    empAttObj.AttendanceTypeId = 2; //A
                                    empAttObj.Date = dateTime;
                                    empAttObj.FinancialYearId = financialYearList.FirstOrDefault(x => x.StartDate.Year == itemYear.Key.Year.Value).FinancialYearId; //item.Year == 2003 ? 

                                    empAttList.Add(empAttObj);
                                    _uow.EmployeeAttendanceRepository.Add(empAttObj);

                                    AbsCount = AbsCount - (AbsCount % 8);

                                    //abs = abs + (AbsCount % 8);

                                }

                                dateCount++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return "OK";
        }
        #endregion

        public string OfficeHours(int iYear)
        {
            //int month = 1;
            //int year = iYear;



            List<PayrollMonthlyHourDetail> xPayrollMonthlyHourDetailList = new List<PayrollMonthlyHourDetail>();

            try
            {
                for (int xyear = iYear; xyear <= 2018; xyear++)
                {
                    for (int month = 1; month <= 12; month++)
                    {

                        int daysInMonthMinusFridayAndSaturday = 0;

                        for (int i = 1; i <= DateTime.DaysInMonth(xyear, month); i++)
                        {
                            DateTime thisDay = new DateTime(xyear, month, i);
                            if (thisDay.DayOfWeek != DayOfWeek.Friday && thisDay.DayOfWeek != DayOfWeek.Saturday)
                            {
                                daysInMonthMinusFridayAndSaturday += 1;
                            }
                        }

                        //int MonthAttendance = daysInMonthMinusFridayAndSaturday * 8;

                        ICollection<OfficeDetail> xOfficeDetailList = _uow.OfficeDetailRepository.FindAll(x => x.IsDeleted == false);

                        foreach (OfficeDetail OfficeTime in xOfficeDetailList)
                        {
                            PayrollMonthlyHourDetail xPayrollMonthlyHourDetail = new PayrollMonthlyHourDetail();
                            xPayrollMonthlyHourDetail.IsDeleted = false;
                            xPayrollMonthlyHourDetail.PayrollMonth = month;
                            xPayrollMonthlyHourDetail.WorkingTime = daysInMonthMinusFridayAndSaturday * 8;
                            xPayrollMonthlyHourDetail.OfficeId = OfficeTime.OfficeId;
                            xPayrollMonthlyHourDetail.PayrollMonth = month;
                            xPayrollMonthlyHourDetail.PayrollYear = xyear;
                            xPayrollMonthlyHourDetail.Hours = 8;
                            xPayrollMonthlyHourDetail.InTime = new DateTime(xyear, month, 1, 3, 30, 0);
                            xPayrollMonthlyHourDetail.OutTime = new DateTime(xyear, month, 1, 11, 30, 0);
                            xPayrollMonthlyHourDetailList.Add(xPayrollMonthlyHourDetail);
                        }
                    }
                }


            }
            catch (Exception xException)
            {

            }

            _uow.GetDbContext().PayrollMonthlyHourDetail.AddRange(xPayrollMonthlyHourDetailList);
            _uow.GetDbContext().SaveChanges();

            return "200 OK";
        }

        public async Task<string> TransferDataForVoucherTransaction2008()
        {
            try
            {
                var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date.Year == 2008 && x.OfficeCode == "KBL").OrderByDescending(x => x.Date).ToListAsync();
                var transactionData = await _uow.GetDbContext().VoucherTransactions.Where(x => x.IsDeleted == false && x.PKRAmount == null).OrderBy(x => x.TransactionId).Skip(12000).Take(6000).ToListAsync();

                List<VoucherTransactions> lst = new List<VoucherTransactions>();

                Dictionary<string, DateTime> dateList = new Dictionary<string, DateTime>();

                #region "LOGIC"
                foreach (var item in transactionData)
                {
                    VoucherTransactions obj = new VoucherTransactions();
                    obj = item;
                    if (item.CurrencyId == (int)Currency.AFG)
                    {
                        obj.AFGAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble(item.Debit), 4) : Math.Round(Convert.ToDouble(item.Credit), 4);

                        var exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.AFG);
                        if (exchangeRateToAFG == null)
                        {
                            var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                            if (exchangeRateToAFG == null)
                            {
                                exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                            }
                        }

                        var exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.EUR);
                        if (exchangeRateToEuro == null)
                        {
                            var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                            if (exchangeRateToEuro == null)
                            {
                                exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                            }
                        }

                        var exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.USD);
                        if (exchangeRateToUSD == null)
                        {
                            var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                            if (exchangeRateToUSD == null)
                            {
                                exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                            }
                        }


                        obj.EURAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit * exchangeRateToAFG.Rate) / (exchangeRateToEuro.Rate)), 4) : Math.Round(Convert.ToDouble((item.Credit * exchangeRateToAFG.Rate) / (exchangeRateToEuro.Rate)), 4);
                        obj.PKRAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit * exchangeRateToAFG.Rate)), 4) : Math.Round(Convert.ToDouble((item.Credit * exchangeRateToAFG.Rate)), 4);
                        obj.USDAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit * exchangeRateToAFG.Rate) / (exchangeRateToUSD.Rate)), 4) : Math.Round(Convert.ToDouble((item.Credit * exchangeRateToAFG.Rate) / (exchangeRateToUSD.Rate)), 4);
                    }

                    if (item.CurrencyId == (int)Currency.EUR)
                    {
                        obj.EURAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble(item.Debit), 4) : Math.Round(Convert.ToDouble(item.Credit), 4);

                        var exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.AFG);
                        if (exchangeRateToAFG == null)
                        {
                            var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                            if (exchangeRateToAFG == null)
                            {
                                exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                            }
                        }

                        var exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.EUR);
                        if (exchangeRateToEuro == null)
                        {
                            var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                            if (exchangeRateToEuro == null)
                            {
                                exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                            }
                        }

                        var exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.USD);
                        if (exchangeRateToUSD == null)
                        {
                            var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                            if (exchangeRateToUSD == null)
                            {
                                exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                            }
                        }


                        obj.AFGAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit * exchangeRateToEuro.Rate) / (exchangeRateToAFG.Rate)), 4) : Math.Round(Convert.ToDouble((item.Credit * exchangeRateToEuro.Rate) / (exchangeRateToAFG.Rate)), 4);
                        obj.PKRAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit * exchangeRateToEuro.Rate)), 4) : Math.Round(Convert.ToDouble((item.Credit * exchangeRateToEuro.Rate)), 4);
                        obj.USDAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit * exchangeRateToEuro.Rate) / (exchangeRateToUSD.Rate)), 4) : Math.Round(Convert.ToDouble((item.Credit * exchangeRateToEuro.Rate) / (exchangeRateToUSD.Rate)), 4);
                    }

                    if (item.CurrencyId == (int)Currency.PKR)
                    {

                        obj.PKRAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble(item.Debit), 4) : Math.Round(Convert.ToDouble(item.Credit), 4);

                        var exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.AFG);
                        if (exchangeRateToAFG == null)
                        {
                            var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                            if (exchangeRateToAFG == null)
                            {
                                exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                            }
                        }

                        var exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.EUR);
                        if (exchangeRateToEuro == null)
                        {
                            var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                            if (exchangeRateToEuro == null)
                            {
                                exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                            }
                        }

                        var exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.USD);
                        if (exchangeRateToUSD == null)
                        {
                            var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                            if (exchangeRateToUSD == null)
                            {
                                exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                            }
                        }


                        obj.AFGAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit) / (exchangeRateToAFG.Rate)), 4) : Math.Round(Convert.ToDouble((item.Credit) / (exchangeRateToAFG.Rate)), 4);
                        obj.EURAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit) / (exchangeRateToEuro.Rate)), 4) : Math.Round(Convert.ToDouble((item.Credit) / (exchangeRateToEuro.Rate)), 4);
                        obj.USDAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit) / (exchangeRateToUSD.Rate)), 4) : Math.Round(Convert.ToDouble((item.Credit) / (exchangeRateToUSD.Rate)), 4);
                    }

                    if (item.CurrencyId == (int)Currency.USD)
                    {
                        obj.USDAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble(item.Debit), 4) : Math.Round(Convert.ToDouble(item.Credit), 4);

                        var exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.AFG);
                        if (exchangeRateToAFG == null)
                        {
                            var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                            if (exchangeRateToAFG == null)
                            {
                                exchangeRateToAFG = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.AFG);
                            }
                        }

                        var exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.EUR);
                        if (exchangeRateToEuro == null)
                        {
                            var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                            if (exchangeRateToEuro == null)
                            {
                                exchangeRateToEuro = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.EUR);
                            }
                        }

                        var exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == (int)Currency.USD);
                        if (exchangeRateToUSD == null)
                        {
                            var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                            exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                            if (exchangeRateToUSD == null)
                            {
                                exchangeRateToUSD = exchangeRate.FirstOrDefault(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == (int)Currency.USD);
                            }
                        }


                        obj.AFGAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit * exchangeRateToUSD.Rate) / (exchangeRateToAFG.Rate)), 4) : Math.Round(Convert.ToDouble((item.Credit * exchangeRateToUSD.Rate) / (exchangeRateToAFG.Rate)), 4);
                        obj.PKRAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit * exchangeRateToUSD.Rate)), 4) : Math.Round(Convert.ToDouble((item.Credit * exchangeRateToUSD.Rate)), 4);
                        obj.EURAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit * exchangeRateToUSD.Rate) / (exchangeRateToEuro.Rate)), 4) : Math.Round(Convert.ToDouble((item.Credit * exchangeRateToUSD.Rate) / (exchangeRateToEuro.Rate)), 4);
                    }

                    //lst.Add(obj);
                    _uow.GetDbContext().VoucherTransactions.Update(obj);
                    await _uow.SaveAsync();
                }
                #endregion

                #region "LOGIC"
                //foreach (var item in transactionData)
                //{
                //	VoucherTransactions obj = new VoucherTransactions();
                //	obj = item;
                //	if (item.CurrencyId == 1)
                //	{
                //		obj.TransactionId = item.TransactionId;
                //		obj.AFGAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble(item.Debit),4) : Math.Round(Convert.ToDouble(item.Credit),4);

                //		var exchangeRateToAFG = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == 1).FirstOrDefaultAsync();
                //		if (exchangeRateToAFG == null)
                //		{
                //			var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                //			exchangeRateToAFG = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == 1).FirstOrDefaultAsync();
                //			if (exchangeRateToAFG == null)
                //			{
                //				exchangeRateToAFG = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == 1).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
                //			}
                //		}

                //		var exchangeRateToEuro = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == 2).FirstOrDefaultAsync();
                //		if (exchangeRateToEuro == null)
                //		{
                //			var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                //			exchangeRateToEuro = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == 2).FirstOrDefaultAsync();
                //			if (exchangeRateToEuro == null)
                //			{
                //				exchangeRateToEuro = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == 2).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
                //			}
                //		}

                //		var exchangeRateToUSD = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == 4).FirstOrDefaultAsync();
                //		if (exchangeRateToUSD == null)
                //		{
                //			var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                //			exchangeRateToUSD = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == 4).FirstOrDefaultAsync();
                //			if (exchangeRateToUSD == null)
                //			{
                //				exchangeRateToUSD = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == 4).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
                //			}
                //		}


                //		obj.EURAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit * exchangeRateToAFG.Rate) / (exchangeRateToEuro.Rate)),4) : Math.Round(Convert.ToDouble((item.Credit * exchangeRateToAFG.Rate ) / (exchangeRateToEuro.Rate)),4);
                //		obj.PKRAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit * exchangeRateToAFG.Rate)),4) : Math.Round(Convert.ToDouble((item.Credit * exchangeRateToAFG.Rate)),4);
                //		obj.USDAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit * exchangeRateToAFG.Rate) / (exchangeRateToUSD.Rate)),4) : Math.Round(Convert.ToDouble((item.Credit * exchangeRateToAFG.Rate) / (exchangeRateToUSD.Rate)),4);
                //	}

                //	if (item.CurrencyId == 2)
                //	{
                //		obj.TransactionId = item.TransactionId;
                //		obj.EURAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble(item.Debit),4) : Math.Round(Convert.ToDouble(item.Credit),4);

                //		var exchangeRateToAFG = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == 1).FirstOrDefaultAsync();
                //		if (exchangeRateToAFG == null)
                //		{
                //			var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                //			exchangeRateToAFG = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == 1).FirstOrDefaultAsync();
                //			if (exchangeRateToAFG == null)
                //			{
                //				exchangeRateToAFG = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == 1).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
                //			}
                //		}

                //		var exchangeRateToEuro = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == 2).FirstOrDefaultAsync();
                //		if (exchangeRateToEuro == null)
                //		{
                //			var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                //			exchangeRateToEuro = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == 2).FirstOrDefaultAsync();
                //			if (exchangeRateToEuro == null)
                //			{
                //				exchangeRateToEuro = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == 2).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
                //			}
                //		}

                //		var exchangeRateToUSD = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == 4).FirstOrDefaultAsync();
                //		if (exchangeRateToUSD == null)
                //		{
                //			var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                //			exchangeRateToUSD = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == 4).FirstOrDefaultAsync();
                //			if (exchangeRateToUSD == null)
                //			{
                //				exchangeRateToUSD = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == 4).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
                //			}
                //		}


                //		obj.AFGAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit * exchangeRateToEuro.Rate) / (exchangeRateToAFG.Rate)),4) : Math.Round(Convert.ToDouble((item.Credit * exchangeRateToEuro.Rate) / (exchangeRateToAFG.Rate)),4);
                //		obj.PKRAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit * exchangeRateToEuro.Rate)),4) : Math.Round(Convert.ToDouble((item.Credit * exchangeRateToEuro.Rate)),4);
                //		obj.USDAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit * exchangeRateToEuro.Rate) / (exchangeRateToUSD.Rate)),4) : Math.Round(Convert.ToDouble((item.Credit * exchangeRateToEuro.Rate) / (exchangeRateToUSD.Rate)),4);
                //	}

                //	if (item.CurrencyId == 3)
                //	{
                //		obj.TransactionId = item.TransactionId;
                //		obj.PKRAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble(item.Debit),4) : Math.Round(Convert.ToDouble(item.Credit),4);

                //		var exchangeRateToAFG = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == 1).FirstOrDefaultAsync();
                //		if (exchangeRateToAFG == null)
                //		{
                //			var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                //			exchangeRateToAFG = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == 1).FirstOrDefaultAsync();
                //			if (exchangeRateToAFG == null)
                //			{
                //				exchangeRateToAFG = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == 1).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
                //			}
                //		}

                //		var exchangeRateToEuro = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == 2).FirstOrDefaultAsync();
                //		if (exchangeRateToEuro == null)
                //		{
                //			var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                //			exchangeRateToEuro = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == 2).FirstOrDefaultAsync();
                //			if (exchangeRateToEuro == null)
                //			{
                //				exchangeRateToEuro = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == 2).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
                //			}
                //		}

                //		var exchangeRateToUSD = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == 4).FirstOrDefaultAsync();
                //		if (exchangeRateToUSD == null)
                //		{
                //			var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                //			exchangeRateToUSD = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == 4).FirstOrDefaultAsync();
                //			if (exchangeRateToUSD == null)
                //			{
                //				exchangeRateToUSD = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == 4).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
                //			}
                //		}


                //		obj.AFGAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit ) / (exchangeRateToAFG.Rate)),4) : Math.Round(Convert.ToDouble((item.Credit ) / (exchangeRateToAFG.Rate)),4);
                //		obj.EURAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit) / (exchangeRateToEuro.Rate)),4) : Math.Round(Convert.ToDouble((item.Credit) / (exchangeRateToEuro.Rate)),4);
                //		obj.USDAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit ) / (exchangeRateToUSD.Rate)),4) : Math.Round(Convert.ToDouble((item.Credit ) / (exchangeRateToUSD.Rate)),4);
                //	}	

                //	if (item.CurrencyId == 4)
                //	{
                //		obj.TransactionId = item.TransactionId;
                //		obj.USDAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble(item.Debit),4) : Math.Round(Convert.ToDouble(item.Credit),4);

                //		var exchangeRateToAFG = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == 1).FirstOrDefaultAsync();
                //		if (exchangeRateToAFG == null)
                //		{
                //			var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                //			exchangeRateToAFG = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == 1).FirstOrDefaultAsync();
                //			if (exchangeRateToAFG == null)
                //			{
                //				exchangeRateToAFG = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == 1).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
                //			}
                //		}

                //		var exchangeRateToEuro = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == 2).FirstOrDefaultAsync();
                //		if (exchangeRateToEuro == null)
                //		{
                //			var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                //			exchangeRateToEuro = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == 2).FirstOrDefaultAsync();
                //			if (exchangeRateToEuro == null)
                //			{
                //				exchangeRateToEuro = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == 2).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
                //			}
                //		}

                //		var exchangeRateToUSD = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == item.TransactionDate.Value.Date && x.FromCurrency == 4).FirstOrDefaultAsync();
                //		if (exchangeRateToUSD == null)
                //		{
                //			var dateForExchangeRate = item.TransactionDate.Value.Date.AddDays(-1);
                //			exchangeRateToUSD = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date == dateForExchangeRate.Date && x.FromCurrency == 4).FirstOrDefaultAsync();
                //			if (exchangeRateToUSD == null)
                //			{
                //				exchangeRateToUSD = await _uow.GetDbContext().ExchangeRates.Where(x => x.Date.Value.Date <= dateForExchangeRate.Date && x.FromCurrency == 4).OrderByDescending(x => x.Date).FirstOrDefaultAsync();
                //			}
                //		}


                //		obj.AFGAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit * exchangeRateToUSD.Rate) / (exchangeRateToAFG.Rate)), 4) : Math.Round(Convert.ToDouble((item.Credit * exchangeRateToUSD.Rate) / (exchangeRateToAFG.Rate)), 4);
                //		obj.PKRAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit * exchangeRateToUSD.Rate)),4) : Math.Round(Convert.ToDouble((item.Credit * exchangeRateToUSD.Rate)),4);
                //		obj.EURAmount = item.Debit != 0 ? Math.Round(Convert.ToDouble((item.Debit * exchangeRateToUSD.Rate) / (exchangeRateToEuro.Rate)),4) : Math.Round(Convert.ToDouble((item.Credit * exchangeRateToUSD.Rate) / (exchangeRateToEuro.Rate)),4);
                //	}

                //	//lst.Add(obj);
                //	_uow.GetDbContext().VoucherTransactions.Update(obj);
                //	await _uow.SaveAsync();
                //}
                #endregion
                //_uow.GetDbContext().VoucherTransactions.UpdateRange(lst);
                //await _uow.SaveAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return "OK";
        }


        public string TransformExchangeRatesToFromCurrency()
        {
            try
            {
                List<ExchangeRate> exchangeRates = _uow.GetDbContext().ExchangeRates.Where(x => x.OfficeCode == "KBL" && x.Date == new DateTime(2016, 10, 30)).OrderByDescending(x => x.Date).ToList();

                foreach (ExchangeRate rate in exchangeRates)
                {
                    List<ExchangeRate> ExchangeRatesonDate = exchangeRates.Where(x => x.Date == rate.Date).ToList();

                    List<ExchangeRateDetail> ExchangeRateDetailList = new List<ExchangeRateDetail>();

                    for (int i = 1; i <= 4; i++)
                    {
                        ExchangeRateDetail exchangeRateDetail = new ExchangeRateDetail();

                        exchangeRateDetail.Date = rate.Date.Value;
                        exchangeRateDetail.OfficeId = rate.OfficeId.Value;
                        exchangeRateDetail.IsDeleted = false;

                        exchangeRateDetail.FromCurrency = rate.FromCurrency.Value;
                        exchangeRateDetail.ToCurrency = i;
                        exchangeRateDetail.Rate = Convert.ToDecimal(ExchangeRatesonDate.FirstOrDefault(x => x.FromCurrency == rate.FromCurrency.Value).Rate / ExchangeRatesonDate.FirstOrDefault(x => x.FromCurrency == i).Rate);
                        ExchangeRateDetailList.Add(exchangeRateDetail);
                    }

                    _uow.GetDbContext().ExchangeRateDetail.AddRange(ExchangeRateDetailList);
                    _uow.GetDbContext().SaveChanges();
                }
            }
            catch (Exception ex)
            {

                return "Exception occured " + ex.Message;

            }

            return "Conversion Done";

        }
    }
}
