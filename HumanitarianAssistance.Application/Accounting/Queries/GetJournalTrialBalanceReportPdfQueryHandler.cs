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
    public class GetJournalTrialBalanceReportPdfQueryHandler : IRequestHandler<GetJournalTrialBalanceReportPdfQuery,byte[]>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private IHostingEnvironment _env;
        public GetJournalTrialBalanceReportPdfQueryHandler(IPdfExportService pdfExportService,HumanitarianAssistanceDbContext dbContext,IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _pdfExportService=pdfExportService;
            _env=env;
        }
        public async Task<byte[]> Handle(GetJournalTrialBalanceReportPdfQuery request, CancellationToken cancellationToken)
        {
           try
           {   
                double? debitSumForReport = 0.0;
                double? creditSumForReport = 0.0; 
                double? totalSumForReport = 0.0; 
                JournalTrialBalancePdfModel pdfreport;
                List<JournalTrialBalanceMainPdfList> mainList=new List<JournalTrialBalanceMainPdfList>();
                List<LedgerModel> finalTrialBalanceList = new List<LedgerModel>();
                //int voucherDetailsCount = 0;
                List<JournalVoucherViewModel> listJournalView = new List<JournalVoucherViewModel>();

                if (request != null)
                {

                    //get Journal Report from sp get_journal_report by passing parameters
                    var spJournalReport = await _dbContext.LoadStoredProc("get_journal_report")
                                          .WithSqlParam("currencyid", request.CurrencyId)
                                          .WithSqlParam("recordtype", request.RecordType)
                                          .WithSqlParam("fromdate", request.fromdate.ToString())
                                          .WithSqlParam("todate", request.todate.ToString())
                                          .WithSqlParam("officelist", request.OfficesList)
                                          .WithSqlParam("journalno", request.JournalCode)
                                          .WithSqlParam("accountslist", request.AccountLists)
                                          .WithSqlParam("project", request.Project)
                                          .WithSqlParam("budgetline", request.BudgetLine)
                                          .WithSqlParam("projectjob", request.JobCode)
                                          .ExecuteStoredProc<SPJournalReport>();


                    listJournalView = spJournalReport.Select(x => new JournalVoucherViewModel
                    {
                        AccountCode = x.AccountCode,
                        ChartOfAccountNewId = x.ChartOfAccountNewId,
                        JournalCode = x.JournalCode,
                        CreditAmount = x.CreditAmount,
                        CurrencyId = x.Currency,
                        DebitAmount = x.DebitAmount,
                        ReferenceNo = x.ReferenceNo,
                        TransactionDate = x.TransactionDate,
                        TransactionDescription = x.TransactionDescription,
                        VoucherNo = x.VoucherNo,
                        AccountName = x.AccountName,
                        // Project = x.ProjectCode,
                        // BudgetLineDescription = x.BudgetCode
                    }).ToList();

                    var journalReport = spJournalReport.GroupBy(x => x.ChartOfAccountNewId).ToList();

                    List<JournalReportViewModel> journalReportList = new List<JournalReportViewModel>();

                    foreach (var accountItem in journalReport)
                    {
                        journalReportList.Add(new JournalReportViewModel
                        {
                            ChartOfAccountNewId = accountItem.Key,
                            AccountCode = accountItem.FirstOrDefault(x => x.ChartOfAccountNewId == accountItem.Key).AccountCode,
                            AccountName = accountItem.FirstOrDefault().AccountName,
                            DebitAmount = Math.Round(Convert.ToDecimal(accountItem.Sum(x => x.DebitAmount)), 4),
                            CreditAmount = Math.Round(Convert.ToDecimal(accountItem.Sum(x => x.CreditAmount)), 4),
                            Balance = Math.Round(Convert.ToDecimal(accountItem.Sum(x => x.DebitAmount) - accountItem.Sum(x => x.CreditAmount)), 4)
                        });
                    }
                    foreach(var item in journalReportList)
                    {   
                        debitSumForReport+=Convert.ToDouble(item.DebitAmount);
                        creditSumForReport+=Convert.ToDouble(item.CreditAmount);
                        totalSumForReport+=Convert.ToDouble(item.DebitAmount-item.CreditAmount);
                        mainList.Add(new JournalTrialBalanceMainPdfList(){
                            Group=item.AccountCode,
                            AccountDescription=item.AccountName,
                            DebitAmount=Convert.ToDouble(item.DebitAmount),
                            CreditAmount=Convert.ToDouble(item.CreditAmount),
                            Balance=Convert.ToDouble(item.DebitAmount - item.CreditAmount)
                        });
                    }
                    pdfreport=new JournalTrialBalancePdfModel(){
                        Logo=_env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath,
                        reportList=mainList,
                        TotalDebit=debitSumForReport,
                        TotalCredit=creditSumForReport,
                        TotalBalance=totalSumForReport
                    };

                }
                else
                {
                   pdfreport=new JournalTrialBalancePdfModel(){
                        Logo=_env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath,
                        reportList=mainList,
                        TotalDebit=debitSumForReport,
                        TotalCredit=creditSumForReport,
                        TotalBalance=totalSumForReport
                    };
                }
                return await _pdfExportService.ExportToPdf(pdfreport, "Pages/PdfTemplates/JournalTrialBalanceReport.cshtml"); 
           }
           catch(Exception ex)
           {
               throw new Exception(ex.Message);
           }
        }
       
    }
}