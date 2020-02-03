using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries {

    public class GetEmployeeContractReportPdfQueryHandler : IRequestHandler<GetEmployeeContractReportPdfQuery, byte[]> {
        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private IHostingEnvironment _env;
        public GetEmployeeContractReportPdfQueryHandler (IPdfExportService pdfExportService, HumanitarianAssistanceDbContext dbContext, IHostingEnvironment env) {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
        }
        public async Task<byte[]> Handle (GetEmployeeContractReportPdfQuery request, CancellationToken cancellationToken) {
            try {
                // ContractId
                var contractDetails = await (from ec in _dbContext.EmployeeContract
                    .Where (x => x.IsDeleted == false && x.EmployeeContractId == request.ContractId) 
                    join p in _dbContext.ProjectDetail on (double) ec.Project 
                    equals p.ProjectId into pdl from p in pdl.DefaultIfEmpty () 
                    join ed in _dbContext.EmployeeDetail on ec.EmployeeId 
                    equals ed.EmployeeID into edl from ed in edl.DefaultIfEmpty () 
                    join dd in _dbContext.DesignationDetail on ec.Designation 
                    equals dd.DesignationId into ddp from dd in ddp.DefaultIfEmpty () 
                    join g in _dbContext.JobGrade on ec.Grade 
                    equals g.GradeId into gd from g in gd.DefaultIfEmpty () 
                    join ds in _dbContext.OfficeDetail on ec.DutyStation 
                    equals ds.OfficeId into od from ds in od.DefaultIfEmpty () 
                    join b in _dbContext.ProjectBudgetLineDetail on ec.BudgetLine 
                    equals b.BudgetLineId into bl from b in bl.DefaultIfEmpty () 
                    join pr in _dbContext.ProvinceDetails on ec.Province 
                    equals pr.ProvinceId into pdd from pr in pdd.DefaultIfEmpty () 
                    select new ContractDetailReportPdfModel {
                        EmployeeName = ed.EmployeeName,
                            FatherName = ed.FatherName,
                            EmployeeCode = ed.EmployeeCode,
                            Designation = dd.Designation,
                            ContractStartDate = ec.ContractStartDate.ToString (),
                            ContractEndDate = ec.ContractEndDate.ToString (),
                            DurationOfContract = ec.DurationOfContract.ToString (),
                            Salary = ec.Salary.ToString (),
                            Grade = g.GradeName,
                            ProjectName = p.ProjectName,
                            ProjectCode = p.ProjectCode,
                            DutyStation = ds.OfficeName,
                            Province = pr.ProvinceName,
                            BudgetLine = b.BudgetName,
                            Job = ec.Job,
                            WorkTime = ec.WorkTime.ToString (),
                            WorkDay = ec.WorkDayHours.ToString ()
                    }).FirstOrDefaultAsync ();

                ContractDetailReportPdfModel summary = new ContractDetailReportPdfModel () {
                    EmployeeName = contractDetails.EmployeeName,
                    FatherName = contractDetails.FatherName,
                    EmployeeCode = contractDetails.EmployeeCode,
                    Designation = contractDetails.Designation,
                    ContractStartDate = contractDetails.ContractStartDate,
                    ContractEndDate = contractDetails.ContractEndDate,
                    DurationOfContract = contractDetails.DurationOfContract,
                    Salary = contractDetails.Salary,
                    Grade = contractDetails.Grade,
                    ProjectName = contractDetails.ProjectName,
                    ProjectCode = contractDetails.ProjectCode,
                    DutyStation = contractDetails.DutyStation,
                    Province = contractDetails.Province,
                    BudgetLine = contractDetails.BudgetLine,
                    Job = contractDetails.Job,
                    WorkTime = contractDetails.WorkTime,
                    WorkDay = contractDetails.WorkDay,
                    LogoPath = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/logo.jpg")?.PhysicalPath,
                    PersianChaName = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/PersianText.png")?.PhysicalPath
                };

                return await _pdfExportService.ExportToPdf (summary, "Pages/PdfTemplates/ContractDetailReport.cshtml");
            } catch (Exception ex) {
                throw new Exception (ex.Message);
            }
        }
    }
}