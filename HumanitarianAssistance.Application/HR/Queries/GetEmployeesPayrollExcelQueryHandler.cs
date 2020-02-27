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
            byte[] result;
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
                                            // .Where(x=> x.IsDeleted == false && x.EmployeeDetail.IsDeleted == false &&
                                            // x.Month >= request.StartDate.Month && x.Year >= request.StartDate.Year && x.Month <= request.EndDate.Month &&
                                            // x.Year <= request.EndDate.Year && request.OfficeId.Contains(x.EmployeeDetail.EmployeeProfessionalDetail.OfficeId)).AsQueryAble();
                                            .Where(x => x.IsDeleted == false && x.EmployeeDetail.IsDeleted == false &&
                                            x.Month == request.Month && x.Year == financialYear.StartDate.Year &&
                                            request.OfficeId.Contains(x.EmployeeDetail.EmployeeProfessionalDetail.OfficeId.Value)).AsQueryable();

                if (request.SelectedEmployees.Any())
                {
                    query = query.Where(x => request.SelectedEmployees.Contains(x.EmployeeId));
                }

                if (!string.IsNullOrEmpty(request.EmployeeName))
                {
                    query = query.Where(x => x.EmployeeDetail.EmployeeName.Contains(request.EmployeeName));
                }

                if (!string.IsNullOrEmpty(request.EmployeeCode))
                {
                    query = query.Where(x => x.EmployeeDetail.EmployeeCode.Contains(request.EmployeeCode));
                }

                if (request.Sex.HasValue)
                {
                    query = query.Where(x => x.EmployeeDetail.SexId == request.Sex.Value);
                }

                if (request.EmploymentStatus.HasValue)
                {
                    query = query.Where(x => x.EmployeeDetail.EmployeeProfessionalDetail.EmployeeTypeId == request.EmploymentStatus.Value);
                }

                // if(request.ProjectIds.Any())
                // {
                //     query= query.Where(x=> x.EmployeeDetail.EmployeeAnalyticalList.Select(y=> y.ProjectId).Contains(request.ProjectIds));
                // }

                var office = await _dbContext.OfficeDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && request.OfficeId.Contains(x.OfficeId));

                model.HeaderAndFooter = new HeaderAndFooter
                {
                    Date = DateTime.Now.ToShortDateString(),
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
                    // AbsentHours = x.EmployeeDetail.EmployeeAttendance.Where(y=> y.IsDeleted == false &&
                    //                 y.Date.Month == x.Month && y.Date.Year == x.Year && y.AttendanceTypeId == AttendanceType.A)
                    //                 .Select(z=> z.OutTime.Value -z.InTime.Value).DefaultIfEmpty(0).Sum()
                    AbsentHours = 0,
                    NetSalary = 0,
                    HourlyRate= x.HourlyRate,
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
                    EmployeeAnalyticalInfoList = (request.ProjectIds != null && x.EmployeeDetail.EmployeeAnalyticalList.Count > 0) ?
                                                (x.EmployeeDetail.EmployeeAnalyticalList.Where(r => x.IsDeleted == false &&
                                                request.ProjectIds.Contains(r.ProjectId)).Select(z => new EmployeeAnalyticalInfo
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

                data.ForEach(x => x.NetSalary = x.GrossSalary + x.Bonus - x.Fine - x.CapacityBuilding - x.Security - x.SalaryTax -
                             x.SalaryTax - x.Advance - x.Pension);

                List<string> EmployeeCodes = data.Where(x => x.EmployeeAnalyticalInfoList.Count == 0).Select(x => x.EmployeeCode).ToList();

                if (EmployeeCodes.Any())
                {
                    return string.Format("Salary Percentage not present for Employees {0}", String.Join(",", EmployeeCodes));
                }

                List<ExchangeRateDetail> exchangeRates = await _dbContext.ExchangeRateDetail.Where(x => x.IsDeleted == false &&
                                                                x.Date.Date == DateTime.Now.Date && request.OfficeId.Contains(x.OfficeId)).ToListAsync();

                model.PayrollExcelData = new List<PayrollExcelData>();

                var currencyAFG = await _dbContext.CurrencyDetails.FirstOrDefaultAsync(x => x.IsDeleted == false && x.CurrencyId == (int)Currency.AFG);

                model.HeaderAndFooter.Currency = currencyAFG.CurrencyCode;

                if(data.Count ==0)
                {
                     return "No approved payroll present for selected employees";
                }

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

                    foreach (var analytical in item.EmployeeAnalyticalInfoList)
                    {
                        PayrollExcelData excelData = new PayrollExcelData
                        {
                            AbsentHours = item.AbsentHours,
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
                            GrossSalary = item.GrossSalary * rate,
                            Job = analytical.Job,
                            Month = item.Month,
                            Name = item.Name,
                            NetSalary = (item.NetSalary * rate * analytical.Percentage) / 100,
                            EmployeeId = item.EmployeeId,
                            Office = item.Office,
                            Pension = (item.Pension * rate * analytical.Percentage) / 100,
                            Project = analytical.Project,
                            Percentage = analytical.Percentage,
                            Salary = (item.BasicPay * rate) - ((item.AttendedHours * item.HourlyRate * rate) * analytical.Percentage / 100) ,
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
    }
}
