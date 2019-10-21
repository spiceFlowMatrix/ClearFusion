using AutoMapper;
using HumanitarianAssistance.Application.Accounting.Commands.Common;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.CommonServices;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Commands.Common
{
    public class UnverifyPurchaseCommandHandler : IRequestHandler<UnverifyPurchaseCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAccountingServices _iAccountingServices;

        public UnverifyPurchaseCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IAccountingServices iAccountingServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _iAccountingServices = iAccountingServices;
        }
        public async Task<ApiResponse> Handle(UnverifyPurchaseCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request == null)
                {
                    throw new Exception("Request values are inappropriate");
                }

                var purchaseRecord = await _dbContext.StoreItemPurchases.FirstOrDefaultAsync(x => x.PurchaseId == request.PurchaseId);

                if (purchaseRecord == null)
                {
                    throw new Exception("Unable to find purchase");
                }
                    _mapper.Map(request, purchaseRecord);

                    purchaseRecord.IsDeleted = false;

                    if (request.IsPurchaseVerified.HasValue && !request.IsPurchaseVerified.Value)
                    {

                        VoucherDetail voucherDetail = _dbContext.VoucherDetail.FirstOrDefault(x => x.IsDeleted == false && x.VoucherNo == request.VerifiedPurchaseVoucher);

                        if (voucherDetail == null)
                        {
                            throw new Exception(" Voucher Not Found on Purchase");
                        }
                        List<VoucherTransactions> voucherTransactionsList = await _dbContext.VoucherTransactions.Where(x => x.IsDeleted == false && x.VoucherNo == request.VerifiedPurchaseVoucher).ToListAsync();

                        var creditTransaction = voucherTransactionsList.FirstOrDefault(x => x.Debit == 0);
                        var debitTransaction = voucherTransactionsList.FirstOrDefault(x => x.Credit == 0);

                        List<VoucherTransactionsModel> transactions = new List<VoucherTransactionsModel>();

                        // Credit
                        transactions.Add(new VoucherTransactionsModel
                        {
                            TransactionId = 0,
                            VoucherNo = voucherDetail.VoucherNo,
                            AccountNo = debitTransaction.ChartOfAccountNewId,
                            Debit = 0,
                            Credit = debitTransaction.Debit,
                            Description = StaticResource.PurchaseVoucherCreated,
                            IsDeleted = false
                        });

                        // Debit
                        transactions.Add(new VoucherTransactionsModel
                        {
                            TransactionId = 0,
                            VoucherNo = voucherDetail.VoucherNo,
                            AccountNo = creditTransaction.ChartOfAccountNewId,
                            Debit = creditTransaction.Credit,
                            Credit = 0,
                            Description = StaticResource.PurchaseVoucherCreated,
                            IsDeleted = false
                        });

                        AddEditTransactionListCommand transactionVoucherDetail = new AddEditTransactionListCommand
                        {
                            VoucherNo = voucherDetail.VoucherNo,
                            VoucherTransactions = transactions
                        };
                        // StoreServices storeObj = new StoreServices(_dbContext);
                        var responseTransaction = _iAccountingServices.AddEditTransactionList(transactionVoucherDetail);

                        if (!responseTransaction)
                        {
                            throw new Exception("Unable to unverify Purchase");
                        }

                        await _dbContext.SaveChangesAsync();
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
                return response;
            }
            return response;
        }
    }
}
