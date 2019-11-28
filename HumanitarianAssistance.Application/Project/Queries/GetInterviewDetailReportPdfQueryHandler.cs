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
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries {

    public class GetInterviewDetailReportPdfQueryHandler : IRequestHandler<GetInterviewDetailReportPdfQuery, byte[]> {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;

        public GetInterviewDetailReportPdfQueryHandler (HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService, IHostingEnvironment env) {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
        }

        public async Task<byte[]> Handle (GetInterviewDetailReportPdfQuery request, CancellationToken cancellationToken) {
            try {
                // model logic here 
                InterviewDetailsPdfModel summary = new InterviewDetailsPdfModel ()
                {
                        CandidateId = request.CandidateId,
                        CheckRadioPath = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/radio-checked.png")?.PhysicalPath,
                        UncheckRadioPath = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/radio-unchecked.png")?.PhysicalPath,
                        LogoPath = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/logo.jpg")?.PhysicalPath,
                        CheckedIconPath = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/check-box.png")?.PhysicalPath,
                        UnCheckedIconPath = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/uncheck-blank.png")?.PhysicalPath,
                        PersianChaName = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/PersianText.png")?.PhysicalPath
                };
                return await _pdfExportService.ExportToPdf (summary, "Pages/PdfTemplates/InterviewDetailReportPdf.cshtml");
            } catch (Exception ex) {
                throw new Exception (ex.Message);
            }
        }
    }
}