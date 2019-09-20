using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{

    public class GetHiringRequestFormPdfQueryHandler : IRequestHandler<GetHiringRequestFormPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;

        public GetHiringRequestFormPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
        }

        public async Task<byte[]> Handle(GetHiringRequestFormPdfQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // model logic here

                List<HiringRequestFormPdfModel> summary = new List<HiringRequestFormPdfModel>();

                summary.Add(new HiringRequestFormPdfModel
                {
                    Description = request.Description,
                    Position = request.Position,
                    Profession = request.Profession,
                    BudgetLine = request.BudgetLine,
                    TotalVacancies = request.TotalVacancies,
                    Office = request.Office,
                    FilledVacancies = request.FilledVacancies,
                    BasicPay = request.BasicPay,
                    jobGrade = request.jobGrade,
                    JobCategory = request.JobCategory,
                    MinimumEducation = request.MinimumEducation,
                    Organization = request.Organization,
                    ContractType = request.ContractType,
                    JobStatus = request.JobStatus,
                    Experience = request.Experience,
                    Background = request.Background,
                    SalaryRange = request.SalaryRange,
                    Province = request.Province,
                    Country = request.Country,
                    ContractDuration = request.ContractDuration,
                    Gender = request.Gender,
                    JobType = request.JobType,
                    Shift = request.Shift,
                    AnnouncingDate = request.AnnouncingDate,
                    ClosingDate = request.ClosingDate,
                    KnowladgeAndSkillRequired = request.KnowladgeAndSkillRequired,
                    SubmissionGuidline = request.SubmissionGuidline,
                    RequestedBy = request.RequestedBy,
                    Currency = request.Currency,
                    SpecificDutiesAndResponsiblities = request.SpecificDutiesAndResponsiblities
                });
                
                return await _pdfExportService.ExportToPdf(summary, "Pages/PdfTemplates/HiringRequestForm.cshtml");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
