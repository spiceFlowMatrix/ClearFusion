using System.IO;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Queries;
using HumanitarianAssistance.Application.HR.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Queries;
using HumanitarianAssistance.Common.Enums;
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

        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetAllEmployeeLeavePdf([FromBody] GetAllEmployeeLeavePdfQuery model)
        {
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "EmployeeLeavePdf.pdf");
        }

        [HttpPost]
        [Produces(contentType: "application/pdf")]  
        public async Task<IActionResult> GetHiringRequestFormPdf([FromBody] GetHiringRequestFormPdfQuery model)
        {  
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "HiringRequestForm.pdf");
        }
        [HttpPost]
        [Produces(contentType: "application/pdf")]  
        public async Task<IActionResult> GetProjectOtherDetailReportPdf([FromBody] GetProjectOtherDetailReportPdfQuery model)
        {  
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "ProjectOtherDetailReport.pdf");
        }
                   
        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> ProjectActivityReportPdf([FromBody] ProjectActivityReportPdfQuery model)
        {
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "ProjectActivityReport.pdf");
        }

        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetAnnualAppraisalReportPdf([FromBody] GetAnnualAppraisalReportPdfQuery model) 
        {
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "AnnualAppraisalReport.pdf");
        }

        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetTrailBalanceReportPdf([FromBody] GetTrialBalanceReportPdfQuery model) 
        {
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "TrialBalanceReport.pdf");
        }

        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetLedgerReportPdf([FromBody] GetLedgerReportPdfQuery model) 
        {
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "LedgerReport.pdf");
        }

        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetJournalTrialBalanceReportPdf([FromBody] GetJournalTrialBalanceReportPdfQuery model) 
        {
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "TrialBalanceReportJournal.pdf");
        }
    }
}

