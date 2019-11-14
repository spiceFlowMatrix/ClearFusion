using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace HumanitarianAssistance.Application.Project.Queries {
    public class GetCandidateDetailReportPdfQueryHandler : IRequestHandler<GetCandidateDetailReportPdfQuery, byte[]> {
        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private IHostingEnvironment _env;
        public GetCandidateDetailReportPdfQueryHandler (IPdfExportService pdfExportService, HumanitarianAssistanceDbContext dbContext, IHostingEnvironment env) {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
        }
        public async Task<byte[]> Handle (GetCandidateDetailReportPdfQuery request, CancellationToken cancellationToken) {
            try {
                // model logic here
                var candidateDetail = (from cd in _dbContext.CandidateDetails
                    .Where (x => x.IsDeleted == false) 
                    join s in _dbContext.HiringRequestCandidateStatus on cd.CandidateId equals s.CandidateId 
                    where s.HiringRequestId == request.HiringRequestId && s.ProjectId == request.ProjectId
                    join g in _dbContext.JobGrade on cd.GradeId equals g.GradeId into gd from g in gd.DefaultIfEmpty () join p in _dbContext.ProfessionDetails on cd.ProfessionId equals p.ProfessionId into pd from p in pd.DefaultIfEmpty () join o in _dbContext.OfficeDetail on cd.OfficeId equals o.OfficeId into od from o in od.DefaultIfEmpty () join e in _dbContext.EducationDegreeDetails on cd.EducationDegreeId equals e.EducationDegreeId into ed from e in ed.DefaultIfEmpty () join c in _dbContext.CountryDetails on cd.CountryId equals c.CountryId into cod from c in cod.DefaultIfEmpty () join pr in _dbContext.ProvinceDetails on cd.ProvinceId equals pr.ProvinceId into prd from pr in prd.DefaultIfEmpty () join d in _dbContext.DistrictDetail on cd.DistrictID equals d.DistrictID into dd from d in dd.DefaultIfEmpty () select new CandidateDetailsModel {
                        CandidateId = cd.CandidateId,
                            FirstName = cd.FirstName,
                            LastName = cd.LastName,
                            Email = cd.Email,
                            PhoneNumber = cd.PhoneNumber,
                            Gender = cd.GenderId == 1 ? "Male" : cd.GenderId == 2 ? "Female" : "Other",
                            EducationDegree = e.EducationDegreeName,
                            Profession = p.ProfessionName,
                            CandidateStatus = s.CandidateStatus,
                            TotalExperienceInYear = cd.TotalExperienceInYear,
                            RelevantExperienceInYear = cd.RelevantExperienceInYear,
                            IrrelevantExperienceInYear = cd.IrrelevantExperienceInYear,
                    }).ToList ();
                List<CandidateDetailsPdfModel> summary = new List<CandidateDetailsPdfModel> ();
                var serial = 1;
                foreach (var item in candidateDetail) {
                    summary.Add (new CandidateDetailsPdfModel {
                        SerialNumber = serial,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Email = item.Email,
                            PhoneNumber = item.PhoneNumber,
                            Gender = item.Gender,
                            EducationDegree = item.EducationDegree,
                            Profession = item.Profession,
                            Status = ((CandidateStatus) item.CandidateStatus).ToString (),
                            // Status = item.CandidateStatus == 0 ? "Shortlist Panding" : item.CandidateStatus == 1 ? "Interview Panding" : "Selection Panding",
                            TotalExperienceInYear = item.TotalExperienceInYear,
                            RelevantExperienceInYear = item.RelevantExperienceInYear,
                            IrrelevantExperienceInYear = item.IrrelevantExperienceInYear,
                    });
                    serial = serial + 1;
                }

                return await _pdfExportService.ExportToPdf (summary, "Pages/PdfTemplates/CandidateDetailReport.cshtml", true);
            } catch (Exception ex) {
                throw new Exception (ex.Message);
            }
        }
    }
}