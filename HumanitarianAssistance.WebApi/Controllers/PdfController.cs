using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Queries;
using HumanitarianAssistance.Application.HR.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Application.Project.Queries;
using HumanitarianAssistance.Application.Store.Queries;
using HumanitarianAssistance.Common.Enums;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize ("view:vouchers")]
        public async Task<IActionResult> GetAllVoucherSummaryReportPdf([FromBody] GetAllVoucherSummaryReportPdfQuery model)
        {
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "VoucherSummaryReport.pdf");
        }

        [HttpPost]
        //[Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetAllEmployeeLeavePdf([FromBody] GetAllEmployeeLeavePdfQuery model)
        {
            try
            {
                var file = await _mediator.Send(model);
                return File(file, "application/pdf", "EmployeeLeaveReport.pdf");
            }
            catch (Exception ex)
            {

                Response.Headers.Add("ExMessage", ex.Message);
                return BadRequest();
            }
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

        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetJournalBudgetLineSummaryPdf([FromBody] GetJournalBudgetLineSummaryPdfQuery model)
        {
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "BudgetLineSummaryJournal.pdf");
        }

        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetJournalLedgerReportPdf([FromBody] GetJournalLedgerReportPdfQuery model)
        {
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "LedgerReportJournal.pdf");
        }

        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetCriteriaEvaluationReportPdf([FromBody] GetCriteriaEvaluationDetailReportPdfQuery model)
        {
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "CriteriaEvaluationDetailReport.pdf");
        }

        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetStorePurchasePdf([FromBody] GetStorePurchasePdfQuery model)
        {
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "StorePurchaseReport.pdf");
        }

        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetJournalReportPdf([FromBody] GetJournalReportPdfQuery model)
        {
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "JournalReport.pdf");
        }

        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetEmployeeExitInteviewPdf([FromBody] GetEmployeeExitInterviewPdfQuery model)
        {
            try
            {
                var file = await _mediator.Send(model);
                return File(file, "application/pdf", "EmployeeExitInterviewReport.pdf");
            }
            catch (Exception ex)
            {

                Response.Headers.Add("ExMessage", ex.Message);
                return BadRequest();
            }
        }




        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetCandidateDetailReportPdf([FromBody]GetCandidateDetailReportPdfQuery model)
        {
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "CandidateDetailReport.pdf");
        }

        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetInterviewDetailReportPdf([FromBody]GetInterviewDetailReportPdfQuery model)
        {
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "InterviewDetailReport.pdf");
        }

        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetJournalReportExcel([FromBody]GetJournalReportExcelQuery model)
        {
            var file = await _mediator.Send(model);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "JournalReport.xlsx");
        }

        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> EmployeeAnnualTunoverReport([FromBody]EmployeeAnnualTunoverReportPdfQuery model)
        {
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "EmployeeAnnualTunoverReport.pdf");
        }
        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetLogisticGoodsNoteReportPdf([FromBody] long LogisticRequestId)
        {
            GetLogisticGoodsNoteReportPdfQuery model = new GetLogisticGoodsNoteReportPdfQuery();
            model.LogisticRequestId = LogisticRequestId;
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "LogisticGoodsNoteReport.pdf");
        }


        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetEmployeePensionPdf([FromBody] GetEmployeePensionReportPdfQuery model)
        {
            try
            {
                var file = await _mediator.Send(model);
                return File(file, "application/pdf", "EmployeePensionReport.pdf");
            }
            catch (Exception ex)
            {
                Response.Headers.Add("ExMessage", ex.Message);
                return BadRequest();
            }
        }
        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetEmployeeSalaryTaxPdf([FromBody] GetEmployeeSalaryTaxReportPdfQuery model)
        {
            try
            {
                var file = await _mediator.Send(model);
                return File(file, "application/pdf", "EmployeeSalaryAndTaxReport.pdf");
            }
            catch (Exception ex)
            {
                Response.Headers.Add("ExMessage", ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetEmployeeContractReportPdf([FromBody]GetEmployeeContractReportPdfQuery model)
        {
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "ContractDetailReport.pdf");
        }

        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> ExportEmployeeLeavePdf([FromBody]int id)
        {
            ExportEmployeeLeavePdfQuery model = new ExportEmployeeLeavePdfQuery
            {
                EmployeeId = id
            };
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "EmployeeLeave.pdf");
        }
        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetMonthlyPaySlipPdf([FromBody] MonthlyPaySlipReportPdfQuery model)
        {
            var file = await _mediator.Send(model);
            return File(file, "application/pdf", "MonthlyPaySlipReport.pdf");
        }

        [HttpPost]
        [Produces(contentType: "application/pdf")]
        public async Task<IActionResult> GetEmployeesPayrollExcel([FromBody]GetEmployeesPayrollExcelQuery model)
        {
            try
            {
                var file = await _mediator.Send(model);
                
                if(file is string)
                {
                    Response.Headers.Add("ExMessage", file.ToString());
                    return Ok();
                }
                else
                {
                    return File((byte[])file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeePayrollExcel.xlsx");
                }
            }
            catch (Exception ex)
            {
                Response.Headers.Add("ExMessage", ex.Message);
                return BadRequest();
            }
        }
    }
}

