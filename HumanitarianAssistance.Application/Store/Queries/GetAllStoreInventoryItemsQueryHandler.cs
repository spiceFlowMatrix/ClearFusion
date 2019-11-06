using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using HumanitarianAssistance.Application.Store.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllStoreInventoryItemsQueryHandler : IRequestHandler<GetAllStoreInventoryItemsQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAllStoreInventoryItemsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllStoreInventoryItemsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var res = await _dbContext.InventoryItems.Where(x=>x.IsDeleted==false).Select(v => new StoreInventoryItemModel
                {
                    ItemId = v.ItemId,
                    ItemInventory = v.ItemInventory,
                    ItemName = v.ItemName,
                    ItemCode = v.ItemCode,
                    Description = v.Description,
                    ItemType = v.ItemType,
                    ItemGroupId = (long)v.ItemGroupId
                }).ToListAsync();

                response.data.InventoryItemList = res;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}
