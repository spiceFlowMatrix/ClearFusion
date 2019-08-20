using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllPurchasesByItemQueryHandler : IRequestHandler<GetAllPurchasesByItemQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetAllPurchasesByItemQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllPurchasesByItemQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var purchases = await _dbContext.StoreItemPurchases.Include(x => x.PurchaseOrders)
                                                                            .Include(x => x.StoreInventoryItem)
                                                                            .Where(x => x.InventoryItem == request.itemId && x.IsDeleted == false).ToListAsync();


                var purchasesModel = purchases.Select(v => new StoreItemPurchaseViewModel
                {
                    PurchaseId = v.PurchaseId,
                    SerialNo = v.SerialNo,
                    Currency = v.Currency,
                    UnitCost = v.UnitCost,
                    Quantity = v.Quantity,
                    TotalCost = v.UnitCost * v.Quantity,
                    UnitType = v.UnitType,
                    CurrentQuantity = v.Quantity - (v.PurchaseOrders != null ? v.PurchaseOrders.Sum(q => q.IssuedQuantity) : 0),
                    ImageFileName = v.ImageFileName + v.ImageFileType,
                    Invoice = v.InvoiceFileName + v.InvoiceFileType,
                    PurchaseDate = v.PurchaseDate,
                    DeliveryDate = v.DeliveryDate,
                    ApplyDepreciation = v.ApplyDepreciation,
                    DepreciationRate = v.DepreciationRate,
                    PurchasedById = v.PurchasedById,
                    InventoryItem = v.InventoryItem,
                    //Newly added fields
                    VoucherId = v.VoucherId,
                    VoucherDate = v.VoucherDate,
                    AssetTypeId = v.AssetTypeId,
                    InvoiceNo = v.InvoiceNo,
                    InvoiceDate = v.InvoiceDate,
                    Status = v.Status,
                    ReceiptTypeId = v.ReceiptTypeId,
                    ReceivedFromLocation = v.ReceivedFromLocation,
                    ProjectId = v.ProjectId,
                    BudgetLineId = v.BudgetLineId,
                    PaymentTypeId = v.PaymentTypeId,
                    IsPurchaseVerified = v.IsPurchaseVerified,
                    VerifiedPurchaseVoucher = v.VerifiedPurchaseVoucher,
                    JournalCode = v.JournalCode,
                    VerifiedPurchaseVoucherReferenceNo = v.VerifiedPurchaseVoucher != null ? _dbContext.VoucherDetail.FirstOrDefault(x => x.IsDeleted == false && x.VoucherNo == v.VerifiedPurchaseVoucher).ReferenceNo : null
                }).ToList();

                foreach (var item in purchasesModel)
                {
                    var exchangeRate = _dbContext.ExchangeRateDetail.OrderByDescending(x=> x.Date).FirstOrDefault(x => x.IsDeleted == false && x.Date.Date <= item.PurchaseDate.Date && x.FromCurrency == item.Currency && x.ToCurrency == (int)Currency.USD);

                    if (exchangeRate == null)
                    {
                        throw new Exception($"Exchange Rates not defined for Date {item.PurchaseDate.Date.ToString("dd/MM/yyyy")}");
                    }

                    item.TotalCostUSD = item.TotalCost * (double)exchangeRate.Rate;
                }

                response.data.StoreItemsPurchaseViewList = purchasesModel;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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
