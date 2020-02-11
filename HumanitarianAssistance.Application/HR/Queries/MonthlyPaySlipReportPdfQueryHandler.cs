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
                select new MonthlyPaySlipReportPdfModel {
                    PaymentDate = new DateTime (),
                        SalaryMonth = "-",
                        SalaryYear = "-",
                        EmployeeCode = e.EmployeeCode,
                        EmployeeName = e.EmployeeName,
                        Designation = dd.Designation,
                        Type = t.EmployeeTypeName,
                        Office = o.OfficeName,
                        Sex = e.Sex,
                        BudgetLine = "-",
                        Program = "-",
                        Project = "-",
                        Job = "-",
                        Sector = "-",
                        Area = "-",
                        Account = "-",
                        SalaryPercentage = "-",
                        AnalyticalSalary = "-",
                        BasicSalary = b.BasicSalary,
                        CurrencyCode = c.CurrencyCode,
                        Attendance = "-",
                        Absentess = "-",
                        Salary = "-",
                        Food = "-",
                        Tr = "-",
                        Medical = "-",
                        AllowanceOther = "-",
                        AllowanceOther1 = "-",
                        AllowanceOther2 = "-",
                        GrossSalary = ps.GrossSalary,
                        Advance = _dbContext.AccumulatedSalaryHeadDetail
                        .Where(x=>x.EmployeeId == e.EmployeeID && x.IsDeleted == false && x.Id == (int)AccumulatedSalaryHead.AdvanceRecovery).Select(x=> new {x.SalaryDeduction}).FirstOrDefault().SalaryDeduction,
                        SalaryTax = _dbContext.AccumulatedSalaryHeadDetail
                        .Where(x=>x.EmployeeId == e.EmployeeID && x.IsDeleted == false && x.Id == (int)AccumulatedSalaryHead.SalaryTax).Select(x=> new {x.SalaryDeduction}).FirstOrDefault().SalaryDeduction,
                        Fine = "-",
                        DeductionOther = "-",
                        Pension = "-",
                        Cb = _dbContext.AccumulatedSalaryHeadDetail
                        .Where(x=>x.EmployeeId == e.EmployeeID && x.IsDeleted == false && x.Id == (int)AccumulatedSalaryHead.CapacityBuilding).Select(x=> new {x.SalaryDeduction}).FirstOrDefault().SalaryDeduction,
                        Security = _dbContext.AccumulatedSalaryHeadDetail
                        .Where(x=>x.EmployeeId == e.EmployeeID && x.IsDeleted == false && x.Id == (int)AccumulatedSalaryHead.Security).Select(x=> new {x.SalaryDeduction}).FirstOrDefault().SalaryDeduction,
                        Net = ps.NetSalary,
                        AFN = "-",
                        Other1Desc = "-",
                        Other2Desc = "-",
                        ApprovedEmployeeCode = "-",
                        ApprovedEmployeeName = "-",
                        LogoPath = logoPath,
                        PersianChaName = persianChaName
                }).FirstOrDefaultAsync ();
                return await _pdfExportService.ExportToPdf (details, "Pages/PdfTemplates/MonthlyPaySlipReport.cshtml", true);
            } catch (Exception ex) {
                throw new Exception (ex.Message);
            }
        }
    }
}