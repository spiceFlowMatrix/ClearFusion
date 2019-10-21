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
    public class GetJournalBudgetLineSummaryPdfQueryHandler : IRequestHandler<GetJournalBudgetLineSummaryPdfQuery,byte[]>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private IHostingEnvironment _env;
        public GetJournalBudgetLineSummaryPdfQueryHandler(IPdfExportService pdfExportService,HumanitarianAssistanceDbContext dbContext,IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _pdfExportService=pdfExportService;
            _env=env;
        }
        public async Task<byte[]> Handle(GetJournalBudgetLineSummaryPdfQuery request, CancellationToken cancellationToken)
        {
           try
           {   
                var spBudgetReport = await _dbContext.LoadStoredProc("get_budgetline_expenditure_report")
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
                                          .ExecuteStoredProc<SPBudgetLineJournalReport>();
                FinalBudgetLineJournalReport pdfreport=new FinalBudgetLineJournalReport{
                    Logo=_env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath,
                    reportList=spBudgetReport
                };
                return await _pdfExportService.ExportToPdf(pdfreport, "Pages/PdfTemplates/JournalBudgetLineReport.cshtml"); 
           }
           catch(Exception ex)
           {
               throw new Exception(ex.Message);
           }
        }
       
    }
}