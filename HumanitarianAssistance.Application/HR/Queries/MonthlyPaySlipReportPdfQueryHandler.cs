using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries {
    public class MonthlyPaySlipReportPdfQueryHandler : IRequestHandler<MonthlyPaySlipReportPdfQuery, byte[]> {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;
        public MonthlyPaySlipReportPdfQueryHandler (HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService, IHostingEnvironment env) {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
        }

        public async Task<byte[]> Handle (MonthlyPaySlipReportPdfQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();

            try {
                FinancialYearDetail financialYear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync (x => x.IsDeleted == false && x.IsDefault == true);

                if (financialYear == null) {
                    throw new Exception (StaticResource.FinancialYearNotFound);
                }
                List<EmployeeAttendance> empPayrollAttendance = await _dbContext.EmployeeAttendance
                    .Include (x => x.EmployeeDetails)
                    .Include (x => x.EmployeeDetails.EmployeeProfessionalDetail)
                    .Where (x => x.EmployeeId == request.EmployeeId &&
                        x.Date.Month == request.Month && x.Date.Year == financialYear.StartDate.Year &&
                        x.IsDeleted == false && x.EmployeeDetails.IsDeleted == false).ToListAsync ();
                        
                // List<PayrollMonthlyHourDetail> payrollHours = await _dbContext.PayrollMonthlyHourDetail
                //                                                               .Where(x => x.IsDeleted == false && request.OfficeId.Contains(x.OfficeId)
                //                                                               && request.Month == x.PayrollMonth &&
                //                                                               x.PayrollYear == DateTime.UtcNow.Year).ToListAsync();
                
                string logoPath = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/logo.jpg")?.PhysicalPath;
                string persianChaName = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/PersianText.png")?.PhysicalPath;
                MonthlyPaySlipReportPdfModel details = new MonthlyPaySlipReportPdfModel();
                details = await (from e in _dbContext.EmployeeDetail
                .Where (x => x.EmployeeID == request.EmployeeId && x.IsDeleted==false) 
                join p in _dbContext.EmployeeProfessionalDetail on e.EmployeeID 
                equals p.EmployeeId into pd from p in pd.DefaultIfEmpty () 
                join t in _dbContext.EmployeeType on p.EmployeeTypeId 
                equals t.EmployeeTypeId into td from t in td.DefaultIfEmpty () 
                join dd in _dbContext.DesignationDetail on p.DesignationId 
                equals dd.DesignationId into ddp from dd in ddp.DefaultIfEmpty () 
                join o in _dbContext.OfficeDetail 
                on p.OfficeId equals o.OfficeId into od from o in od.DefaultIfEmpty ()
                join b in _dbContext.EmployeeBasicSalaryDetail 
                on p.EmployeeId equals b.EmployeeId into bs from b in bs.DefaultIfEmpty ()
                join c in _dbContext.CurrencyDetails 
                on b.CurrencyId equals c.CurrencyId into cd from c in cd.DefaultIfEmpty ()
                join ps in _dbContext.EmployeePayrollInfoDetail 
                on b.EmployeeId equals ps.EmployeeId into psl from ps in psl.DefaultIfEmpty ()
                join ph in _dbContext.PayrollMonthlyHourDetail
                on p.OfficeId equals ph.OfficeId into phd from ph in phd.DefaultIfEmpty ()
                where ph.AttendanceGroupId == p.AttendanceGroupId && ph.PayrollMonth == request.Month &&
                ph.PayrollYear == financialYear.StartDate.Year && ph.IsDeleted == false &&
                ps.IsDeleted == false  && b.IsDeleted == false  && o.IsDeleted == false &&
                ps.Month == request.Month && ps.Year == financialYear.StartDate.Year &&
                dd.IsDeleted == false  && t.IsDeleted == false  && p.IsDeleted == false
                select new MonthlyPaySlipReportPdfModel {
                    PaymentDate = DateTime.Now.ToString("MM/dd/yyyy"),
                        SalaryMonth = request.Month,
                        SalaryYear = financialYear.StartDate.Year,
                        EmployeeCode = e.EmployeeCode,
                        EmployeeName = e.EmployeeName,
                        Designation = dd.Designation,
                        Type = t.EmployeeTypeName,
                        Office = o.OfficeName,
                        HourlyRate= ps.HourlyRate,
                        WorkingHours = ph.OutTime.Value.Subtract(ph.InTime.Value).Hours,
                        Sex = e.SexId == (int)Gender.MALE ? "Male" : e.SexId == (int)Gender.FEMALE ? "Female" : e.SexId == (int)Gender.OTHER ? "Other" : null,
                        AnalyticalInfo = (from mp in _dbContext.EmployeeSalaryAnalyticalInfo
                                         .Where(x=>x.EmployeeID==request.EmployeeId && x.IsDeleted==false) 
                                         join acc in _dbContext.ChartOfAccountNew 
                                         on mp.AccountNo equals acc.ChartOfAccountNewId into accl from acc in accl.DefaultIfEmpty ()
                                         join prj in _dbContext.ProjectDetail 
                                         on mp.ProjectId equals prj.ProjectId into prjl from prj in prjl.DefaultIfEmpty ()
                                         select new AnalyticalInfo {
                                            BudgetLine = mp.ProjectBudgetLine.BudgetName,
                                            Program = (from ppd in _dbContext.ProjectProgram
                                                      .Where(x=>x.ProjectId==prj.ProjectId && x.IsDeleted==false)
                                                       join pd in _dbContext.ProgramDetail 
                                                       on ppd.ProgramId equals pd.ProgramId into pdl from pd in pdl.DefaultIfEmpty ()
                                                      select new ProjectProgramDetail {
                                                        Program = pd.ProgramName
                                                      }).ToList(),
                                            Project = prj.ProjectCode + '-' + prj.ProjectName,
                                            Job = (from pbl in _dbContext.ProjectBudgetLineDetail
                                                      .Where(x=>x.BudgetLineId==mp.ProjectBudgetLine.BudgetLineId && x.IsDeleted==false)
                                                       join pjd in _dbContext.ProjectJobDetail 
                                                       on pbl.ProjectJobId equals pjd.ProjectJobId into pjdl from pjd in pjdl.DefaultIfEmpty ()
                                                      select new JobDetail{Job = pjd.ProjectJobName}).FirstOrDefault(),
                                            Sector = (from psd in _dbContext.ProjectSector
                                                      .Where(x=>x.ProjectId==prj.ProjectId && x.IsDeleted==false)
                                                       join sd in _dbContext.SectorDetails 
                                                       on psd.SectorId equals sd.SectorId into sdl from sd in sdl.DefaultIfEmpty ()
                                                      select new ProjectSectorDetail {
                                                        Sector = sd.SectorName
                                                      }).ToList(),
                                            Area = "-",
                                            Account = acc.ChartOfAccountNewCode + '-' + acc.AccountName,
                                            SalaryPercentage = mp.SalaryPercentage.ToString(),
                                            AnalyticalSalary = (b.BasicSalary * mp.SalaryPercentage) / 100
                                         }).ToList(),
                        BasicSalary = b.BasicSalary,
                        GrossSalary = ps.GrossSalary,
                        CurrencyCode = c.CurrencyCode,
                        Attendance = empPayrollAttendance.Where(x=>x.AttendanceTypeId == (int)AttendanceType.P).Count(),
                        Absentess = empPayrollAttendance.Where(x=>x.AttendanceTypeId == (int)AttendanceType.A).Count(),

                        AllowanceAdvance = _dbContext.AccumulatedSalaryHeadDetail
                        .Where(x=>x.Year == financialYear.StartDate.Year && x.Month == request.Month && x.EmployeeId == e.EmployeeID && x.IsDeleted == false && x.SalaryComponentId == (int)AccumulatedSalaryHead.AdvanceRecovery).Select(x=> new {x.SalaryAllowance}).FirstOrDefault().SalaryAllowance,
                        AllowanceSalaryTax = _dbContext.AccumulatedSalaryHeadDetail
                        .Where(x=>x.Year == financialYear.StartDate.Year && x.Month == request.Month && x.EmployeeId == e.EmployeeID && x.IsDeleted == false && x.SalaryComponentId == (int)AccumulatedSalaryHead.SalaryTax).Select(x=> new {x.SalaryAllowance}).FirstOrDefault().SalaryAllowance,
                        AllowancePension = _dbContext.AccumulatedSalaryHeadDetail
                        .Where(x=>x.Year == financialYear.StartDate.Year && x.Month == request.Month && x.EmployeeId == e.EmployeeID && x.IsDeleted == false && x.SalaryComponentId == (int)AccumulatedSalaryHead.Pension).Select(x=> new {x.SalaryAllowance}).FirstOrDefault().SalaryAllowance,                       
                        AllowanceCb = _dbContext.AccumulatedSalaryHeadDetail
                        .Where(x=>x.Year == financialYear.StartDate.Year && x.Month == request.Month && x.EmployeeId == e.EmployeeID && x.IsDeleted == false && x.SalaryComponentId == (int)AccumulatedSalaryHead.CapacityBuilding).Select(x=> new {x.SalaryAllowance}).FirstOrDefault().SalaryAllowance,
                        AllowanceSecurity = _dbContext.AccumulatedSalaryHeadDetail
                        .Where(x=>x.Year == financialYear.StartDate.Year && x.Month == request.Month && x.EmployeeId == e.EmployeeID && x.IsDeleted == false && x.SalaryComponentId == (int)AccumulatedSalaryHead.Security).Select(x=> new {x.SalaryAllowance}).FirstOrDefault().SalaryAllowance,
                        Advance = _dbContext.AccumulatedSalaryHeadDetail
                        .Where(x=>x.Year == financialYear.StartDate.Year && x.Month == request.Month && x.EmployeeId == e.EmployeeID && x.IsDeleted == false && x.SalaryComponentId == (int)AccumulatedSalaryHead.AdvanceRecovery).Select(x=> new {x.SalaryDeduction}).FirstOrDefault().SalaryDeduction,
                        SalaryTax = _dbContext.AccumulatedSalaryHeadDetail
                        .Where(x=>x.Year == financialYear.StartDate.Year && x.Month == request.Month && x.EmployeeId == e.EmployeeID && x.IsDeleted == false && x.SalaryComponentId == (int)AccumulatedSalaryHead.SalaryTax).Select(x=> new {x.SalaryDeduction}).FirstOrDefault().SalaryDeduction,
                        Fine = _dbContext.EmployeeBonusFineSalaryHead.Any(y => y.IsDeleted == false && y.EmployeeId == e.EmployeeID &&
                           y.Month == request.Month && y.Year == financialYear.StartDate.Year && y.TransactionTypeId == (int)TransactionType.Credit) ?
                            _dbContext.EmployeeBonusFineSalaryHead.Where(y => y.IsDeleted == false && y.EmployeeId == e.EmployeeID &&
                            y.Month == request.Month && y.Year == financialYear.StartDate.Year && y.TransactionTypeId == (int)TransactionType.Credit).Select(z => z.Amount).DefaultIfEmpty(0).Sum() : 0,
                        Bonus = _dbContext.EmployeeBonusFineSalaryHead.Any(y => y.IsDeleted == false && y.EmployeeId == e.EmployeeID &&
                           y.Month == request.Month && y.Year == financialYear.StartDate.Year && y.TransactionTypeId == (int)TransactionType.Debit) ?
                            _dbContext.EmployeeBonusFineSalaryHead.Where(y => y.IsDeleted == false && y.EmployeeId == e.EmployeeID &&
                            y.Month == request.Month && y.Year == financialYear.StartDate.Year && y.TransactionTypeId == (int)TransactionType.Debit).Select(z => z.Amount).DefaultIfEmpty(0).Sum() : 0,
                        Pension = _dbContext.AccumulatedSalaryHeadDetail
                        .Where(x=>x.Year == financialYear.StartDate.Year && x.Month == request.Month && x.EmployeeId == e.EmployeeID && x.IsDeleted == false && x.SalaryComponentId == (int)AccumulatedSalaryHead.Pension).Select(x=> new {x.SalaryDeduction}).FirstOrDefault().SalaryDeduction,                      
                        Cb = _dbContext.AccumulatedSalaryHeadDetail
                        .Where(x=>x.Year == financialYear.StartDate.Year && x.Month == request.Month && x.EmployeeId == e.EmployeeID && x.IsDeleted == false && x.SalaryComponentId == (int)AccumulatedSalaryHead.CapacityBuilding).Select(x=> new {x.SalaryDeduction}).FirstOrDefault().SalaryDeduction,
                        Security = _dbContext.AccumulatedSalaryHeadDetail
                        .Where(x=>x.Year == financialYear.StartDate.Year && x.Month == request.Month && x.EmployeeId == e.EmployeeID && x.IsDeleted == false && x.SalaryComponentId == (int)AccumulatedSalaryHead.Security).Select(x=> new {x.SalaryDeduction}).FirstOrDefault().SalaryDeduction,
                        Net = ps.NetSalary,
                        SalaryCurrencyCode = c.CurrencyCode,
                        ApprovedEmployeeCode = e.EmployeeCode,
                        ApprovedEmployeeName = e.EmployeeName,
                        LogoPath = logoPath,
                        PersianChaName = persianChaName
                }).FirstOrDefaultAsync ();

                if(details != null)
                {
                    details.Salary = (double)Math.Round((double)(details.BasicSalary - (details.Absentess* details.WorkingHours * details.HourlyRate)), 2);
                }
                return await _pdfExportService.ExportToPdf (details, "Pages/PdfTemplates/MonthlyPaySlipReport.cshtml", true);
            } catch (Exception ex) {
                throw new Exception (ex.Message);
            }
        }
    }
}