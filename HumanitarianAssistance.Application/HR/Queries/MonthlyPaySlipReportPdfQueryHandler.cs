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

                MonthlyPaySlipReportPdfModel summary = new MonthlyPaySlipReportPdfModel () {
                    PaymentDate = new DateTime(),
                    SalaryMonth = "test",
                    SalaryYear = "test",
                    EmployeeCode = "test",
                    EmployeeName = "test",
                    Designation = "test",
                    Type = "test",
                    Office = "test",
                    Sex = "test",
                    BudgetLine = "test",
                    Program = "test",
                    Project = "test",
                    Job = "test",
                    Sector = "test",
                    Area = "test",
                    Account = "test",
                    SalaryPercentage = "test",
                    AnalyticalSalary = "test",
                    BasicSalary = "test",
                    CurrencyCode = "test",
                    Attendance = "test",
                    Absentess = "test",
                    Salary = "test",
                    Food = "test",
                    Tr = "test",
                    Medical = "test",
                    AllowanceOther = "test",
                    AllowanceOther1 = "test",
                    AllowanceOther2 = "test",
                    GrossSalary = "test",
                    Advance = "test",
                    SalaryTax = "test",
                    Fine = "test",
                    DeductionOther = "test",
                    Pension = "test",
                    Cb = "test",
                    Security = "test",
                    Net = "test",
                    AFN = "test",
                    Other1Desc = "test",
                    Other2Desc = "test",
                    ApprovedEmployeeCode = "test",
                    ApprovedEmployeeName = "test",
                    LogoPath = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/logo.jpg")?.PhysicalPath,
                    PersianChaName = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/PersianText.png")?.PhysicalPath,
                };
                return await _pdfExportService.ExportToPdf (summary, "Pages/PdfTemplates/MonthlyPaySlipReport.cshtml", true);
            } catch (Exception ex) {
                throw new Exception (ex.Message);
            }
        }
    }
}