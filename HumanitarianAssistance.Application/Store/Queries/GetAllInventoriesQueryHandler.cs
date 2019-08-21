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
    public class GetAllInventoriesQueryHandler : IRequestHandler<GetAllInventoriesQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllInventoriesQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllInventoriesQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {

                List<StoreInventory> inventoryList = new List<StoreInventory>();

                if (request.AssetType != null)
                {
                    inventoryList = await _dbContext.StoreInventories
                           .Where(c => c.IsDeleted == false && c.AssetType == request.AssetType)
                           .OrderByDescending(c => c.InventoryCode).ToListAsync();
                }
                else
                {
                    inventoryList = await _dbContext.StoreInventories
                           .Where(c => c.IsDeleted == false)
                           .OrderByDescending(c => c.InventoryCode).ToListAsync();
                }

                List<StoreInventoryModel> invModelList = inventoryList.Select(v => new StoreInventoryModel
                {
                    InventoryId = v.InventoryId,
                    InventoryCode = v.InventoryCode,
                    InventoryName = v.InventoryName,
                    InventoryDescription = v.InventoryDescription,
                    InventoryCreditAccount = v.InventoryCreditAccount,
                    InventoryDebitAccount = v.InventoryDebitAccount,
                    AssetType = v.AssetType
                }).ToList();
                response.data.InventoryList = invModelList;
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
