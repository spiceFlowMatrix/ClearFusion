using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
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
            try
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

                // var spVoucherSummaryList = await _dbContext.LoadStoredProc("get_vouchersummaryreport_pdf")
                //                                             // .WithSqlParam("budgetlines", request.BudgetLines)
                //                                             .ExecuteStoredProc<SPVoucherSummaryReportPdfModel>();
                var spVoucherSummaryList=await _dbContext.LoadStoredProc("get_vouchersummaryreportvouchersbyfilter")
                                      .WithSqlParam("accounts", request.Accounts)
                                      .WithSqlParam("budgetlines", request.BudgetLines)
                                      .WithSqlParam("currencyid", request.Currency)
                                      .WithSqlParam("journals", request.Journals)
                                      .WithSqlParam("offices", request.Offices)
                                      .WithSqlParam("projectjobs", request.ProjectJobs)
                                      .WithSqlParam("projects", request.Projects)
                                      .WithSqlParam("recordtype", request.RecordType)
                                      .ExecuteStoredProc<SPVoucherSummaryReportModel>();

                // Console.WriteLine(spVoucherSummaryList);



                return await _pdfExportService.ExportToPdf(summary, "Pages/PdfTemplates/VoucherSummaryReport.cshtml");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}