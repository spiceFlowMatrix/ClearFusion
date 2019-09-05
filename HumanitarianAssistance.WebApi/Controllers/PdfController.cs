using System.IO;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers
{
    [ApiController]
    [Route("api/Pdf/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.PdfExport))]
    //[Authorize]
    public class PdfController : BaseController
    {
        private readonly IHostingEnvironment _hosting;
        public PdfController(IHostingEnvironment hosting)
        {
            _hosting = hosting;
        }
        [HttpGet]
        public async Task<ApiResponse> GetAllChartOfAccountHierarchyPdf()
        {
            return await _mediator.Send(new GetAllAccountsInHierarchyQuery());
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllVoucherSummaryReportPdf()
        {
            return await _mediator.Send(new GetAllVoucherSummaryReportPdfQuery());
            // return File(file, "application/pdf", "VoucherSummaryReport.pdf");
            // return Ok(file);
        }
        [HttpGet]
        [Produces(contentType:"application/pdf")]
        public IActionResult CreatePdf()
        {
            var _stream = new MemoryStream();
            using (var pdfWriter = new PdfWriter(_stream))
            {
                pdfWriter.SetCloseStream(false);
                using (var document = HtmlConverter.ConvertToDocument(new FileStream(_hosting.WebRootPath + "/test.html",FileMode.Open), pdfWriter))
                {
                  
                }
            }
            _stream.Position = 0;
            return File(_stream.ToArray(), "application/pdf", "TestFile.pdf");
        }

    }
}