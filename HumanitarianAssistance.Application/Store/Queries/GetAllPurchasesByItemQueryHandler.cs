using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.FileManagement.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllPurchasesByItemQueryHandler : IRequestHandler<GetAllPurchasesByItemQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IFileManagementService _fileManagement;

        public GetAllPurchasesByItemQueryHandler(HumanitarianAssistanceDbContext dbContext, IFileManagementService fileManagement)
        {
            _dbContext = dbContext;
            _fileManagement = fileManagement;
        }

        public async Task<ApiResponse> Handle(GetAllPurchasesByItemQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var purchases = await _dbContext.StoreItemPurchases.Include(x => x.PurchaseOrders)
                                                                            .Include(x => x.StoreInventoryItem)
                                                                            .Where(x => x.InventoryItem == request.ItemId && x.IsDeleted == false).ToListAsync();


                var purchasesModel = purchases.Select(v => new StoreItemPurchaseViewModel
                {
                    PurchaseId = v.PurchaseId,
                    SerialNo = v.SerialNo,
                    Currency = v.Currency,
                    UnitCost = v.UnitCost,
                    Quantity = v.Quantity,
                    TotalCost = Math.Round(v.UnitCost * v.Quantity, 4) ,
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
                   // VoucherId = v.VoucherId,
                   // VoucherDate = v.VoucherDate,
                    AssetTypeId = v.AssetTypeId,
                    InvoiceNo = v.InvoiceNo,
                    InvoiceDate = v.InvoiceDate,
                    Status = v.Status,
                    ReceiptTypeId = v.ReceiptTypeId,
                    ReceivedFromLocation = v.ReceivedFromLocation,
                    ProjectId = v.ProjectId,
                    BudgetLineId = v.BudgetLineId,
                    PaymentTypeId = v.PaymentTypeId,
                   // IsPurchaseVerified = v.IsPurchaseVerified,
                   // VerifiedPurchaseVoucher = v.VerifiedPurchaseVoucher,
                    JournalCode = v.JournalCode,
                    PurchaseName= v.PurchaseName,
                    PurchaseCode= PurchaseCode.GetPurchaseCode(v.PurchaseDate, v.PurchaseName, v.PurchaseId)
                   // VerifiedPurchaseVoucherReferenceNo = v.VerifiedPurchaseVoucher != null ? _dbContext.VoucherDetail.FirstOrDefault(x => x.IsDeleted == false && x.VoucherNo == v.VerifiedPurchaseVoucher).ReferenceNo : null
                }).OrderByDescending(x=> x.PurchaseDate).ToList();

                List<ExchangeRateDetail> exchangeRate= new List<ExchangeRateDetail>();

                if(purchasesModel.Any())
                {
                    exchangeRate = await _dbContext.ExchangeRateDetail.OrderByDescending(x=> x.Date).Where(x => x.IsDeleted == false && x.Date.Date <= purchasesModel.First().PurchaseDate.Date && x.Date.Date >= purchasesModel[purchasesModel.Count-1].PurchaseDate.Date).ToListAsync();

                }

                foreach (var item in purchasesModel)
                {
                    FileModel model = new FileModel()
                    {
                        PageId = (int)FileSourceEntityTypes.StorePurchase,
                        RecordId = item.PurchaseId,
                        DocumentTypeId = (int)DocumentFileTypes.PurchaseImage
                    };

                    StoreDocumentModel documentModel = new StoreDocumentModel();

                    //get Saved Document ID and Signed URL For Purchase Image
                    documentModel = await _fileManagement.GetFilesByRecordIdAndDocumentType(model);

                    if (documentModel != null)
                    {
                        item.ImageFileName = documentModel.SignedURL;
                        item.ImageDocumentId = documentModel.DocumentFileId;
                    }

                    model.DocumentTypeId = (int)DocumentFileTypes.PurchaseInvoice;

                    documentModel = new StoreDocumentModel();

                    //get Saved Document ID and Signed URL For Purchase Invoice
                    documentModel = await _fileManagement.GetFilesByRecordIdAndDocumentType(model);

                    if (documentModel != null)
                    {
                        item.Invoice = documentModel.SignedURL;
                        item.InvoiceDocumentId = documentModel.DocumentFileId;
                    }


                   var exRate = exchangeRate.OrderByDescending(x=> x.Date).FirstOrDefault(x => x.IsDeleted == false && x.Date.Date <= item.PurchaseDate.Date && x.FromCurrency == item.Currency && x.ToCurrency == (int)Currency.USD);

                   //var exchangeRate = _dbContext.ExchangeRateDetail.OrderByDescending(x=> x.Date).FirstOrDefault(x => x.IsDeleted == false && x.Date.Date <= item.PurchaseDate.Date && x.FromCurrency == item.Currency && x.ToCurrency == (int)Currency.USD);

                    if (exRate == null)
                    {
                        throw new Exception($"Exchange Rates not defined for Date {item.PurchaseDate.Date.ToString("dd/MM/yyyy")}");
                    }

                    item.TotalCostUSD = Math.Round(item.TotalCost * (double)exRate.Rate, 4) ;
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
