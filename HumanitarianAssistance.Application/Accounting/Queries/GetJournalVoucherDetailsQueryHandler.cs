using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetJournalVoucherDetailsQueryHandler : IRequestHandler<GetJournalVoucherDetailsQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetJournalVoucherDetailsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetJournalVoucherDetailsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                int voucherDetailsCount = 0;
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
                        AccountName = x.AccountName
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


                    response.data.JournalVoucherViewList = listJournalView;
                    response.data.JournalReportList = journalReportList; //Report
                    response.data.TotalCount = voucherDetailsCount;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";


                }
                else
                {
                    response.data.JournalVoucherViewList = null;
                    response.data.TotalCount = voucherDetailsCount;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";

                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}
