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


namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeesPayrollExcelQueryHandler : IRequestHandler<GetEmployeesPayrollExcelQuery, byte[]>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IExcelExportService _excelExportService;

        public GetEmployeesPayrollExcelQueryHandler(IAccountingServices iAccountingServices,
                HumanitarianAssistanceDbContext dbContext, IExcelExportService excelExportService)
        {
            _dbContext = dbContext;
            _excelExportService = excelExportService;
        }
        public async Task<byte[]> Handle(GetEmployeesPayrollExcelQuery request, CancellationToken cancellationToken)
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

                var office = await _dbContext.OfficeDetail.FirstOrDefaultAsync(x=> x.IsDeleted == false && request.OfficeId.Contains(x.OfficeId));

                model.HeaderAndFooter = new HeaderAndFooter
                {
                    Date= DateTime.Now.ToShortDateString(),
                    Header= "Coordination of Humanitarian Assistance",
                    SubHeader= "Payroll Report",
                    Office= office.OfficeCode
                };

                model.PayrollExcelData = query.Select(x => new PayrollExcelData
                {
                    EmployeeId = x.EmployeeId,
                    Name = x.EmployeeDetail.EmployeeName,
                    Designation = x.EmployeeDetail.EmployeeProfessionalDetail.DesignationDetails != null ?
                                                x.EmployeeDetail.EmployeeProfessionalDetail.DesignationDetails.Designation : "",
                    Gender = (x.EmployeeDetail.SexId.HasValue ? (x.EmployeeDetail.SexId == (int)Gender.MALE ? "Male" :
                                                              x.EmployeeDetail.SexId == (int)Gender.FEMALE ? "Female" : "Other") : ""),
                    Currency = x.EmployeeDetail.EmployeeBasicSalaryDetail.CurrencyId.HasValue ?
                              x.EmployeeDetail.EmployeeBasicSalaryDetail.CurrencyDetails.CurrencyName : "",
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
                                                (x.EmployeeDetail.EmployeeAnalyticalList.Where(t => x.IsDeleted == false &&
                                                request.ProjectIds.Contains(t.ProjectId)).Select(z => new EmployeeAnalyticalInfo
                                                {
                                                    Percentage = z.SalaryPercentage,
                                                    BudgetLine = z.ProjectBudgetLine != null ? (z.ProjectBudgetLine.BudgetCode + z.ProjectBudgetLine.BudgetName) : "",
                                                    Project = z.ProjectDetail != null ? (z.ProjectDetail.ProjectCode + z.ProjectDetail.ProjectName) : "",
                                                    Job = (z.ProjectBudgetLine != null && z.ProjectBudgetLine.ProjectJobDetail != null) ? z.ProjectBudgetLine.ProjectJobDetail.ProjectJobCode : ""
                                                }).ToList()) : new List<EmployeeAnalyticalInfo>()
                }).ToList();

                model.PayrollExcelData.ForEach(x => x.NetSalary = x.GrossSalary + x.Bonus - x.Fine - x.CapacityBuilding - x.Security - x.SalaryTax -
                             x.SalaryTax - x.Advance - x.Pension);

                List<string> headers = new List<string>
                {
                    "S. No", "Month", "Emp Code", "Name", "Designation", "Sex", "Currency",
                    "Office", "Basic Pay", "Atd", "Abs", "Salary", "Bonus", "Gross", "Cap. B", "Security",
                    "S. Tax", "Fine", "Adv.", "Pension", "Net Salary", "Project", "Job", "BLine", "Percentage"
                };


                return _excelExportService.ExportEmployeePayrollExcel(model, headers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
