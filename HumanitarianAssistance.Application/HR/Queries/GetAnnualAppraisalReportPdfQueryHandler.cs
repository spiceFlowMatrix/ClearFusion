using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAnnualAppraisalReportPdfQueryHandler : IRequestHandler<GetAnnualAppraisalReportPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;

        public GetAnnualAppraisalReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
        }

        public async Task<byte[]> Handle(GetAnnualAppraisalReportPdfQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // model logic here

                List<AnnualAppraisalReportPdfModel> summary = new List<AnnualAppraisalReportPdfModel>();

                //summary.Add(new ProjectActivityReportPdfModel
                //{
                //    FilterValue = request.FilterValue
                //});

                return await _pdfExportService.ExportToPdf(summary, "Pages/PdfTemplates/AnnualAppraisalReport.cshtml");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

