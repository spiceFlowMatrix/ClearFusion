using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeExitInterviewPdfQueryHandler : IRequestHandler<GetEmployeeExitInterviewPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;
        public GetEmployeeExitInterviewPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService, IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
        }

        public async Task<byte[]> Handle(GetEmployeeExitInterviewPdfQuery request, CancellationToken cancellationToken)
        {
          
            return await _pdfExportService.ExportToPdf(request, "Pages/PdfTemplates/EmployeeExitInterviewReport.cshtml");

        }
    }
}