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
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllInventoryItemsQueryHandler : IRequestHandler<GetAllInventoryItemsQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllInventoryItemsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllInventoryItemsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<StoreInventoryItem> inventoryItemsList = new List<StoreInventoryItem>();

                if (request.ItemGroupId != 0)
                {

                    inventoryItemsList = await _dbContext.InventoryItems
                                                   .Where(x => x.IsDeleted == false && x.ItemGroupId == request.ItemGroupId)
                                                   .ToListAsync();
                }
                else
                {
                    inventoryItemsList = await _dbContext.InventoryItems
                                                   .Where(x => x.IsDeleted == false)
                                                   .ToListAsync(); 
                }

                var invModelList = inventoryItemsList.Select(v => new StoreInventoryItemModel
                {
                    ItemId = v.ItemId,
                    ItemInventory = v.ItemInventory,
                    ItemName = v.ItemName,
                    ItemCode = v.ItemCode,
                    Description = v.Description,
                    ItemType = v.ItemType,
                    ItemGroupId = (long)v.ItemGroupId
                }).ToList();

                response.data.InventoryItemList = invModelList;
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
