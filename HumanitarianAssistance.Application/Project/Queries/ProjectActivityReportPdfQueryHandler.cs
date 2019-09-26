using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class ProjectActivityReportPdfQueryHandler : IRequestHandler<ProjectActivityReportPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;

        public ProjectActivityReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
        }

        public async Task<byte[]> Handle(ProjectActivityReportPdfQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // model logic here

                List<ProjectActivityReportPdfModel> summary = new List<ProjectActivityReportPdfModel>();

                summary.Add(new ProjectActivityReportPdfModel
                {
                    FilterValue=request.FilterValue
                });

                return await _pdfExportService.ExportToPdf(summary, "Pages/PdfTemplates/ProjectActivityReport.cshtml");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
