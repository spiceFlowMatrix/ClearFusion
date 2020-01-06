using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class EmployeeAnnualTunoverReportPdfQueryHandler : IRequestHandler<EmployeeAnnualTunoverReportPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;

        public EmployeeAnnualTunoverReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService, IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
        }

        public async Task<byte[]> Handle(EmployeeAnnualTunoverReportPdfQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var exitInterviewDetails = (from eid in _dbContext.ExistInterviewDetails
                   .Where(x => x.IsDeleted == false && x.OfficeId == request.OfficeId)
                                            join ed in _dbContext.EmployeeDetail
                                            on eid.EmployeeID equals ed.EmployeeID into edl
                                            from ed in edl.DefaultIfEmpty()
                                            join epd in _dbContext.EmployeeProfessionalDetail
                                            on ed.EmployeeID equals epd.EmployeeId into epdl
                                            from epd in epdl.DefaultIfEmpty()
                                            join d in _dbContext.DesignationDetail
                                            on epd.DesignationId equals d.DesignationId into dl
                                            from d in dl.DefaultIfEmpty()
                                            join o in _dbContext.OfficeDetail
                                            on eid.OfficeId equals o.OfficeId into od
                                            from o in od.DefaultIfEmpty()
                                            join et in _dbContext.EmployeeType
                                            on ed.EmployeeTypeId equals et.EmployeeTypeId into etl
                                            from et in etl.DefaultIfEmpty()
                                            select new EmployeeAnnualTunoverPdfModel
                                            {
                                                EmployeeCode = eid.EmployeeCode,
                                                EmployeeName = ed.EmployeeName,
                                                Designation = d.Designation,
                                                Office = o.OfficeName,
                                                EmployeeStatus = et.EmployeeTypeName,
                                                Tenure = (DateTime.Now.Date - epd.HiredOn.Value.Date).Days.ToString() + " Days",
                                                Remarks = ed.Remarks,
                                                ReasonForLeavingDetails = new ReasonForLeavingModel
                                                {
                                                    Benefits = eid.Benefits,
                                                    BetterJobOpportunity = eid.BetterJobOpportunity,
                                                    FamilyReasons = eid.FamilyReasons,
                                                    NotChallenged = eid.NotChallenged,
                                                    Pay = eid.Pay,
                                                    PersonalReasons = eid.PersonalReasons,
                                                    Relocation = eid.Relocation,
                                                    ReturnToSchool = eid.ReturnToSchool,
                                                    ConflictWithSuoervisors = eid.ConflictWithSuoervisors,
                                                    ConflictWithOther = eid.ConflictWithOther,
                                                    WorkRelationship = eid.WorkRelationship,
                                                    CompanyInstability = eid.CompanyInstability,
                                                    CareerChange = eid.CareerChange,
                                                    HealthIssue = eid.HealthIssue
                                                }
                                            }).ToList();

                List<EmployeeAnnualTunoverPdfModel> summary = new List<EmployeeAnnualTunoverPdfModel>();
                if (exitInterviewDetails.Count > 0)
                {
                    var serial = 1;
                    foreach (var item in exitInterviewDetails)
                    {
                        summary.Add(new EmployeeAnnualTunoverPdfModel
                        {
                            LogoPath = serial == 1 ? _env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath : null,
                            PersianChaName = serial == 1 ? _env.WebRootFileProvider.GetFileInfo("ReportLogo/PersianText.png")?.PhysicalPath : null,
                            SerialNumber = serial,
                            EmployeeCode = item.EmployeeCode,
                            EmployeeName = item.EmployeeName,
                            Designation = item.Designation,
                            Office = item.Office,
                            EmployeeStatus = item.EmployeeStatus,
                            Tenure = item.Tenure,
                            Remarks = item.Remarks,
                            ReasonForLeavingDetails = item.ReasonForLeavingDetails
                        });
                        serial = serial + 1;
                    }
                }
                // model logic here 
                return await _pdfExportService.ExportToPdf(summary, "Pages/PdfTemplates/EmployeeAnnualTunoverReport.cshtml", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}