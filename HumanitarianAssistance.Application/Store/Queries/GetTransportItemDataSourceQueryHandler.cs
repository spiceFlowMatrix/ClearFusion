using HumanitarianAssistance.Application.Store.Models;
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
    public class GetTransportItemDataSourceQueryHandler : IRequestHandler<GetTransportItemDataSourceQuery, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetTransportItemDataSourceQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetTransportItemDataSourceQuery request, CancellationToken cancellationToken)
        {
            List<TransportItemDataSourceModel> model = new List<TransportItemDataSourceModel>();

            try
            {
                var result = await _dbContext.StoreItemPurchases
                                        .Include(x => x.PurchasedVehicleDetailList)
                                        .Include(x => x.PurchasedGeneratorDetailList)
                                        .Include(x => x.StoreInventoryItem)
                                        .ThenInclude(x => x.Inventory)
                                        .Include(x => x.StoreInventoryItem)
                                        .ThenInclude(x => x.StoreItemGroup)
                                        .Where(x => x.IsDeleted == false && x.StoreInventoryItem.Inventory.AssetType == request.InventoryTypeId
                                        && x.StoreInventoryItem.Inventory.InventoryId == request.InventoryId &&
                                        x.StoreInventoryItem.ItemGroupId == request.ItemGroupId).ToListAsync();

                foreach (var item in result)
                {

                    if (item.PurchasedVehicleDetailList.Any())
                    {
                        foreach (var vehicle in item.PurchasedVehicleDetailList)
                        {
                            TransportItemDataSourceModel tItem = new TransportItemDataSourceModel
                            {
                                PurchaseIdName = $"{vehicle.PlateNo}-{vehicle.PurchaseId}",
                                ItemId = vehicle.Id
                            };

                            model.Add(tItem);
                        }
                    }
                    else
                    {
                        if (item.PurchasedGeneratorDetailList.Any())
                        {
                            foreach (var generator in item.PurchasedGeneratorDetailList)
                            {
                                TransportItemDataSourceModel tItem = new TransportItemDataSourceModel
                                {
                                    PurchaseIdName = $"{generator.Voltage}-{generator.PurchaseId}",
                                    ItemId = generator.Id
                                };

                                model.Add(tItem);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return model;
        }
    }
}