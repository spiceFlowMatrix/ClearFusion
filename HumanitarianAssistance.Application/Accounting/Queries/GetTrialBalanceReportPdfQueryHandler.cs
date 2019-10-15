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
    public class GetTrialBalanceReportPdfQueryHandler : IRequestHandler<GetTrialBalanceReportPdfQuery,byte[]>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private IHostingEnvironment _env;
        public GetTrialBalanceReportPdfQueryHandler(IPdfExportService pdfExportService,HumanitarianAssistanceDbContext dbContext,IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _pdfExportService=pdfExportService;
            _env=env;
        }
        public async Task<byte[]> Handle(GetTrialBalanceReportPdfQuery model, CancellationToken cancellationToken)
        {
           try
           {   
                double? debitSumForReport = 0.0;
                double? creditSumForReport = 0.0; 
                List<TrialBalanceMainPdfList> mainList=new List<TrialBalanceMainPdfList>();
                List<LedgerModel> finalTrialBalanceList = new List<LedgerModel>();
                DateTime defaultdate = new DateTime(DateTime.UtcNow.Year, 1, 1);
                if (model.fromdate == null && model.todate == null)
                {
                    model.fromdate = new DateTime(DateTime.UtcNow.Year, 1, 1);
                    model.todate = DateTime.UtcNow;
                }


                if (model != null)
                {
                    ICollection<CurrencyDetails> allCurrencies = await _dbContext.CurrencyDetails.Where(x => x.IsDeleted == false).ToListAsync();

                    var accountFourthLevel = model.accountLists;


                    // Single
                    if (model.RecordType == 1)
                    {
                        //get trialbalance report from Stored Procedure get_trialbalance_report
                        var spTrialBalanceReport = await _dbContext.LoadStoredProc("get_trialbalance_report")
                                                                    .WithSqlParam("currency", model.CurrencyId)
                                                                    .WithSqlParam("recordtype", model.RecordType)
                                                                    .WithSqlParam("fromdate", model.fromdate.ToString())
                                                                    .WithSqlParam("todate", model.todate.ToString())
                                                                    .WithSqlParam("officelist", model.OfficesList)
                                                                    .WithSqlParam("accountslist", model.accountLists)
                                                                    .ExecuteStoredProc<SP_TrialBalanceModel>();

                        var transactionDetail = spTrialBalanceReport.Select(x => new LedgerModel
                        {
                            AccountName = x.AccountName,
                            ChartOfAccountNewId = x.ChartOfAccountNewId,
                            Description = x.Description,
                            CurrencyName = x.CurrencyName,
                            CreditAmount = x.CreditAmount,
                            DebitAmount = x.DebitAmount,
                            TransactionDate = x.TransactionDate,
                            ChartOfAccountNewCode = x.ChartOfAccountNewCode

                        }).ToList();

                        List<LedgerModel> transactionDetail1 = new List<LedgerModel>();

                        var accountGroup = transactionDetail.GroupBy(x => x.ChartOfAccountNewId);

                        foreach (var item in accountGroup)
                        {
                            LedgerModel obj = new LedgerModel();

                            obj = item.FirstOrDefault();

                            var debit = Math.Round((decimal)item.Sum(x => x.DebitAmount), 4);
                            var credit = Math.Round((decimal)item.Sum(x => x.CreditAmount),4);

                            if (debit > credit)
                            {
                                obj.DebitAmount = Convert.ToDouble(Math.Round((debit - credit),4));
                                obj.CreditAmount = 0;
                            }
                            else if (debit < credit)
                            {
                                obj.DebitAmount = 0;
                                obj.CreditAmount = Convert.ToDouble(Math.Round((credit - debit),4));
                            }
                            else if (debit == credit)
                            {
                                obj.DebitAmount = 0;
                                obj.CreditAmount = 0;
                            }

                            finalTrialBalanceList.Add(obj);
                        }

                        var noTransactionAccounts = accountFourthLevel.Except(accountGroup.Select(x => (x.Key)));

                        var allAccountDetails = await _dbContext.ChartOfAccountNew.Where(x => x.IsDeleted == false).ToListAsync();

                        foreach (var detail in noTransactionAccounts)
                        {
                            LedgerModel obj = new LedgerModel();
                            var noTransactionAccount = allAccountDetails.FirstOrDefault(x => x.ChartOfAccountNewId == detail);

                            obj.ChartOfAccountNewId = noTransactionAccount.ChartOfAccountNewId;
                            obj.AccountName = noTransactionAccount.AccountName;
                            obj.ChartAccountName = noTransactionAccount.AccountName;
                            obj.Description = "";
                            obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == model.CurrencyId)?.CurrencyName;
                            obj.TransactionDate = null;
                            obj.DebitAmount = 0;
                            obj.CreditAmount = 0;
                            obj.ChartOfAccountNewCode = noTransactionAccount.ChartOfAccountNewCode;

                            finalTrialBalanceList.Add(obj);
                        }
                    }
                    else
                    {
                        var accountFourthLevelNotNull = accountFourthLevel.ConvertAll(x => x);

                        List<LedgerModel> trialBalanceList = new List<LedgerModel>();
                        finalTrialBalanceList = new List<LedgerModel>();

                        var spTrialbalanceReport = await _dbContext.LoadStoredProc("get_trialbalance_report")
                                                                    .WithSqlParam("currency", model.CurrencyId)
                                                                    .WithSqlParam("recordtype", model.RecordType)
                                                                    .WithSqlParam("fromdate", model.fromdate.ToString())
                                                                    .WithSqlParam("todate", model.todate.ToString())
                                                                    .WithSqlParam("officelist", model.OfficesList)
                                                                    .WithSqlParam("accountslist", model.accountLists)
                                                                    .ExecuteStoredProc<SP_TrialBalanceModel>();

                        trialBalanceList = spTrialbalanceReport.Select(x => new LedgerModel
                        {
                            ChartOfAccountNewId = x.ChartOfAccountNewId,
                            AccountName = x.AccountName,
                            ChartAccountName = x.AccountName,
                            Description = x.Description,
                            CurrencyName = x.CurrencyName,
                            TransactionDate = x.TransactionDate,
                            ChartOfAccountNewCode = x.ChartOfAccountNewCode,
                            CreditAmount = x.CreditAmount,
                            DebitAmount = x.DebitAmount
                        }).ToList();

                        var accountGroup = trialBalanceList.GroupBy(x => x.ChartOfAccountNewId);

                        var noTransactionAccounts = accountFourthLevelNotNull.Except(accountGroup.Select(x => (x.Key)));

                        foreach (var item in accountGroup)
                        {
                            LedgerModel obj = new LedgerModel();

                            obj = item.FirstOrDefault();

                            var debit = Math.Round((decimal)item.Sum(x => x.DebitAmount), 4);
                            var credit = Math.Round((decimal)item.Sum(x => x.CreditAmount),4);

                            if (debit > credit)
                            {
                                obj.DebitAmount = Convert.ToDouble(Math.Round((debit - credit),4));
                                obj.CreditAmount = 0;
                            }
                            else if (debit < credit)
                            {
                                obj.DebitAmount = 0;
                                obj.CreditAmount = Convert.ToDouble(Math.Round((credit - debit), 4));
                            }
                            else if (debit == credit)
                            {
                                obj.DebitAmount = 0;
                                obj.CreditAmount = 0;
                            }

                            finalTrialBalanceList.Add(obj);

                        }

                        var allAccountDetails = _dbContext.ChartOfAccountNew.Where(x => x.IsDeleted == false);

                        foreach (var detail in noTransactionAccounts)
                        {
                            LedgerModel obj = new LedgerModel();
                            var noTransactionAccount = allAccountDetails.FirstOrDefault(x => x.ChartOfAccountNewId == detail);

                            obj.ChartOfAccountNewId = noTransactionAccount.ChartOfAccountNewId;
                            obj.AccountName = noTransactionAccount.AccountName;
                            obj.ChartAccountName = noTransactionAccount.AccountName;
                            obj.Description = "";
                            obj.CurrencyName = allCurrencies.FirstOrDefault(x => x.CurrencyId == model.CurrencyId)?.CurrencyName;
                            obj.TransactionDate = null;
                            obj.DebitAmount = 0;
                            obj.CreditAmount = 0;
                            obj.ChartOfAccountNewCode = noTransactionAccount.ChartOfAccountNewCode;

                            finalTrialBalanceList.Add(obj);
                        }
                    }
                }
                foreach(var item in finalTrialBalanceList)
                {   
                    debitSumForReport+=item.DebitAmount;
                    creditSumForReport+=item.CreditAmount;
                    mainList.Add(new TrialBalanceMainPdfList(){
                        LineDescription=item.ChartOfAccountNewCode+ '-' +item.AccountName,
                        DebitAmount=item.DebitAmount,
                        CreditAmount=item.CreditAmount
                    });
                }
                TrialBalancePdfModel pdfreport=new TrialBalancePdfModel(){
                    Logo=_env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath,
                    reportList=mainList,
                    TotalDebit=debitSumForReport,
                    TotalCredit=creditSumForReport
                };
                return await _pdfExportService.ExportToPdf(pdfreport, "Pages/PdfTemplates/TrialBalanceReport.cshtml"); 
           }
           catch(Exception ex)
           {
               throw new Exception(ex.Message);
           }
        }
       
    }
}