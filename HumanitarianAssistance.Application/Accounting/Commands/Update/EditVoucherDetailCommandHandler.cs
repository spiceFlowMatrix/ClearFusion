using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Commands.Update
{

    public class EditVoucherDetailCommandHandler : IRequestHandler<EditVoucherDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public EditVoucherDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(EditVoucherDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                request.TimezoneOffset = request.TimezoneOffset > 0 ? request.TimezoneOffset * -1 : Math.Abs(request.TimezoneOffset.Value);

                DateTime filterVoucherDate = request.VoucherDate.AddMinutes(request.TimezoneOffset.Value);

                Task<List<CurrencyDetails>> currencyListTask = _dbContext.CurrencyDetails.Where(x => x.IsDeleted == false).ToListAsync();
                Task<List<ExchangeRateDetail>> exchangeRatePresentTask = _dbContext.ExchangeRateDetail.Where(x => x.Date.Date == request.VoucherDate.Date && x.IsDeleted == false).ToListAsync();
                Task<VoucherDetail> voucherDetailTask = _dbContext.VoucherDetail.FirstOrDefaultAsync(c => c.VoucherNo == request.VoucherNo);
                Task<OfficeDetail> officeDetailTask = _dbContext.OfficeDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.OfficeId == request.OfficeId);
                List<CurrencyDetails> currencyDetailsList = await currencyListTask;

                var currencyList = currencyDetailsList.Select(x => x.CurrencyId).ToList();

                List<ExchangeRateDetail> exchangeRatePresent = await exchangeRatePresentTask;

                if (CheckExchangeRateIsPresent(currencyList, exchangeRatePresent))
                {
                    VoucherDetail voucherdetailInfo = await voucherDetailTask;

                    string currencyCode = currencyDetailsList.FirstOrDefault(x => x.CurrencyId == request.CurrencyId).CurrencyCode;

                    int voucherCount = await _dbContext.VoucherDetail.Where(x => x.VoucherDate.Month == filterVoucherDate.Month && x.OfficeId == request.OfficeId && x.VoucherDate.Year == filterVoucherDate.Year).CountAsync();

                    if (voucherdetailInfo != null)
                    {
                        OfficeDetail officeDetail = await officeDetailTask;

                        if (request.VoucherDate.Date != voucherdetailInfo.VoucherDate.Date || request.OfficeId != voucherdetailInfo.OfficeId || voucherdetailInfo.CurrencyCode != request.CurrencyCode)
                        {
                            string referenceNo = AccountingUtility.GenerateVoucherReferenceCode(filterVoucherDate, voucherCount, currencyCode, officeDetail.OfficeCode);

                            if (!string.IsNullOrEmpty(referenceNo))
                            {
                                //check if same sequence number is already present in the voucher table
                                int sameVoucherReferenceNoCount = 0;

                                do
                                {
                                    sameVoucherReferenceNoCount = await _dbContext.VoucherDetail.Where(x => x.ReferenceNo == referenceNo).CountAsync();

                                    if (sameVoucherReferenceNoCount == 0)
                                    {
                                        voucherdetailInfo.ReferenceNo = referenceNo;
                                    }
                                    else
                                    {
                                        var refNo = referenceNo.Split('-');
                                        int count = Convert.ToInt32(refNo[3]);
                                        referenceNo = AccountingUtility.GenerateVoucherReferenceCode(request.VoucherDate, count, currencyCode, officeDetail.OfficeCode);
                                    }
                                }
                                while (sameVoucherReferenceNoCount != 0);
                            }
                        }
                        else if (request.CurrencyId != voucherdetailInfo.CurrencyId)
                        {
                            if (string.IsNullOrEmpty(voucherdetailInfo.ReferenceNo))
                            {
                                var refNo = voucherdetailInfo.ReferenceNo.Split('-');
                                refNo[1] = currencyCode;
                                voucherdetailInfo.ReferenceNo = refNo[0] + "-" + refNo[1] + "-" + refNo[2] + "-" + refNo[3] + "-" + refNo[4];
                            }
                            else
                            {
                                throw new Exception("Reference No cannot be set");
                            }
                        }

                        voucherdetailInfo.CurrencyId = request.CurrencyId;
                        voucherdetailInfo.OfficeId = request.OfficeId;
                        voucherdetailInfo.VoucherDate = request.VoucherDate;
                        voucherdetailInfo.ChequeNo = request.ChequeNo;
                        voucherdetailInfo.JournalCode = request.JournalCode;
                        voucherdetailInfo.FinancialYearId = request.FinancialYearId;
                        voucherdetailInfo.VoucherTypeId = request.VoucherTypeId;
                        voucherdetailInfo.Description = request.Description;
                        voucherdetailInfo.ModifiedById = request.ModifiedById;
                        voucherdetailInfo.ModifiedDate = request.ModifiedDate;

                        _dbContext.VoucherDetail.Update(voucherdetailInfo);
                        await _dbContext.SaveChangesAsync();

                        if (await _dbContext.VoucherTransactions.AnyAsync(x => x.VoucherNo == voucherdetailInfo.VoucherNo))
                        {
                            var voucherTransactions = await _dbContext.VoucherTransactions.Where(x => x.VoucherNo == voucherdetailInfo.VoucherNo).ToListAsync();
                            foreach (var transaction in voucherTransactions)
                            {
                                transaction.TransactionDate = voucherdetailInfo.VoucherDate;
                            }

                            _dbContext.VoucherTransactions.UpdateRange(voucherTransactions);
                            await _dbContext.SaveChangesAsync();
                        }

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = StaticResource.SuccessText;
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.VoucherNotPresent;
                    }
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.ExchagneRateNotDefined;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// check if Exchange Rate is present or not
        /// </summary>
        /// <param name="currencyList"></param>
        /// <param name="exchangeRatePresedsnt"></param>
        /// <returns>false</returns>
        /// <returns>true</returns>
        public bool CheckExchangeRateIsPresent(List<int> currencyList, List<ExchangeRateDetail> exchangeRates)
        {
            var groupedDataCount = exchangeRates.GroupBy(x => new { x.FromCurrency, x.ToCurrency }).ToList().Count;
            if (groupedDataCount >= (int)Math.Pow(currencyList.Count(), 2))
            {
                return true;
            }
            return false;
        }
    }
}