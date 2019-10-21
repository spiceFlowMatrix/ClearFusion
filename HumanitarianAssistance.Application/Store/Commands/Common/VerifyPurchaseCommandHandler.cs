using AutoMapper;
using HumanitarianAssistance.Application.Accounting.Commands.Common;
using HumanitarianAssistance.Application.Accounting.Commands.Create;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.CommonServices;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
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
    public class VerifyPurchaseCommandHandler : IRequestHandler<VerifyPurchaseCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        private IAccountingServices _iAccountingServices;

        public VerifyPurchaseCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IAccountingServices iAccountingServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _iAccountingServices = iAccountingServices;
        }
        public async Task<ApiResponse> Handle(VerifyPurchaseCommand request, CancellationToken cancellationToken)
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
                    throw new Exception("Record not Found");
                }

                _mapper.Map(request, purchaseRecord);

                purchaseRecord.IsDeleted = false;

                if (request.IsPurchaseVerified.HasValue && request.IsPurchaseVerified.Value)
                {
                    var financialYearDetails = _dbContext.FinancialYearDetail.FirstOrDefault(x => x.IsDeleted == false && x.StartDate.Date.Year == DateTime.Now.Year);
                    var inventory = _dbContext.InventoryItems.Include(x => x.Inventory).FirstOrDefault(x => x.ItemId == request.InventoryItem);
                    var paymentTypes = _dbContext.PaymentTypes.FirstOrDefault(x => x.PaymentId == request.PaymentTypeId);

                    #region "Generate Voucher"
                    AddVoucherDetailCommand voucherModel = new AddVoucherDetailCommand
                    {
                        CurrencyId = request.Currency,
                        Description = StaticResource.PurchaseVoucherCreated,
                        JournalCode = request.JournalCode,
                        VoucherTypeId = (int)VoucherTypes.Journal,
                        OfficeId = request.OfficeId,
                        ProjectId = request.ProjectId,
                        BudgetLineId = request.BudgetLineId,
                        IsExchangeGainLossVoucher = false,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                        FinancialYearId = financialYearDetails.FinancialYearId,
                        VoucherDate = DateTime.UtcNow,
                        TimezoneOffset = request.TimezoneOffset
                    };

                    var responseVoucher = await _iAccountingServices.AddVoucherDetail(voucherModel);
                    #endregion

                    if (responseVoucher.VoucherNo == 0)
                    {
                        throw new Exception("Error Creating Voucher");
                    }

                    purchaseRecord.VerifiedPurchaseVoucher = responseVoucher.VoucherNo;
                    await _dbContext.SaveChangesAsync();

                    List<VoucherTransactionsModel> transactions = new List<VoucherTransactionsModel>();

                    // Credit
                    transactions.Add(new VoucherTransactionsModel
                    {
                        TransactionId = 0,
                        VoucherNo = responseVoucher.VoucherNo,
                        AccountNo = paymentTypes.ChartOfAccountNewId,
                        Debit = 0,
                        Credit = request.UnitCost * request.Quantity,
                        Description = StaticResource.PurchaseVoucherCreated,
                        IsDeleted = false
                    });

                    // Debit
                    transactions.Add(new VoucherTransactionsModel
                    {
                        TransactionId = 0,
                        VoucherNo = responseVoucher.VoucherNo,
                        AccountNo = inventory.Inventory.InventoryDebitAccount,
                        Debit = request.UnitCost * request.Quantity,
                        Credit = 0,
                        Description = StaticResource.PurchaseVoucherCreated,
                        IsDeleted = false
                    });

                    AddEditTransactionListCommand transactionVoucherDetail = new AddEditTransactionListCommand
                    {
                        VoucherNo = responseVoucher.VoucherNo,
                        VoucherTransactions = transactions
                    };
                    var responseTransaction = _iAccountingServices.AddEditTransactionList(transactionVoucherDetail);

                    if (responseTransaction)
                    {
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = StaticResource.SuccessText;
                    }
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
