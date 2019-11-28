using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{

        public class GetInterviewDetailReportPdfQueryHandler : IRequestHandler<GetInterviewDetailReportPdfQuery, byte[]> {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;

        public GetInterviewDetailReportPdfQueryHandler (HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService) {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
        }

        public async Task<byte[]> Handle (GetInterviewDetailReportPdfQuery request, CancellationToken cancellationToken) 
        {
               try {
                // model logic here 
                List<InterviewDetailsModel> summary = new List<InterviewDetailsModel> ();
                summary.Add (new InterviewDetailsModel {
                       CandidateId = request.CandidateId
                });

                return await _pdfExportService.ExportToPdf (summary, "Pages/PdfTemplates/InterviewDetailReportPdf.cshtml");
            } catch (Exception ex) {
                throw new Exception (ex.Message);
            }  
        }
    }
}