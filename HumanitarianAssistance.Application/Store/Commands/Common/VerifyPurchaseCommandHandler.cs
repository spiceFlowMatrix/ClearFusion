using AutoMapper;
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
        private IStoreServices _iStoreServices;

        public VerifyPurchaseCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IStoreServices iStoreServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _iStoreServices = iStoreServices;
        }
        public async Task<ApiResponse> Handle(VerifyPurchaseCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    var purchaseRecord = await _dbContext.StoreItemPurchases.FirstOrDefaultAsync(x => x.PurchaseId == request.PurchaseId);
                    if (purchaseRecord != null)
                    {
                        _mapper.Map(request, purchaseRecord);

                        if (!string.IsNullOrEmpty(request.ImageFileName))
                        {
                            if (request.ImageFileName.Contains(","))
                            {
                                string[] str = request.ImageFileName.Split(",");
                                byte[] filepath = Convert.FromBase64String(str[1]);
                                string ex = str[0].Split("/")[1].Split(";")[0];
                                string guidname = Guid.NewGuid().ToString();
                                string filename = guidname + "." + ex;
                                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                                File.WriteAllBytes(@"Documents/" + filename, filepath);

                                purchaseRecord.ImageFileName = guidname;
                                purchaseRecord.ImageFileType = "." + ex;
                            }
                        }

                        if (request.InvoiceFileName != null && request.InvoiceFileName != "")
                        {
                            if (request.InvoiceFileName.Contains(","))
                            {
                                string[] str = request.InvoiceFileName.Split(",");
                                byte[] filepath = Convert.FromBase64String(str[1]);
                                string ex = str[0].Split("/")[1].Split(";")[0];
                                string guidname = Guid.NewGuid().ToString();
                                string filename = guidname + "." + ex;
                                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                                File.WriteAllBytes(@"Documents/" + filename, filepath);

                                purchaseRecord.InvoiceFileName = guidname;
                                purchaseRecord.InvoiceFileType = "." + ex;
                            }
                        }

                        purchaseRecord.IsDeleted = false;


                        //List<ExchangeRate> exchangeRate = new List<ExchangeRate>();

                        if (request.IsPurchaseVerified.HasValue && request.IsPurchaseVerified.Value)
                        {
                            var financialYearDetails = _dbContext.FinancialYearDetail.FirstOrDefault(x => x.IsDeleted == false && x.StartDate.Date.Year == DateTime.Now.Year);
                            var inventory = _dbContext.InventoryItems.Include(x => x.Inventory).FirstOrDefault(x => x.ItemId == request.InventoryItem);
                            var paymentTypes = _dbContext.PaymentTypes.FirstOrDefault(x => x.PaymentId == request.PaymentTypeId);

                            #region "Generate Voucher"
                            VoucherDetailModel voucherModel = new VoucherDetailModel
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

                            var responseVoucher = await _iStoreServices.AddVoucherNewDetail(voucherModel);
                            #endregion

                            if (responseVoucher.StatusCode == 200)
                            {
                                purchaseRecord.VerifiedPurchaseVoucher = responseVoucher.data.VoucherDetailEntity.VoucherNo;
                                await _dbContext.SaveChangesAsync();

                                List<VoucherTransactionsModel> transactions = new List<VoucherTransactionsModel>();

                                // Credit
                                transactions.Add(new VoucherTransactionsModel
                                {
                                    TransactionId = 0,
                                    VoucherNo = responseVoucher.data.VoucherDetailEntity.VoucherNo,
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
                                    VoucherNo = responseVoucher.data.VoucherDetailEntity.VoucherNo,
                                    AccountNo = inventory.Inventory.InventoryDebitAccount,
                                    Debit = request.UnitCost * request.Quantity,
                                    Credit = 0,
                                    Description = StaticResource.PurchaseVoucherCreated,
                                    IsDeleted = false
                                });

                                AddEditTransactionModel transactionVoucherDetail = new AddEditTransactionModel
                                {
                                    VoucherNo = responseVoucher.data.VoucherDetailEntity.VoucherNo,
                                    VoucherTransactions = transactions
                                };
                                var responseTransaction = _iStoreServices.AddEditTransactionList(transactionVoucherDetail, request.CreatedById);

                                if (responseTransaction.StatusCode == 200)
                                {
                                    response.StatusCode = StaticResource.successStatusCode;
                                    response.Message = StaticResource.SuccessText;
                                }
                                else
                                {
                                    throw new Exception(responseTransaction.Message);
                                }
                            }
                            else
                            {
                                response.StatusCode = StaticResource.failStatusCode;
                                response.Message = responseVoucher.Message;
                            }
                        }
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = "Record cannot be updated";
                        return response;
                    }
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "request values are inappropriate";
                    return response;
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
