using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Helpers;
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
    public class GetItemDetailByPurchaseIdQueryHandler : IRequestHandler<GetItemDetailByPurchaseIdQuery, ItemDetailModel>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetItemDetailByPurchaseIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ItemDetailModel> Handle(GetItemDetailByPurchaseIdQuery request, CancellationToken cancellationToken)
        {
            ItemDetailModel model = new ItemDetailModel();

            try
            {
                model = await _dbContext.StoreItemPurchases
                                        .Include(x => x.StoreInventoryItem)
                                        .ThenInclude(x=> x.Inventory)
                                        .Where(x => !x.IsDeleted &&
                                                x.PurchaseId == request.PurchaseId)
                                        .Select(x => new ItemDetailModel
                                            {
                                                ItemId = x.InventoryItem,
                                                InventoryTypeId = x.StoreInventoryItem.Inventory.AssetType,
                                                InventoryId = x.StoreInventoryItem.ItemInventory,
                                                ItemGroupId = x.StoreInventoryItem.ItemGroupId.Value,
                                                PurchaseId = x.PurchaseId
                                            }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return model;
        }
    }
}