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
                // model logic here OfficeDetail
                var candidateDetail = (from s in _dbContext.HiringRequestCandidateStatus
                    .Where (x => x.IsDeleted == false && x.CandidateStatus == 1 && x.HiringRequestId == request.HiringRequestId && x.CandidateId != null && x.ProjectId == request.ProjectId) 
                    join cd in _dbContext.CandidateDetails 
                    on s.CandidateId equals cd.CandidateId into cdl from cd in cdl.DefaultIfEmpty ()                     
                    join phd in _dbContext.ProjectHiringRequestDetail 
                    on s.HiringRequestId equals phd.HiringRequestId into phds from phd in phds.DefaultIfEmpty ()
                    join o in _dbContext.OfficeDetail 
                    on phd.OfficeId equals o.OfficeId into od from o in od.DefaultIfEmpty ()
                    join ddd in _dbContext.DesignationDetail 
                    on phd.PositionId equals ddd.DesignationId into ddl from ddd in ddl.DefaultIfEmpty () 
                    join p in _dbContext.ProfessionDetails 
                    on cd.ProfessionId equals p.ProfessionId into pd from p in pd.DefaultIfEmpty () 
                    join e in _dbContext.EducationDegreeMaster 
                    on cd.EducationDegreeId equals e.Id into ed from e in ed.DefaultIfEmpty () 
                    join c in _dbContext.CountryDetails 
                    on cd.CountryId equals c.CountryId into cod from c in cod.DefaultIfEmpty () 
                    join pr in _dbContext.ProvinceDetails
                     on cd.ProvinceId equals pr.ProvinceId into prd from pr in prd.DefaultIfEmpty () 
                     join d in _dbContext.DistrictDetail 
                     on cd.DistrictID equals d.DistrictID into dd from d in dd.DefaultIfEmpty () 
                     join et in _dbContext.EducationDegreeMaster 
                    on phd.EducationDegreeId equals et.Id into edt from et in edt.DefaultIfEmpty () 
                     join phr in _dbContext.ProfessionDetails 
                    on phd.ProfessionId equals phr.ProfessionId into pdhr from phr in pdhr.DefaultIfEmpty () 
                     select new CandidateDetailsPdfModel {
                        FirstName =cd.FirstName,
                        LastName = cd.LastName,
                        Email =cd.Email,
                        PhoneNumber = cd.PhoneNumber,
                        Sex = cd.GenderId == 1 ? "Male" : cd.GenderId == 2 ? "Female" : "Other",
                        RelevantExperienceInYear = cd.RelevantExperienceInYear,
                        IrrelevantExperienceInYear = cd.IrrelevantExperienceInYear,
                        Office = o.OfficeName,
                        Position = ddd.Designation,
                        RequiredEducation = et.Name,
                        RequiredProfession =  phr.ProfessionName,
                        RequiredExperience = phd.Experience + " Years",
                        CurrentEducation = e.Name,
                        CurrentProfession = p.ProfessionName,
                        CurrentExperience = cd.RelevantExperienceInYear + cd.IrrelevantExperienceInYear + " Years",                     
                    }).ToList ();

                List<CandidateDetailsPdfModel> summary = new List<CandidateDetailsPdfModel> ();
                if(candidateDetail.Count>0)
                {
                var serial = 1;
                foreach (var item in candidateDetail) {
                    summary.Add (new CandidateDetailsPdfModel {
                        SerialNumber = serial,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Email = item.Email,
                            PhoneNumber = item.PhoneNumber,
                            Sex = item.Sex,
                            Office = item.Office,
                            Position = item.Position,
                            RequiredEducation = item.RequiredEducation,
                            RequiredProfession = item.RequiredProfession,
                            RequiredExperience = item.RequiredExperience,
                            CurrentEducation = item.CurrentEducation,
                            CurrentProfession = item.CurrentProfession,
                            CurrentExperience = item.CurrentExperience,                           
                            RelevantExperienceInYear = item.RelevantExperienceInYear,
                            IrrelevantExperienceInYear = item.IrrelevantExperienceInYear
                    });
                    serial = serial + 1;
                  }
                }
                return await _pdfExportService.ExportToPdf (summary, "Pages/PdfTemplates/CandidateDetailReport.cshtml", true);
            } catch (Exception ex) {
                throw new Exception (ex.Message);
            }
        }
    }
}