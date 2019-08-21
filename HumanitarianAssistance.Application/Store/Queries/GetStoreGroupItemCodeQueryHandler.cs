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
    public class GetStoreGroupItemCodeQueryHandler : IRequestHandler<GetStoreGroupItemCodeQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetStoreGroupItemCodeQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetStoreGroupItemCodeQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            string ItemGroupCode = "";
            try
            {
                if (request.inventoryId != null)
                {
                    StoreItemGroup storeItemGroup = await _dbContext.StoreItemGroups
                                                                             .OrderByDescending(x => x.CreatedDate)
                                                                             .Include(x => x.StoreInventory)
                                                                             .FirstOrDefaultAsync(x => x.IsDeleted == false && x.InventoryId == request.inventoryId);
                    if (storeItemGroup != null)
                    {
                        long count = Convert.ToInt64(storeItemGroup.ItemGroupCode.Substring(4));
                        ItemGroupCode = storeItemGroup.StoreInventory.InventoryCode + "-" + String.Format("{0:D2}", ++count);
                    }
                    else
                    {
                        StoreInventory storeInventory = await _dbContext.StoreInventories.FirstOrDefaultAsync(x => x.IsDeleted == false && x.InventoryId == request.inventoryId);

                        ItemGroupCode = storeInventory.InventoryCode + "-" + String.Format("{0:D2}", 1);
                    }
                }

                response.data.ItemGroupCode = ItemGroupCode;
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
