using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.CommonServicesInterface;
using Microsoft.AspNetCore.Hosting;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetLedgerReportPdfQueryHandler : IRequestHandler<GetLedgerReportPdfQuery,byte[]>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private IHostingEnvironment _env;
        public GetLedgerReportPdfQueryHandler(IPdfExportService pdfExportService,HumanitarianAssistanceDbContext dbContext,IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _pdfExportService=pdfExportService;
            _env=env;
        }
        public async Task<byte[]> Handle(GetLedgerReportPdfQuery request, CancellationToken cancellationToken)
        {
            try
            {
                double? debitSumForReport = 0.0;
                double? creditSumForReport = 0.0; 
                List<LedgerReportMainPdfList> mainList=new List<LedgerReportMainPdfList>();
                List<LedgerModel> closingLedgerList = new List<LedgerModel>();
                List<LedgerModel> openingLedgerList = new List<LedgerModel>();

                if (request != null)
                {

                    var allCurrencies = await _dbContext.CurrencyDetails.Where(x => x.IsDeleted == false).ToListAsync();

                    Boolean isRecordPresenntForOffice = await _dbContext.VoucherDetail
                                                                .AnyAsync(x => x.IsDeleted == false &&
                                                                          request.OfficeIdList.Contains(x.OfficeId.Value) &&
                                                                          x.VoucherDate.Date >= request.fromdate.Date &&
                                                                          x.VoucherDate.Date <= request.todate.Date);

                    if (isRecordPresenntForOffice)
                    {
                        if (request.RecordType == 1)
                        {

                            var spLedgerReportOpening = await _dbContext.LoadStoredProc("get_ledger_report")
                                                                  .WithSqlParam("currency", request.CurrencyId)
                                                                  .WithSqlParam("recordtype", request.RecordType)
                                                                  .WithSqlParam("fromdate", request.fromdate.ToString())
                                                                  .WithSqlParam("todate", request.todate.ToString())
                                                                  .WithSqlParam("officelist", request.OfficeIdList)
                                                                  .WithSqlParam("accountslist", request.accountLists)
                                                                  .WithSqlParam("openingbalance", true)
                                                                  .ExecuteStoredProc<SPLedgerReport>();

                            //Opening Calculation
                            openingLedgerList = spLedgerReportOpening.Select(x => new LedgerModel
                            {
                                ChartOfAccountNewId = x.ChartOfAccountNewId,
                                AccountName = x.AccountName,
                                VoucherNo = x.VoucherNo.ToString(),
                                ChartAccountName = x.AccountName,
                                Description = x.Description,
                                VoucherReferenceNo = x.VoucherReferenceNo,
                                CurrencyName = x.CurrencyName,
                                TransactionDate = x.TransactionDate,
                                ChartOfAccountNewCode = x.ChartOfAccountNewCode,
                                CreditAmount = x.CreditAmount,
                                DebitAmount = x.DebitAmount
                            }).ToList();

                            var spLedgerReportClosing = await _dbContext.LoadStoredProc("get_ledger_report")
                                                                 .WithSqlParam("currency", request.CurrencyId)
                                                                 .WithSqlParam("recordtype", request.RecordType)
                                                                 .WithSqlParam("fromdate", request.fromdate.ToString())
                                                                 .WithSqlParam("todate", request.todate.ToString())
                                                                 .WithSqlParam("officelist", request.OfficeIdList)
                                                                 .WithSqlParam("accountslist", request.accountLists)
                                                                 .WithSqlParam("openingbalance", false)
                                                                 .ExecuteStoredProc<SPLedgerReport>();

                            closingLedgerList = spLedgerReportClosing.Select(x => new LedgerModel
                            {
                                ChartOfAccountNewId = x.ChartOfAccountNewId,
                                AccountName = x.AccountName,
                                VoucherNo = x.VoucherNo.ToString(),
                                ChartAccountName = x.AccountName,
                                Description = x.Description,
                                VoucherReferenceNo = x.VoucherReferenceNo,
                                CurrencyName = x.CurrencyName,
                                TransactionDate = x.TransactionDate,
                                ChartOfAccountNewCode = x.ChartOfAccountNewCode,
                                CreditAmount = x.CreditAmount,
                                DebitAmount = x.DebitAmount
                            }).ToList();

                            mainList=spLedgerReportClosing.Select(x=> new LedgerReportMainPdfList(){
                                //VoucherDate=x.VoucherDate.ToString("dd/MM/yyyy"),
                                ReferenceNo=x.VoucherReferenceNo,
                                LineDescription=x.Description,
                                Currency=x.CurrencyName,
                                DebitAmount=x.DebitAmount,
                                CreditAmount=x.CreditAmount,
                                StatusText=(x.IsVoucherVerified)?"V":"NV"                                
                            }).ToList();
                            debitSumForReport=mainList.Sum(x=>x.DebitAmount);
                            creditSumForReport=mainList.Sum(x=>x.CreditAmount);
                            // response.data.AccountOpendingAndClosingBL = new AccountOpendingAndClosingBL
                            // {
                            //     OpeningBalance = Math.Round(Convert.ToDouble(openingLedgerList.Sum(x => x.DebitAmount) - openingLedgerList.Sum(x => x.CreditAmount))),
                            //     //ClosingBalance = opening + closing
                            //     ClosingBalance = Math.Round(Convert.ToDouble(openingLedgerList.Sum(x => x.DebitAmount) - openingLedgerList.Sum(x => x.CreditAmount) + (closingLedgerList.Sum(x => x.DebitAmount) - closingLedgerList.Sum(x => x.CreditAmount))))

                            // };
                        }
                        else
                        {
                            //Consolidate

                            var spLedgerReportOpening = await _dbContext.LoadStoredProc("get_ledger_report")
                                                                   .WithSqlParam("currency", request.CurrencyId)
                                                                   .WithSqlParam("recordtype", request.RecordType)
                                                                   .WithSqlParam("fromdate", request.fromdate.ToString("MM/dd/yyyy"))
                                                                   .WithSqlParam("todate", "")
                                                                   .WithSqlParam("officelist", request.OfficeIdList)
                                                                   .WithSqlParam("accountslist", request.accountLists)
                                                                   .WithSqlParam("openingbalance", true)
                                                                   .ExecuteStoredProc<SPLedgerReport>();

                            openingLedgerList = spLedgerReportOpening.Select(x => new LedgerModel
                            {
                                ChartOfAccountNewId = x.ChartOfAccountNewId,
                                AccountName = x.AccountName,
                                VoucherNo = x.VoucherNo.ToString(),
                                ChartAccountName = x.AccountName,
                                Description = x.Description,
                                VoucherReferenceNo = x.VoucherReferenceNo,
                                CurrencyName = x.CurrencyName,
                                TransactionDate = x.TransactionDate,
                                ChartOfAccountNewCode = x.ChartOfAccountNewCode,
                                CreditAmount = x.CreditAmount,
                                DebitAmount = x.DebitAmount
                            }).ToList();


                            var spLedgerReportClosing = await _dbContext.LoadStoredProc("get_ledger_report")
                                                                  .WithSqlParam("currency", request.CurrencyId)
                                                                  .WithSqlParam("recordtype", request.RecordType)
                                                                  .WithSqlParam("fromdate", request.fromdate.ToString("MM/dd/yyyy"))
                                                                  .WithSqlParam("todate", request.todate.ToString("MM/dd/yyyy"))
                                                                  .WithSqlParam("officelist", request.OfficeIdList)
                                                                  .WithSqlParam("accountslist", request.accountLists)
                                                                  .WithSqlParam("openingbalance", false)
                                                                  .ExecuteStoredProc<SPLedgerReport>();

                            closingLedgerList = spLedgerReportClosing.Select(x => new LedgerModel
                            {
                                ChartOfAccountNewId = x.ChartOfAccountNewId,
                                AccountName = x.AccountName,
                                VoucherNo = x.VoucherNo.ToString(),
                                ChartAccountName = x.AccountName,
                                Description = x.Description,
                                VoucherReferenceNo = x.VoucherReferenceNo,
                                CurrencyName = x.CurrencyName,
                                TransactionDate = x.TransactionDate,
                                ChartOfAccountNewCode = x.ChartOfAccountNewCode,
                                CreditAmount = x.CreditAmount,
                                DebitAmount = x.DebitAmount
                            }).ToList();
                            
                            mainList=spLedgerReportClosing.Select(x=> new LedgerReportMainPdfList(){
                                //VoucherDate=x.VoucherDate.ToString("dd/MM/yyyy"),
                                ReferenceNo=x.VoucherReferenceNo,
                                LineDescription=x.Description,
                                Currency=x.CurrencyName,
                                DebitAmount=x.DebitAmount,
                                CreditAmount=x.CreditAmount,
                                StatusText=(x.IsVoucherVerified)?"V":"NV"                                
                            }).ToList();
                            debitSumForReport=mainList.Sum(x=>x.DebitAmount);
                            creditSumForReport=mainList.Sum(x=>x.CreditAmount);
                        }
                    }
                }

                //#region "report data"

                var ledgerByAccount = closingLedgerList.GroupBy(x => x.ChartOfAccountNewId).ToList();

                List<LedgerReportViewModel> ledgerReportFinal = new List<LedgerReportViewModel>();

                foreach (var accountItem in ledgerByAccount)
                {
                    ledgerReportFinal.Add(new LedgerReportViewModel
                    {
                        AccountName = accountItem.FirstOrDefault().AccountName,
                        LedgerList = accountItem.ToList(),
                        DebitAmount = Math.Round(Convert.ToDecimal(accountItem.Sum(x => x.DebitAmount)), 4),
                        CreditAmount = Math.Round(Convert.ToDecimal(accountItem.Sum(x => x.CreditAmount)), 4),
                        Balance = Math.Round(Convert.ToDecimal(accountItem.Sum(x => x.DebitAmount) - accountItem.Sum(x => x.CreditAmount)), 4)
                    });
                }
                LedgerReportPdfModel pdfreport=new LedgerReportPdfModel(){
                    Logo=_env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath,
                    reportList=mainList,
                    TotalDebit=debitSumForReport,
                    TotalCredit=creditSumForReport
                };
                return await _pdfExportService.ExportToPdf(pdfreport, "Pages/PdfTemplates/LedgerReport.cshtml"); 
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}