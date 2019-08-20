using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetExchangeGainLossReportQueryHandler : IRequestHandler<GetExchangeGainLossReportQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IAccountBalanceServices _iAccountBalanceServices;

        public GetExchangeGainLossReportQueryHandler(HumanitarianAssistanceDbContext dbContext, IAccountBalanceServices iAccountBalanceServices)
        {
            _dbContext = dbContext;
            _iAccountBalanceServices = iAccountBalanceServices;
        }

        public async Task<ApiResponse> Handle(GetExchangeGainLossReportQuery request, CancellationToken cancellationToken)
        {
            List<ExchangeGainLossReportViewModel> exchangeGainLossReportData = new List<ExchangeGainLossReportViewModel>();

            ApiResponse response = new ApiResponse();

            if (request != null)
            {
                try
                {
                    var originalBalance = await _iAccountBalanceServices.GetAccountBalancesById(
                                                            request.AccountIdList,
                                                            request.ToCurrencyId,
                                                            request.FromDate,
                                                            request.ToDate,
                                                            request.JournalIdList,
                                                            request.OfficeIdList,
                                                            request.ProjectIdList
                                                );

                    var currentBalance = await _iAccountBalanceServices.GetAccountBalancesById(
                                                            request.AccountIdList,
                                                            request.ComparisionDate,
                                                            request.ToCurrencyId,
                                                            request.FromDate,
                                                            request.ToDate,
                                                            request.JournalIdList,
                                                            request.OfficeIdList,
                                                            request.ProjectIdList
                                               );

                    foreach (var balance in originalBalance)
                    {
                        ExchangeGainLossReportViewModel exchangeGainLossReport = new ExchangeGainLossReportViewModel();
                        var currentDateBalance = currentBalance.FirstOrDefault(x => x.AccountId == balance.AccountId);

                        exchangeGainLossReport.AccountCode = balance.AccountCode;
                        exchangeGainLossReport.AccountCodeName = balance.AccountCode + "-" + balance.AccountName;
                        exchangeGainLossReport.AccountName = balance.AccountName;
                        exchangeGainLossReport.BalanceOnCurrentDate = currentDateBalance.Balance;
                        exchangeGainLossReport.BalanceOnOriginalDate = balance.Balance;
                        exchangeGainLossReport.GainLossAmount = currentDateBalance.Balance - balance.Balance;
                        exchangeGainLossReportData.Add(exchangeGainLossReport);
                    }

                    response.data.ExchangeGainLossReportList = exchangeGainLossReportData;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                catch (Exception exception)
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = exception.Message;
                }
            }

            return response;
        }
    }
}