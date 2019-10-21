using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllLedgerDetailsQueryHandler : IRequestHandler<GetAllLedgerDetailsQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetAllLedgerDetailsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllLedgerDetailsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            #region "new"

            try
            {
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

                            response.data.AccountOpendingAndClosingBL = new AccountOpendingAndClosingBL
                            {
                                OpeningBalance = Math.Round(Convert.ToDouble(openingLedgerList.Sum(x => x.DebitAmount) - openingLedgerList.Sum(x => x.CreditAmount))),
                                //ClosingBalance = opening + closing
                                ClosingBalance = Math.Round(Convert.ToDouble(openingLedgerList.Sum(x => x.DebitAmount) - openingLedgerList.Sum(x => x.CreditAmount) + (closingLedgerList.Sum(x => x.DebitAmount) - closingLedgerList.Sum(x => x.CreditAmount))))

                            };
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

                            response.data.AccountOpendingAndClosingBL = new AccountOpendingAndClosingBL
                            {
                                OpeningBalance = Math.Round(Convert.ToDouble(openingLedgerList.Sum(x => x.DebitAmount) - openingLedgerList.Sum(x => x.CreditAmount))),
                                //ClosingBalance = opening + closing
                                ClosingBalance = Math.Round(Convert.ToDouble(openingLedgerList.Sum(x => x.DebitAmount) - openingLedgerList.Sum(x => x.CreditAmount) + (closingLedgerList.Sum(x => x.DebitAmount) - closingLedgerList.Sum(x => x.CreditAmount))))

                                //ClosingBalance = debitSum - creditSum + lst.Sum(x => x.TotalDebits) - lst.Sum(x => x.TotalCredits)
                            };
                        }
                    }
                }

                #region "report data"

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

                #endregion

                response.data.LedgerList = closingLedgerList;
                response.data.ledgerReportFinal = ledgerReportFinal;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            #endregion

            return response;
        }
    }
}
