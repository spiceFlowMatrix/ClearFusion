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
    public class GetJournalLedgerReportPdfQueryHandler : IRequestHandler<GetJournalLedgerReportPdfQuery,byte[]>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private IHostingEnvironment _env;
        public GetJournalLedgerReportPdfQueryHandler(IPdfExportService pdfExportService,HumanitarianAssistanceDbContext dbContext,IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _pdfExportService=pdfExportService;
            _env=env;
        }
        public async Task<byte[]> Handle(GetJournalLedgerReportPdfQuery request, CancellationToken cancellationToken)
        {
           try
           {   
               List<JournalLedgerReportModel> pdfReport =new List<JournalLedgerReportModel>();
               JournalLedgerMainReportModel mainReport= new JournalLedgerMainReportModel();
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

                var groupedAccountReport=spJournalReport.GroupBy(x => new {x.ChartOfAccountNewId,x.AccountCode,x.AccountName}).ToList();
               
               foreach (var accountslist in groupedAccountReport)
               {
                   List<AccountTransactionList> transactionlist=new List<AccountTransactionList>();
                   foreach(var record in accountslist){
                    transactionlist.Add(new AccountTransactionList{
                        Group=record.BudgetCode,
                        TransactionDate=record.TransactionDate.ToString("dd/MM/yyyy"),
                        VoucherNo=record.ReferenceNo,
                        Description=record.TransactionDescription,
                        Currency= await _dbContext.CurrencyDetails.Where(x=>x.IsDeleted==false && x.CurrencyId==record.Currency).Select(y=>y.CurrencyCode).FirstOrDefaultAsync(),
                        Rate= ((request.RecordType==1)?1:await getExchangeRate(record.Currency,request.CurrencyId,record.TransactionDate)),
                        Debit=record.DebitAmount,
                        Credit=record.CreditAmount,
                        IsVoucherVerified=  await _dbContext.VoucherDetail.Where(x=>x.IsDeleted==false && x.VoucherNo==record.VoucherNo).Select(y=>y.IsVoucherVerified).FirstOrDefaultAsync()
                    });
                   }
                   pdfReport.Add(new JournalLedgerReportModel{
                       AccountCode=accountslist.Key.AccountCode,
                       AccountName=accountslist.Key.AccountName,
                       TotalDebit=transactionlist.Sum(x=>x.Debit),
                       TotalCredit=transactionlist.Sum(x=>x.Credit),
                       Balance=(transactionlist.Sum(x=>x.Debit)-transactionlist.Sum(x=>x.Credit)),
                       TransactionList=transactionlist
                   });
               }
                mainReport.Logo=_env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath;
                mainReport.mainList= pdfReport;
                mainReport.FromDate=request.fromdate.ToString("dd/MM/yyyy");
                mainReport.ToDate=request.todate.ToString("dd/MM/yyyy");
                mainReport.RecordType=((request.RecordType==1)?"Single":"Consolidated");
                mainReport.Currency=await _dbContext.CurrencyDetails.Where(x=>x.IsDeleted==false && x.CurrencyId==request.CurrencyId).Select(y=>y.CurrencyCode).FirstOrDefaultAsync();
                return await _pdfExportService.ExportToPdf(mainReport, "Pages/PdfTemplates/JournalLedgerReport.cshtml"); 
           }
           catch(Exception ex)
           {
               throw new Exception(ex.Message);
           }
        }

        private async Task<double> getExchangeRate(double fromCurrency,double toCurrency,DateTime TransactionDate)
        {
            return (double) await _dbContext.ExchangeRateDetail.Where(x=>x.IsDeleted==false && x.FromCurrency==fromCurrency && x.ToCurrency==toCurrency && x.Date.Date<=TransactionDate.Date)
            .OrderByDescending(z=>z.Date).Select(y=>y.Rate).FirstOrDefaultAsync();
        }
       
    }
}