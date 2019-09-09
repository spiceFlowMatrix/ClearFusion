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
        [HttpGet]
        public async Task<ApiResponse> GetAllChartOfAccountHierarchyPdf()
        {
            return await _mediator.Send(new GetAllAccountsInHierarchyQuery());
        }

        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetAllVoucherSummaryReportPdf([FromBody] GetAllVoucherSummaryReportPdfQuery model)
        {
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "VoucherSummaryReport.pdf");
        }
    }
}