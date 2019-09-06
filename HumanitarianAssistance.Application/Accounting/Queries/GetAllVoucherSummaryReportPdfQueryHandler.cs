using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllVoucherSummaryReportPdfQueryHandler : IRequestHandler<GetAllVoucherSummaryReportPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;

        public GetAllVoucherSummaryReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
        }

        public async Task<byte[]> Handle(GetAllVoucherSummaryReportPdfQuery request, CancellationToken cancellationToken)
        {
            // model logic here

            List<VoucherSummaryReportPdfModel> summary = new List<VoucherSummaryReportPdfModel>();

            summary.Add(new VoucherSummaryReportPdfModel
            {
                Currency = "Afgani",
                Cheque = "554545",
                VoucherNo = "AFG-0001-14",
                Journal = "Hello",
                Date = "Helldfgdfgo",
                Region = "Helld fgdfgo",
                Description = "H dgf dfgello",
                TotalCredit = "Hello",
                TotalDebit = "Hello"
            });

            summary.Add(new VoucherSummaryReportPdfModel
            {
                Currency = "Hed fg dfllo",
                Cheque = "Heg llo",
                VoucherNo = "H dfg dfg dfg ello",
                Journal = "Hed gdfg llo",
                Date = "Heldfg dfglo",
                Region = "Hel dfg dflo",
                Description = "Hg dfg ello",
                TransactionDetails = new List<TransactionSummaryReportPdfModel> {
                     new TransactionSummaryReportPdfModel
                    {
                        AccountNo = "dfgd",
                        Description = "dfgd",
                        Debit = "dfgd",
                        Credit = "dfgd",
                        BudgetLine = "dfgd",
                        Area = "dfgd",
                        Sector = "dfgd",
                        Program = "dfgd",
                        Project = "dfgd",
                        Job = "dfgd"
                    },                     new TransactionSummaryReportPdfModel
                    {
                        AccountNo = "dfgd",
                        Description = "dfgd",
                        Debit = "dfgd",
                        Credit = "dfgd",
                        BudgetLine = "dfgd",
                        Area = "dfgd",
                        Sector = "dfgd",
                        Program = "dfgd",
                        Project = "dfgd",
                        Job = "dfgd"
                    },
                },
                TotalCredit = "15241",
                TotalDebit = "45465"
            });






            return await _pdfExportService.ExportToPdf(summary, "Pages/PdfTemplates/VoucherSummaryReport.cshtml");
        }
    }
}