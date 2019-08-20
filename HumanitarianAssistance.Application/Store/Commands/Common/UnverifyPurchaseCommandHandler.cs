using AutoMapper;
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
        private readonly IStoreServices _iStoreServices;

        public UnverifyPurchaseCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IStoreServices iStoreServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _iStoreServices = iStoreServices;
        }
        public async Task<ApiResponse> Handle(UnverifyPurchaseCommand request, CancellationToken cancellationToken)
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

                        if (request.IsPurchaseVerified.HasValue && !request.IsPurchaseVerified.Value)
                        {

                            VoucherDetail voucherDetail = _dbContext.VoucherDetail.FirstOrDefault(x => x.IsDeleted == false && x.VoucherNo == request.VerifiedPurchaseVoucher);

                            if (voucherDetail != null)
                            {
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

                                AddEditTransactionModel transactionVoucherDetail = new AddEditTransactionModel
                                {
                                    VoucherNo = voucherDetail.VoucherNo,
                                    VoucherTransactions = transactions
                                };
                                // StoreServices storeObj = new StoreServices(_dbContext);
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


                                response.StatusCode = StaticResource.successStatusCode;
                                response.Message = "Success";
                            }
                            else
                            {
                                throw new Exception(" Voucher Not Found on Verified Purchase");
                            }
                        }
                        else
                        {
                            await _dbContext.SaveChangesAsync();
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
