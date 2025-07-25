using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetTrialBalanceReportQueryHandler: IRequestHandler<GetTrialBalanceReportQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetTrialBalanceReportQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext=dbContext;
        }

       public async Task<ApiResponse> Handle(GetTrialBalanceReportQuery model, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                DateTime defaultdate = new DateTime(DateTime.UtcNow.Year, 1, 1);
                if (model.fromdate == null && model.todate == null)
                {
                    model.fromdate = new DateTime(DateTime.UtcNow.Year, 1, 1);
                    model.todate = DateTime.UtcNow;
                }


                if (model != null)
                {
                    List<LedgerModel> finalTrialBalanceList = new List<LedgerModel>();

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

                    response.data.TrialBalanceList = finalTrialBalanceList;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.data.TrialBalanceList = null;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "No data Found";
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        } 
    }
}