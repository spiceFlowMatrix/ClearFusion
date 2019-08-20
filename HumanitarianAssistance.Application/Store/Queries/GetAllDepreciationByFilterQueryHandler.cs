using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllDepreciationByFilterQueryHandler : IRequestHandler<GetAllDepreciationByFilterQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAllDepreciationByFilterQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllDepreciationByFilterQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (request.CurrentDate != null)
                {
                    List<DepreciationReportModel> depreciationList = new List<DepreciationReportModel>();

                    List<StoreItemPurchase> storeItemPurchased = new List<StoreItemPurchase>();

                    if (request.StoreId != null && request.InventoryId != null && request.ItemId != null)
                    {
                        storeItemPurchased = await _dbContext.StoreItemPurchases.Include(x => x.StoreInventoryItem).ThenInclude(x => x.StoreItemGroup).ThenInclude(x => x.StoreInventory).Where(x => x.IsDeleted == false && x.ApplyDepreciation == true && x.InventoryItem == request.ItemId && x.StoreInventoryItem.StoreItemGroup.StoreInventory.AssetType == request.StoreId).ToListAsync();
                    }
                    else if (request.StoreId != null && request.InventoryId != null && request.ItemId == null && request.ItemGroupId != 0)
                    {
                        storeItemPurchased = await _dbContext.StoreItemPurchases.Include(x => x.StoreInventoryItem).ThenInclude(x => x.StoreItemGroup).ThenInclude(x => x.StoreInventory).Where(x => x.IsDeleted == false && x.ApplyDepreciation == true && x.StoreInventoryItem.StoreItemGroup.StoreInventory.AssetType == request.StoreId && x.StoreInventoryItem.Inventory.InventoryId == request.InventoryId && x.StoreInventoryItem.ItemGroupId == request.ItemGroupId).ToListAsync();
                    }
                    else if (request.StoreId != null && request.InventoryId != null && request.ItemId == null && request.ItemGroupId == 0)
                    {
                        storeItemPurchased = await _dbContext.StoreItemPurchases.Include(x => x.StoreInventoryItem).ThenInclude(x => x.StoreItemGroup).ThenInclude(x => x.StoreInventory).Where(x => x.IsDeleted == false && x.ApplyDepreciation == true && x.StoreInventoryItem.StoreItemGroup.StoreInventory.AssetType == request.StoreId && x.StoreInventoryItem.ItemInventory == request.InventoryId).ToListAsync();
                    }
                    else if (request.StoreId != null && request.InventoryId == null && request.ItemId == null && request.ItemGroupId != 0)
                    {
                        storeItemPurchased = await _dbContext.StoreItemPurchases.Include(x => x.StoreInventoryItem).ThenInclude(x => x.Inventory).Where(x => x.IsDeleted == false && x.ApplyDepreciation == true && x.StoreInventoryItem.StoreItemGroup.StoreInventory.AssetType == request.StoreId && x.StoreInventoryItem.ItemGroupId == request.ItemGroupId).ToListAsync();
                    }
                    else if (request.StoreId != null && request.InventoryId == null && request.ItemId == null && request.ItemGroupId == 0)
                    {
                        storeItemPurchased = await _dbContext.StoreItemPurchases.Include(x => x.StoreInventoryItem).ThenInclude(x => x.Inventory).Where(x => x.IsDeleted == false && x.ApplyDepreciation == true && x.StoreInventoryItem.StoreItemGroup.StoreInventory.AssetType == request.StoreId).ToListAsync();
                    }
                    else if (request.StoreId == null && request.InventoryId == null && request.ItemId == null)
                    {
                        storeItemPurchased = await _dbContext.StoreItemPurchases.Include(x => x.StoreInventoryItem).ThenInclude(x => x.Inventory).Where(x => x.IsDeleted == false && x.ApplyDepreciation == true).ToListAsync();
                    }

                    foreach (var item in storeItemPurchased)
                    {
                        ExchangeRateDetail dollarExchangeRate = _dbContext.ExchangeRateDetail.OrderByDescending(x => x.Date)
                                                        .FirstOrDefault(x => x.IsDeleted == false && x.FromCurrency == item.Currency
                                                        && x.ToCurrency == (int)Currency.USD && x.Date.Date <= request.CurrentDate.Date);

                        if (dollarExchangeRate == null)
                        {
                            throw new Exception("Exchange Rate not defined!!");
                        }

                        DepreciationReportModel obj = new DepreciationReportModel();

                        obj.ItemName = item.StoreInventoryItem.ItemName;
                        obj.PurchaseId = item.PurchaseId;
                        obj.PurchaseDate = item.PurchaseDate;
                        obj.HoursSincePurchase = Math.Round(Math.Abs(request.CurrentDate.Date.Subtract(item.PurchaseDate).TotalHours), 4);
                        obj.DepreciationRate = item.DepreciationRate;
                        obj.DepreciationAmount = Math.Round(((obj.HoursSincePurchase * item.DepreciationRate * item.UnitCost) / 100) * (double)dollarExchangeRate.Rate, 4);
                        obj.CurrentValue = Math.Round((item.UnitCost - obj.DepreciationAmount) * (double)dollarExchangeRate.Rate, 4);
                        obj.PurchasedCost = Math.Round(item.UnitCost * (double)dollarExchangeRate.Rate, 4);
                        depreciationList.Add(obj);
                    }

                    response.data.DepreciationReportList = depreciationList.ToList();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Please Select Date";
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
