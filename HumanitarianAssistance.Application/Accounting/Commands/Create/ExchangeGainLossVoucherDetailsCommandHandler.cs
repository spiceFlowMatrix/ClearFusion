using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Accounting.Commands.Common;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Create
{
    public class ExchangeGainLossVoucherDetailsCommandHandler : IRequestHandler<ExchangeGainLossVoucherDetailsCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAccountingServices _iAccountingServices;

        public ExchangeGainLossVoucherDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IAccountingServices iAccountingServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _iAccountingServices = iAccountingServices;
        }

        public async Task<ApiResponse> Handle(ExchangeGainLossVoucherDetailsCommand model, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                #region "Generate Voucher"
                AddVoucherDetailCommand voucherModel = new AddVoucherDetailCommand
                {
                    VoucherNo = model.VoucherNo,
                    CurrencyId = model.CurrencyId,
                    Description = model.Description,
                    JournalCode = model.JournalId,
                    VoucherTypeId = model.VoucherType,
                    OfficeId = model.OfficeId,
                    ProjectId = model.ProjectId,
                    BudgetLineId = model.BudgetLineId,
                    IsExchangeGainLossVoucher = true,
                    TimezoneOffset = model.TimezoneOffset,
                };

                var responseVoucher = await _iAccountingServices.AddVoucherDetail(voucherModel);

                #endregion

                #region "Generate Transaction"

                if (responseVoucher != null)
                {
                    List<VoucherTransactionsModel> transactions = new List<VoucherTransactionsModel>();

                    // Credit
                    transactions.Add(new VoucherTransactionsModel
                    {
                        TransactionId = 0,
                        VoucherNo = responseVoucher.VoucherNo,
                        AccountNo = model.CreditAccount,
                        Debit = 0,
                        Credit = Math.Abs(model.Amount),
                        Description = "Gain-Loss-Voucher-Credit",
                        IsDeleted = false
                    });

                    // Debit
                    transactions.Add(new VoucherTransactionsModel
                    {
                        TransactionId = 0,
                        VoucherNo = responseVoucher.VoucherNo,
                        AccountNo = model.DebitAccount,
                        Debit = Math.Abs(model.Amount),
                        Credit = 0,
                        Description = "Gain-Loss-Voucher-Debit",
                        IsDeleted = false
                    });

                    AddEditTransactionListCommand transactionVoucherDetail = new AddEditTransactionListCommand
                    {
                        VoucherNo = responseVoucher.VoucherNo,
                        VoucherTransactions = transactions
                    };

                    bool isTransactionSaved = _iAccountingServices.AddEditTransactionList(transactionVoucherDetail);

                    if (isTransactionSaved)
                    {
                        string journalName = _dbContext.VoucherDetail.FirstOrDefault(x => x.JournalCode == responseVoucher.JournalCode)?.JournalDetails.JournalName;

                        response.data.GainLossVoucherDetail = new GainLossVoucherListModel
                        {
                            VoucherId = responseVoucher.VoucherNo,
                            JournalName = string.IsNullOrEmpty(journalName) ? journalName : "",
                            VoucherName = responseVoucher.ReferenceNo,
                            VoucherDate = responseVoucher.VoucherDate
                        };
                    }
                    else
                    {
                        throw new Exception(StaticResource.TransactionsNotSaved);
                    }

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.VoucherNotSaved;
                }

                #endregion
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