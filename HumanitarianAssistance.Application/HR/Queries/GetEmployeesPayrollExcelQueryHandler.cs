using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Common.Enums;
using System.Collections.Generic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Domain.Entities.HR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeesPayrollExcelQueryHandler : IRequestHandler<GetEmployeesPayrollExcelQuery, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IExcelExportService _excelExportService;

        public GetEmployeesPayrollExcelQueryHandler(IAccountingServices iAccountingServices,
                HumanitarianAssistanceDbContext dbContext, IExcelExportService excelExportService)
        {
            _dbContext = dbContext;
            _excelExportService = excelExportService;
        }
        public async Task<object> Handle(GetEmployeesPayrollExcelQuery request, CancellationToken cancellationToken)
        {
            try
            {
                EmployeesPayrollExcelModel model = new EmployeesPayrollExcelModel();

                var financialYear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.IsDefault == true);

                var query = _dbContext.EmployeePayrollInfoDetail
                                            .Include(x => x.EmployeeDetail)
                                            .ThenInclude(x => x.EmployeeProfessionalDetail)
                                            .ThenInclude(x => x.OfficeDetail)
                                            .Include(x => x.EmployeeDetail)
                                            .ThenInclude(x => x.EmployeeProfessionalDetail)
                                            .ThenInclude(x => x.DesignationDetails)
                                            .Include(x => x.EmployeeDetail)
                                            .ThenInclude(x => x.EmployeeBasicSalaryDetail)
                                            .ThenInclude(x => x.CurrencyDetails)
                                            .Include(x => x.EmployeeDetail)
                                            .ThenInclude(x => x.EmployeeAttendance)
                                            .Include(x => x.EmployeeDetail)
                                            .ThenInclude(x => x.EmployeeAnalyticalList)
                                            .ThenInclude(x => x.ProjectDetail)
                                            .Include(x => x.EmployeeDetail)
                                            .ThenInclude(x => x.EmployeeAnalyticalList)
                                            .ThenInclude(x => x.ProjectBudgetLine)
                                            .Include(x => x.EmployeeDetail)
                                            .ThenInclude(x => x.EmployeeAnalyticalList)
                                            .ThenInclude(x => x.ProjectBudgetLine)
                                            .ThenInclude(x => x.ProjectJobDetail)
                                            .Include(x => x.EmployeeDetail)
                                            .ThenInclude(x => x.AccumulatedSalaryHeadDetailList)
                                            .Include(x => x.EmployeeDetail)
                                            .ThenInclude(x => x.EmployeeBonusFineSalaryHeadList)
                                            .Where(x => x.IsDeleted == false && x.EmployeeDetail.IsDeleted == false &&
                                            request.Month.Contains(x.Month) && x.Year == financialYear.StartDate.Year &&
                                            request.OfficeId.Contains(x.EmployeeDetail.EmployeeProfessionalDetail.OfficeId.Value)).AsQueryable();

                if (request.SelectedEmployees.Any())
                {
                    query = query.Where(x => request.SelectedEmployees.Contains(x.EmployeeId));

                    if (request.SelectedEmployees.Count != query.Select(x => x.EmployeeDetail).Distinct().Count())
                    {
                        return "Please ensure approved payroll is present for selected employees and months";
                    }
                }

                if (!string.IsNullOrEmpty(request.EmployeeName))
                {
                    query = query.Where(x => x.EmployeeDetail.EmployeeName.Contains(request.EmployeeName));
                }

                if (!string.IsNullOrEmpty(request.EmployeeCode))
                {
                    query = query.Where(x => x.EmployeeDetail.EmployeeCode.Contains(request.EmployeeCode));
                }

                if (request.Sex.HasValue && request.Sex != 0)
                {
                    query = query.Where(x => x.EmployeeDetail.SexId == request.Sex.Value);
                }

                if (request.EmploymentStatus.HasValue && request.EmploymentStatus != 0)
                {
                    query = query.Where(x => x.EmployeeDetail.EmployeeProfessionalDetail.EmployeeTypeId == request.EmploymentStatus.Value);
                }

                if (!string.IsNullOrEmpty(request.Project))
                {
                    query = query.Where(x => x.EmployeeDetail.EmployeeAnalyticalList.Count > 0 && (x.EmployeeDetail.EmployeeAnalyticalList
                                    .Select(y => y.ProjectDetail.ProjectName.Contains(request.Project) ||
                                    y.ProjectDetail.ProjectCode.Contains(request.Project)).Count() > 0)
                                 );
                }

                var office = await _dbContext.OfficeDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && request.OfficeId.Contains(x.OfficeId));

                string months = string.Empty;

                for (int i = 0; i < request.Month.Count; i++)
                {
                    months += CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(request.Month[i].Value) + ((i != request.Month.Count - 1) ? "," : string.Empty);
                }

                model.HeaderAndFooter = new HeaderAndFooter
                {
                    Months = months,
                    Header = "Coordination of Humanitarian Assistance",
                    SubHeader = "Payroll Report",
                    Office = office.OfficeCode
                };

                var data = query.Select(x => new PayrollExcelData
                {
                    EmployeeId = x.EmployeeId,
                    Name = x.EmployeeDetail.EmployeeName,
                    EmployeeCode = x.EmployeeDetail.EmployeeCode,
                    Designation = x.EmployeeDetail.EmployeeProfessionalDetail.DesignationDetails != null ?
                                                x.EmployeeDetail.EmployeeProfessionalDetail.DesignationDetails.Designation : "",
                    Gender = (x.EmployeeDetail.SexId.HasValue ? (x.EmployeeDetail.SexId == (int)Gender.MALE ? "Male" :
                                                              x.EmployeeDetail.SexId == (int)Gender.FEMALE ? "Female" : "Other") : ""),

                    CurrencyId = x.EmployeeDetail.EmployeeBasicSalaryDetail.CurrencyId,
                    Currency = x.EmployeeDetail.EmployeeBasicSalaryDetail.CurrencyId.HasValue ?
                              x.EmployeeDetail.EmployeeBasicSalaryDetail.CurrencyDetails.CurrencyName : "",
                    OfficeId = x.EmployeeDetail.EmployeeProfessionalDetail.OfficeId,
                    Office = x.EmployeeDetail.EmployeeProfessionalDetail.OfficeDetail.OfficeCode,
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x.Month),
                    BasicPay = x.EmployeeDetail.EmployeeBasicSalaryDetail.BasicSalary,
                    AttendedHours = x.EmployeeDetail.EmployeeAttendance.Where(y => y.IsDeleted == false &&
                                    y.Date.Month == x.Month && y.Date.Year == x.Year && y.AttendanceTypeId == (int)AttendanceType.P)
                                    .Select(z => z.OutTime.Value.Subtract(z.InTime.Value).Hours).DefaultIfEmpty(0).Sum(),
                    AbsentDays = x.EmployeeDetail.EmployeeAttendance.Where(y => y.IsDeleted == false &&
                                    y.Date.Month == x.Month && y.Date.Year == x.Year && y.AttendanceTypeId == (int)AttendanceType.A)
                                    .Count(),
                    AbsentHours = 0,
                    NetSalary = 0,
                    MonthNumber = x.Month,
                    HourlyRate = x.HourlyRate,
                    Salary = x.EmployeeDetail.AccumulatedSalaryHeadDetailList.FirstOrDefault(y => y.IsDeleted == false &&
                             y.Month == x.Month && y.Year == x.Year && y.SalaryComponentId == (int)AccumulatedSalaryHead.GrossSalary) != null ?
                            x.EmployeeDetail.AccumulatedSalaryHeadDetailList.FirstOrDefault(y => y.IsDeleted == false &&
                            y.Month == x.Month && y.Year == x.Year && y.SalaryComponentId == (int)AccumulatedSalaryHead.GrossSalary).SalaryDeduction : 0,
                    GrossSalary = x.EmployeeDetail.AccumulatedSalaryHeadDetailList.FirstOrDefault(y => y.IsDeleted == false &&
                             y.Month == x.Month && y.Year == x.Year && y.SalaryComponentId == (int)AccumulatedSalaryHead.GrossSalary) != null ?
                            x.EmployeeDetail.AccumulatedSalaryHeadDetailList.FirstOrDefault(y => y.IsDeleted == false &&
                            y.Month == x.Month && y.Year == x.Year && y.SalaryComponentId == (int)AccumulatedSalaryHead.GrossSalary).SalaryDeduction : 0,
                    Bonus = x.EmployeeDetail.EmployeeBonusFineSalaryHeadList.Any(y => y.IsDeleted == false &&
                             y.Month == x.Month && y.Year == x.Year && y.TransactionTypeId == (int)TransactionType.Debit) ?
                            x.EmployeeDetail.EmployeeBonusFineSalaryHeadList.Where(y => y.IsDeleted == false &&
                            y.Month == x.Month && y.Year == x.Year && y.TransactionTypeId == (int)TransactionType.Debit).Select(z => z.Amount)
                            .DefaultIfEmpty(0).Sum() : 0,
                    CapacityBuilding = x.EmployeeDetail.AccumulatedSalaryHeadDetailList.FirstOrDefault(y => y.IsDeleted == false &&
                             y.Month == x.Month && y.Year == x.Year && y.SalaryComponentId == (int)AccumulatedSalaryHead.CapacityBuilding) != null ?
                            x.EmployeeDetail.AccumulatedSalaryHeadDetailList.FirstOrDefault(y => y.IsDeleted == false &&
                            y.Month == x.Month && y.Year == x.Year && y.SalaryComponentId == (int)AccumulatedSalaryHead.CapacityBuilding).SalaryDeduction : 0,
                    Security = x.EmployeeDetail.AccumulatedSalaryHeadDetailList.FirstOrDefault(y => y.IsDeleted == false &&
                             y.Month == x.Month && y.Year == x.Year && y.SalaryComponentId == (int)AccumulatedSalaryHead.Security) != null ?
                            x.EmployeeDetail.AccumulatedSalaryHeadDetailList.FirstOrDefault(y => y.IsDeleted == false &&
                            y.Month == x.Month && y.Year == x.Year && y.SalaryComponentId == (int)AccumulatedSalaryHead.Security).SalaryDeduction : 0,
                    SalaryTax = x.EmployeeDetail.AccumulatedSalaryHeadDetailList.FirstOrDefault(y => y.IsDeleted == false &&
                            y.Month == x.Month && y.Year == x.Year && y.SalaryComponentId == (int)AccumulatedSalaryHead.SalaryTax) != null ?
                            x.EmployeeDetail.AccumulatedSalaryHeadDetailList.FirstOrDefault(y => y.IsDeleted == false &&
                            y.Month == x.Month && y.Year == x.Year && y.SalaryComponentId == (int)AccumulatedSalaryHead.SalaryTax).SalaryDeduction : 0,
                    Fine = x.EmployeeDetail.EmployeeBonusFineSalaryHeadList.Any(y => y.IsDeleted == false &&
                           y.Month == x.Month && y.Year == x.Year && y.TransactionTypeId == (int)TransactionType.Credit) ?
                            x.EmployeeDetail.EmployeeBonusFineSalaryHeadList.Where(y => y.IsDeleted == false &&
                            y.Month == x.Month && y.Year == x.Year && y.TransactionTypeId == (int)TransactionType.Credit).Select(z => z.Amount)
                            .DefaultIfEmpty(0).Sum() : 0,
                    Advance = x.EmployeeDetail.AccumulatedSalaryHeadDetailList.FirstOrDefault(y => y.IsDeleted == false &&
                            y.Month == x.Month && y.Year == x.Year && y.SalaryComponentId == (int)AccumulatedSalaryHead.AdvanceRecovery) != null ?
                            x.EmployeeDetail.AccumulatedSalaryHeadDetailList.FirstOrDefault(y => y.IsDeleted == false &&
                            y.Month == x.Month && y.Year == x.Year && y.SalaryComponentId == (int)AccumulatedSalaryHead.AdvanceRecovery).SalaryDeduction : 0,
                    Pension = x.EmployeeDetail.AccumulatedSalaryHeadDetailList.FirstOrDefault(y => y.IsDeleted == false &&
                             y.Month == x.Month && y.Year == x.Year && y.SalaryComponentId == (int)AccumulatedSalaryHead.Pension) != null ?
                            x.EmployeeDetail.AccumulatedSalaryHeadDetailList.FirstOrDefault(y => y.IsDeleted == false &&
                            y.Month == x.Month && y.Year == x.Year && y.SalaryComponentId == (int)AccumulatedSalaryHead.Pension).SalaryDeduction : 0,
                    EmployeeAnalyticalInfoList = (!string.IsNullOrEmpty(request.Project) && x.EmployeeDetail.EmployeeAnalyticalList.Count > 0) ?
                                                (x.EmployeeDetail.EmployeeAnalyticalList.Where(r => r.IsDeleted == false &&
                                                (r.ProjectDetail.ProjectName.Contains(request.Project) || r.ProjectDetail.ProjectCode.Contains(request.Project)))
                                                .Select(z => new EmployeeAnalyticalInfo
                                                {
                                                    Percentage = z.SalaryPercentage,
                                                    BudgetLine = z.ProjectBudgetLine != null ? (z.ProjectBudgetLine.BudgetCode + z.ProjectBudgetLine.BudgetName) : "",
                                                    Project = z.ProjectDetail != null ? (z.ProjectDetail.ProjectCode + z.ProjectDetail.ProjectName) : "",
                                                    Job = (z.ProjectBudgetLine != null && z.ProjectBudgetLine.ProjectJobDetail != null) ? z.ProjectBudgetLine.ProjectJobDetail.ProjectJobCode : ""
                                                }).ToList()) : x.EmployeeDetail.EmployeeAnalyticalList.Count > 0 ?
                                                (x.EmployeeDetail.EmployeeAnalyticalList.Where(t => x.IsDeleted == false).Select(z => new EmployeeAnalyticalInfo
                                                {
                                                    Percentage = z.SalaryPercentage,
                                                    BudgetLine = z.ProjectBudgetLine != null ? (z.ProjectBudgetLine.BudgetCode + z.ProjectBudgetLine.BudgetName) : "",
                                                    Project = z.ProjectDetail != null ? (z.ProjectDetail.ProjectCode + "-" + z.ProjectDetail.ProjectName) : "",
                                                    Job = (z.ProjectBudgetLine != null && z.ProjectBudgetLine.ProjectJobDetail != null) ? z.ProjectBudgetLine.ProjectJobDetail.ProjectJobCode : ""
                                                }).ToList()) : new List<EmployeeAnalyticalInfo>()
                }).ToList();

                data.ForEach(x => x.NetSalary = x.GrossSalary + x.Bonus - x.Fine - x.CapacityBuilding - x.Security - 
                            x.SalaryTax - x.Advance - x.Pension);

                List<string> EmployeeCodes = data.Where(x => x.EmployeeAnalyticalInfoList.Count == 0).Select(x => x.EmployeeCode).Distinct().ToList();

                if (EmployeeCodes.Any())
                {
                    return string.Format("Salary Percentage not present for Employees {0}", String.Join(",", EmployeeCodes));
                }

                List<ExchangeRateDetail> exchangeRates = await _dbContext.ExchangeRateDetail.Where(x => x.IsDeleted == false &&
                                                                x.Date.Date == DateTime.Now.Date && request.OfficeId.Contains(x.OfficeId)).ToListAsync();

                model.PayrollExcelData = new List<PayrollExcelData>();

                var currencyAFG = await _dbContext.CurrencyDetails.FirstOrDefaultAsync(x => x.IsDeleted == false && x.CurrencyId == (int)Currency.AFG);

                model.HeaderAndFooter.Currency = currencyAFG.CurrencyCode;

                if (data.Count == 0)
                {
                    return "No approved payroll present for employee";
                }

                List<PayrollMonthlyHourDetail> payrollHours = await _dbContext.PayrollMonthlyHourDetail
                                                                              .Where(x => x.IsDeleted == false && request.OfficeId.Contains(x.OfficeId)
                                                                              && request.Month.Contains(x.PayrollMonth) &&
                                                                              x.PayrollYear == DateTime.UtcNow.Year).ToListAsync();

                List<string> weeklyOff = _dbContext.HolidayWeeklyDetails.Where(x => x.IsDeleted == false &&
                                                        x.FinancialYearId == financialYear.FinancialYearId)
                                                        .Select(x => x.Day).ToList();

                foreach (var item in data)
                {
                    double rate = 1;
                    if (item.CurrencyId != (int)Currency.AFG)
                    {
                        var exchangeRate = exchangeRates.FirstOrDefault(x => x.FromCurrency == item.CurrencyId && x.ToCurrency == (int)Currency.AFG);

                        if (exchangeRate == null)
                        {
                            var currency = await _dbContext.CurrencyDetails.FirstOrDefaultAsync(x => x.IsDeleted == false && x.CurrencyId == item.CurrencyId);
                            var officeDetail = await _dbContext.OfficeDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.OfficeId == item.OfficeId);
                            return string.Format("Exchange rate not defined for currency {0} and Office {1}", currency.CurrencyCode, officeDetail.OfficeCode);
                        }
                    }

                    int weeklyOffDays = ParticularDayInMonth(new DateTime(financialYear.StartDate.Year, item.MonthNumber, 1), weeklyOff);
                    var payrollHourForEmployee = payrollHours.FirstOrDefault(x => x.IsDeleted == false && x.OfficeId == item.OfficeId
                                                                              && x.PayrollMonth == item.MonthNumber &&
                                                                              x.PayrollYear == financialYear.StartDate.Year);

                    int workingHours = payrollHourForEmployee.OutTime.Value.Subtract(payrollHourForEmployee.InTime.Value).Hours;
                    double payToBeSubtracted = weeklyOffDays * rate * item.HourlyRate * workingHours;

                    foreach (var analytical in item.EmployeeAnalyticalInfoList)
                    {
                        PayrollExcelData excelData = new PayrollExcelData
                        {
                            AbsentHours = item.AbsentDays * workingHours,
                            Advance = (item.Advance * rate * analytical.Percentage) / 100,
                            AttendedHours = item.AttendedHours,
                            BasicPay = (item.BasicPay * rate * analytical.Percentage) / 100,
                            Bonus = item.Bonus * rate,
                            CapacityBuilding = (item.CapacityBuilding * rate * analytical.Percentage) / 100,
                            Currency = currencyAFG.CurrencyCode,
                            Designation = item.Designation,
                            BudgetLine = analytical.BudgetLine,
                            Fine = item.Fine * rate,
                            Gender = item.Gender,
                            GrossSalary = (item.GrossSalary * rate * analytical.Percentage) / 100,
                            Job = analytical.Job,
                            Month = item.Month,
                            Name = item.Name,
                            NetSalary = (item.NetSalary * rate * analytical.Percentage) / 100,
                            EmployeeId = item.EmployeeId,
                            EmployeeCode= item.EmployeeCode,
                            Office = item.Office,
                            Pension = (item.Pension * rate * analytical.Percentage) / 100,
                            Project = analytical.Project,
                            Percentage = analytical.Percentage,
                            Salary = ((item.BasicPay * rate) - ((item.AbsentHours * item.HourlyRate * rate)* analytical.Percentage / 100)),
                            SalaryTax = (item.SalaryTax * rate * analytical.Percentage) / 100,
                            Security = (item.Security * rate * analytical.Percentage) / 100,
                        };

                        model.PayrollExcelData.Add(excelData);
                    }
                }

                List<int> calculateSumOnKeyIndex = new List<int>
               {

                   8,9,10,11,12,13,14,15,16,17,18,19,20
               };

                List<string> headers = new List<string>
                {
                    "S. No", "Month", "Emp Code", "Name", "Designation", "Sex", "Currency",
                    "Office", "Basic Pay", "Atd", "Abs", "Salary", "Bonus", "Gross", "Cap. B", "Security",
                    "S. Tax", "Fine", "Adv.", "Pension", "Net Salary", "Project", "Job", "BLine", "Percentage"
                };


                return _excelExportService.ExportEmployeePayrollExcel(model, headers, calculateSumOnKeyIndex);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static int ParticularDayInMonth(DateTime time, List<string> dayNames)
        {
            int weekDaysInAMonth = 0;

            int daysInAMonth = DateTime.DaysInMonth(time.Year, time.Month);

            for (int i = 1; i <= daysInAMonth; i++)
            {
                DateTime date = new DateTime(time.Year, time.Month, i);
                string dayName = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(date.DayOfWeek);
                if (dayNames.Contains(dayName))
                {
                    weekDaysInAMonth++;
                }
            }
            return weekDaysInAMonth;
        }
    }
}
