using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Queries
{
    class GetInventoriesQueryHandler : IRequestHandler<GetInventoriesQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetInventoriesQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetInventoriesQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (request.AssetType != null)
                {
                    var inventoryList = await _dbContext.StoreInventories
                                     .Include("StoreItemGroups")
                                     .Include("StoreItemGroups.InventoryItems")
                                     .Where(x => x.IsDeleted == false && x.AssetType == request.AssetType).ToListAsync();

                    List<StoreInventoryModelNew> invModelList = inventoryList.Select(v => new StoreInventoryModelNew
                    {
                        Id = v.InventoryId,
                        Code = v.InventoryCode,
                        Name = v.InventoryName,
                        Description = v.InventoryDescription,
                        InventoryCreditAccount = v.InventoryCreditAccount,
                        InventoryDebitAccount = v.InventoryDebitAccount,
                        IsTransportCategory = v.IsTransportCategory,
                        AssetType = v.AssetType,
                       
                        children = v.StoreItemGroups.Where(y => y.IsDeleted == false).Select(y => new StoreItemGroupModelNew
                        {
                            Description = y.Description,
                            InventoryId = y.InventoryId,
                            Code = y.ItemGroupCode,
                            Id = y.ItemGroupId,
                            Name = y.ItemGroupName,
                            ItemTypeCategory = y.ItemTypeCategory,
                           
                            children = y.InventoryItems.Where(m => m.IsDeleted == false).Select(s => new StoreInventoryItemModelNew
                            {
                                Id = s.ItemId,
                                InventoryId = s.ItemInventory,
                                Name = s.ItemName,
                                Code = s.ItemCode,
                                Description = s.Description,
                                ItemType = s.ItemType,
                                ItemGroupId = (long)s.ItemGroupId,
                                ItemTypeCategory = s.ItemTypeCategory,
                                DefaultUnitType= s.DefaultUnitType
                            }).ToList()
                        }).ToList()


                    }).ToList();

                    response.ResponseData = invModelList;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";


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
