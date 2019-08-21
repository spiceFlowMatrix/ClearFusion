using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetVoucherTransactionListQueryHandler : IRequestHandler<GetVoucherTransactionListQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetVoucherTransactionListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetVoucherTransactionListQuery model, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var data = await _dbContext.VoucherDetail
                                                    .Include(x => x.VoucherTransactionDetails)
                                                    .ThenInclude(y => y.ChartOfAccountDetail)
                                                    .Include(x => x.CurrencyDetail)
                                                    .FirstOrDefaultAsync(x => x.IsDeleted == false && x.VoucherNo == model.VoucherNo);

                if (data != null)
                {
                    if (data.VoucherTransactionDetails.Any())
                    {

                        if (model.RecordType == (int)RECORDTYPE.SINGLE)
                        {
                            response.data.VoucherSummaryTransactionList = data.VoucherTransactionDetails.Select(x => new VoucherSummaryTransactionModel
                            {
                                AccountCode = x.ChartOfAccountDetail.ChartOfAccountNewCode,
                                AccountName = x.ChartOfAccountDetail.AccountName,
                                CurrencyName = data.CurrencyDetail.CurrencyName,
                                TransactionDescription = x.Description,
                                Amount = x.Debit == 0 ? x.Credit : x.Debit,
                                TransactionType = x.Debit == 0 ? "Credit" : "Debit"
                            }).ToList();
                        }
                        else //consolidated
                        {
                            response.data.VoucherSummaryTransactionList = new List<VoucherSummaryTransactionModel>();

                            ExchangeRateDetail exchangeRateDetail = exchangeRateDetail = await _dbContext.ExchangeRateDetail
                                                                                  .OrderByDescending(x => x.Date)
                                                                                  .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                   x.Date <= data.VoucherDate.Date && x.FromCurrency == data.CurrencyId &&
                                                                                   x.ToCurrency == model.CurrencyId);

                            foreach (var item in data.VoucherTransactionDetails)
                            {
                                VoucherSummaryTransactionModel voucherSummaryTransactionModel = new VoucherSummaryTransactionModel();

                                if (item.CurrencyId != model.CurrencyId)
                                {
                                    if (exchangeRateDetail == null)
                                    {
                                        throw new Exception("Exchange Rate Not Defined");
                                    }

                                    if (item.Debit == 0)
                                    {
                                        voucherSummaryTransactionModel.Amount = Math.Round((double)(item.Credit * (double)exchangeRateDetail.Rate), 2);
                                        voucherSummaryTransactionModel.TransactionType = "Credit";
                                    }
                                    else
                                    {
                                        voucherSummaryTransactionModel.Amount = Math.Round((double)(item.Debit * (double)exchangeRateDetail.Rate), 2);
                                        voucherSummaryTransactionModel.TransactionType = "Debit";
                                    }
                                }
                                else
                                {
                                    if (item.Debit == 0)
                                    {
                                        voucherSummaryTransactionModel.Amount = item.Credit;
                                        voucherSummaryTransactionModel.TransactionType = "Credit";
                                    }
                                    else
                                    {
                                        voucherSummaryTransactionModel.Amount = item.Debit;
                                        voucherSummaryTransactionModel.TransactionType = "Debit";
                                    }
                                }

                                voucherSummaryTransactionModel.AccountCode = item.ChartOfAccountDetail.ChartOfAccountNewCode;
                                voucherSummaryTransactionModel.AccountName = item.ChartOfAccountDetail.AccountName;
                                voucherSummaryTransactionModel.CurrencyName = data.CurrencyDetail.CurrencyName;
                                voucherSummaryTransactionModel.TransactionDescription = item.Description;

                                response.data.VoucherSummaryTransactionList.Add(voucherSummaryTransactionModel);
                            }
                        }
                    }
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception exception)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + exception.Message;
            }

            return response;
        }
    }
}