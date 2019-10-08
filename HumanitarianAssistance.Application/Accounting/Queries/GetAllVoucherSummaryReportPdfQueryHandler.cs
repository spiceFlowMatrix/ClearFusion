using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Domain.Entities.Accounting;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllVoucherSummaryReportPdfQueryHandler : IRequestHandler<GetAllVoucherSummaryReportPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;

        public GetAllVoucherSummaryReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService,IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env=env;
        }

        public async Task<byte[]> Handle(GetAllVoucherSummaryReportPdfQuery request, CancellationToken cancellationToken)
        {
            try
            {
                VoucherSummaryMainReportPdfModel mainsummary=new VoucherSummaryMainReportPdfModel();
                
                mainsummary.Logo=_env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath;

                var spVoucherSummaryList=await _dbContext.LoadStoredProc("get_all_voucher_transaction_list")
                                                .WithSqlParam("voucherid", request.VoucherId)   
                                                .ExecuteStoredProc<VoucherSummaryReportNewPdfModel>();
                var groupedlist=spVoucherSummaryList.GroupBy(x=>new {
                    x.VoucherNo,
                    x.ReferenceNo,
                    x.VoucherDate,
                    x.ChequeNo,
                    x.VoucherDescription,
                    x.CurrencyCode,
                    x.JournalName,
                    x.OfficeName
                }).Select(s=>new VoucherSummaryReportPdfModel{
                    Currency=s.Key.CurrencyCode,
                    Cheque=s.Key.ChequeNo,
                    VoucherNo=s.Key.VoucherNo,
                    ReferenceNo=s.Key.ReferenceNo,
                    Journal=s.Key.JournalName,
                    Date=s.Key.VoucherDate.ToString(),
                    Region=s.Key.OfficeName,
                    Description=s.Key.VoucherDescription,
                    TotalCredit=s.Sum(x=>x.Credit).ToString(),
                    TotalDebit=s.Sum(x=>x.Debit).ToString(),
                    TransactionDetails=s.Select(ss=>new TransactionSummaryReportPdfModel{
                        AccountNo=ss.ChartOfAccountNewCode.ToString(),
                        Description=ss.TransactionDescription,
                        Debit=ss.Debit.ToString(),
                        Credit=ss.Credit.ToString(),
                        BudgetLine=ss.BudgetCode,
                        Project=ss.ProjectCode,
                        Job=ss.ProjectJobCode,
                        Sector=ss.SectorCode
                        }).ToList()
                    }).ToList();
               
               
                mainsummary.VoucherDetails=groupedlist;
                return await _pdfExportService.ExportToPdf(mainsummary, "Pages/PdfTemplates/VoucherSummaryReport.cshtml");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}