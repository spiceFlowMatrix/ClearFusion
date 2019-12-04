using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries {

    public class GetHiringRequestFormPdfQueryHandler : IRequestHandler<GetHiringRequestFormPdfQuery, byte[]> {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;

        public GetHiringRequestFormPdfQueryHandler (HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService) {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
        }

        public async Task<byte[]> Handle (GetHiringRequestFormPdfQuery request, CancellationToken cancellationToken) {
            try {
                // model logic here JobCategory Organization Province SalaryRange Nationality JobStatus Background
                var requestDetail = await (from hr in _dbContext.ProjectHiringRequestDetail
                    .Where (x => x.IsDeleted == false && x.ProjectId == request.ProjectId  && x.HiringRequestId == request.HiringRequestId) 
                    join c in _dbContext.CurrencyDetails 
                    on hr.CurrencyId equals c.CurrencyId into h from c in h.DefaultIfEmpty () 
                    join o in _dbContext.OfficeDetail 
                    on hr.OfficeId equals o.OfficeId into od from o in od.DefaultIfEmpty () 
                    join g in _dbContext.JobGrade 
                    on hr.GradeId equals g.GradeId into gd from g in gd.DefaultIfEmpty () 
                    join p in _dbContext.ProfessionDetails 
                    on hr.ProfessionId equals p.ProfessionId into pd from p in pd.DefaultIfEmpty () 
                    join b in _dbContext.ProjectBudgetLineDetail
                    on hr.BudgetLineId equals b.BudgetLineId into bl from b in bl.DefaultIfEmpty () 
                    join d in _dbContext.Department 
                    on hr.JobTypeId equals d.DepartmentId into dp from d in dp.DefaultIfEmpty () 
                    join dd in _dbContext.DesignationDetail 
                    on hr.PositionId equals dd.DesignationId into ddp from dd in ddp.DefaultIfEmpty () 
                    join ed in _dbContext.EducationDegreeMaster 
                    on hr.EducationDegreeId equals ed.Id into edm from ed in edm.DefaultIfEmpty () 
                    // join pdl in _dbContext.ProvinceDetails 
                    // on hr.ProvinceId equals pdl.ProvinceId into pdd from pdl in pdd.DefaultIfEmpty () 
                    // join cdl in _dbContext.CountryDetails 
                    // on hr.CountryId equals cdl.CountryId into cdd from cdl in cdd.DefaultIfEmpty () 
                    join dpl in _dbContext.Department 
                    on hr.JobTypeId equals dpl.DepartmentId into dpld from dpl in dpld.DefaultIfEmpty () 
                    select new HiringRequestFormPdfModel {
                        //JobCategory = "-",
                            MinimumEducation = ed.Name,
                            TotalVacancies = hr.TotalVacancies,
                            Position = dd.Designation,
                           // Organization = hr.Organization,
                            Office = o.OfficeName,
                           // Province = pdl.ProvinceName,
                            ContractType = hr.ContractType,
                            ContractDuration = hr.ContractDuration,
                            Gender = hr.GenderId == 1 ? "Male" : hr.GenderId == 2 ? "Female" : "Other",
                            HourlyPayRate = hr.HourlyRate,
                            PayCurrency = c.CurrencyName,
                            HiringRequestCode =  hr.HiringRequestCode, 
                            AnnouncingDate = hr.AnouncingDate != null ? hr.AnouncingDate.Value.ToShortDateString () : "",
                            ClosingDate = hr.ClosingDate != null ? hr.ClosingDate.Value.ToShortDateString () : "",
                           // Country = cdl.CountryName,
                            FilledVacancies = hr.FilledVacancies,
                            JobType = dpl.DepartmentName,
                            Shift = hr.Shift == 1 ? "Day" : "Night",
                            JobStatus = ((HiringRequestStatus)(hr.HiringRequestStatus)).ToString(),
                            Experience = hr.Experience,
                            // Background = hr.Background,
                            SpecificDutiesAndResponsiblities = hr.SpecificDutiesAndResponsblities,
                            KnowladgeAndSkillRequired = hr.KnowladgeAndSkillRequired,
                            SubmissionGuidline = hr.SubmissionGuidlines
                    }).FirstOrDefaultAsync ();

                List<HiringRequestFormPdfModel> summary = new List<HiringRequestFormPdfModel> ();
                summary.Add (new HiringRequestFormPdfModel {
                        // JobCategory = requestDetail.JobCategory,
                        MinimumEducation = requestDetail.MinimumEducation,
                        TotalVacancies = requestDetail.TotalVacancies,
                        Position = requestDetail.Position,
                        // Organization = requestDetail.Organization,
                        Office = requestDetail.Office,
                        // Province = requestDetail.Province,
                        ContractType = requestDetail.ContractType,
                        ContractDuration = requestDetail.ContractDuration,
                        Gender = requestDetail.Gender,
                        // SalaryRange = requestDetail.SalaryRange,
                        AnnouncingDate = requestDetail.AnnouncingDate,
                        ClosingDate = requestDetail.ClosingDate,
                        // Country = requestDetail.Country,
                        FilledVacancies = requestDetail.FilledVacancies,
                        JobType = requestDetail.JobType,
                        Shift = requestDetail.Shift,
                        JobStatus = requestDetail.JobStatus,
                        Experience = requestDetail.Experience,
                        // Background = requestDetail.Background,
                        SpecificDutiesAndResponsiblities = requestDetail.SpecificDutiesAndResponsiblities,
                        KnowladgeAndSkillRequired = requestDetail.KnowladgeAndSkillRequired,
                        SubmissionGuidline = requestDetail.SubmissionGuidline,
                        HourlyPayRate = requestDetail.HourlyPayRate,
                        PayCurrency = requestDetail.PayCurrency,
                        HiringRequestCode =  requestDetail.HiringRequestCode
                });

                return await _pdfExportService.ExportToPdf (summary, "Pages/PdfTemplates/HiringRequestForm.cshtml");
            } catch (Exception ex) {
                throw new Exception (ex.Message);
            }

        }
    }
}