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
    public class GetJournalReportPdfQueryHandler : IRequestHandler<GetJournalReportPdfQuery,byte[]>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private IHostingEnvironment _env;
        public GetJournalReportPdfQueryHandler(IPdfExportService pdfExportService,HumanitarianAssistanceDbContext dbContext,IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _pdfExportService=pdfExportService;
            _env=env;
        }
        public async Task<byte[]> Handle(GetJournalReportPdfQuery request, CancellationToken cancellationToken)
        {
           try
           {  
                MainJournalReportPdfModel pdfreport;
                List<JournalReportPdfModel> listJournalView = new List<JournalReportPdfModel>();
                double? Balance =0.0;
                double? TotalDebit = 0.0;
                double? TotalCredit = 0.0;
                if (request != null)
                {
                    var currencyList = await _dbContext.CurrencyDetails.ToListAsync();
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
                    
                    listJournalView = spJournalReport.Select(x => new JournalReportPdfModel
                    {
                        TransactionDate = x.TransactionDate.ToString("dd/MM/yyyy"),
                        ReferenceNo = x.ReferenceNo,
                        AccountCode = x.AccountCode,
                        TransactionDescription = x.TransactionDescription,
                        DebitAmount = x.DebitAmount,
                        CreditAmount = x.CreditAmount,
                        Currency = currencyList.Where(y=>y.CurrencyId==x.Currency).Select(z=>z.CurrencyCode).FirstOrDefault(),
                        BudgetLine = x.BudgetCode,
                        Project = x.ProjectCode
                    }).ToList();
                    
                    foreach( var item in listJournalView){
                        item.Balance = Balance + (item.DebitAmount - item.CreditAmount);
                        Balance = item.Balance;
                    }
                    
                    TotalDebit= listJournalView.Sum(x=>x.DebitAmount);
                    TotalCredit= listJournalView.Sum(x=>x.CreditAmount);

                    pdfreport=new MainJournalReportPdfModel(){
                        Logo=_env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath,
                        reportList=listJournalView,
                        fromDate = request.fromdate.ToString("dd/MM/yyyy"),
                        toDate = request.todate.ToString("dd/MM/yyyy"),
                        TotalDebit = TotalDebit,
                        TotalCredit =TotalCredit
                    };
                }
                else
                {
                   pdfreport=new MainJournalReportPdfModel(){
                        Logo=_env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath,
                        reportList=listJournalView,
                        fromDate = request.fromdate.ToString("dd/MM/yyyy"),
                        toDate = request.todate.ToString("dd/MM/yyyy"),
                        TotalDebit = TotalDebit,
                        TotalCredit =TotalCredit
                    };
                }
                return await _pdfExportService.ExportToPdf(pdfreport, "Pages/PdfTemplates/JournalReport.cshtml"); 
           }
           catch(Exception ex)
           {
               throw new Exception(ex.Message);
           }
        }
       
    }
}