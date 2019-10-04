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
                DateTime parsedDateTime;
                string voucherNoValue = null;
                string referenceNoValue = null;
                string descriptionValue = null;
                string journalNameValue = null;
                string dateValue = null;
                string formattedDate = null;
                // model logic here
                if (!string.IsNullOrEmpty(request.FilterValue))
                {
                    voucherNoValue = request.VoucherNoFlag ? request.FilterValue.ToLower().Trim() : null;
                    referenceNoValue = request.ReferenceNoFlag ? request.FilterValue.ToLower().Trim() : null;
                    descriptionValue = request.DescriptionFlag ? request.FilterValue.ToLower().Trim() : null;
                    journalNameValue = request.JournalNameFlag ? request.FilterValue.ToLower().Trim() : null;
                    dateValue = request.DateFlag ? request.FilterValue.ToLower().Trim() : null;
                    if(DateTime.TryParseExact(dateValue, "dd/mm/yyyy", 
                        CultureInfo.InvariantCulture, 
                        DateTimeStyles.None, 
                        out parsedDateTime))
                    {
                        formattedDate = parsedDateTime.ToString("yyyy-mm-dd");
                    }
                    else
                    {
                        formattedDate =dateValue;
                    }
                }
                VoucherSummaryMainReportPdfModel mainsummary=new VoucherSummaryMainReportPdfModel();
                
                mainsummary.Logo=_env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath;

                var spVoucherSummaryList=await _dbContext.LoadStoredProc("get_all_voucher_transaction_list")
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
                    }).Where(v=>!string.IsNullOrEmpty(request.FilterValue) ? (
                            (!string.IsNullOrEmpty(voucherNoValue)?v.VoucherNo.ToString().Trim().Contains(voucherNoValue):false) ||
                            (!string.IsNullOrEmpty(referenceNoValue)?v.ReferenceNo.Trim().ToLower().Contains(referenceNoValue):false) ||
                            (!string.IsNullOrEmpty(descriptionValue)?v.Description.Trim().ToLower().Contains(descriptionValue):false)||
                            (!string.IsNullOrEmpty(journalNameValue)?v.Journal.Trim().ToLower().Contains(journalNameValue):false) ||
                            (!string.IsNullOrEmpty(formattedDate)?v.Date.ToString().Trim().Contains(formattedDate):false)
                            ) : true).ToList();
               
               
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