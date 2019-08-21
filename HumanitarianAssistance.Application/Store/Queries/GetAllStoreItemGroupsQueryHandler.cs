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
    public class GetAllStoreItemGroupsQueryHandler : IRequestHandler<GetAllStoreItemGroupsQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllStoreItemGroupsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllStoreItemGroupsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<StoreItemGroupModel> storeItemGroupList = new List<StoreItemGroupModel>();

                if (request.inventoryId != null)
                {
                    storeItemGroupList = await _dbContext.StoreItemGroups.Where(x => x.IsDeleted == false && x.InventoryId == request.inventoryId).Select(x => new StoreItemGroupModel
                    {
                        Description = x.Description,
                        InventoryId = x.InventoryId,
                        ItemGroupCode = x.ItemGroupCode,
                        ItemGroupId = x.ItemGroupId,
                        ItemGroupName = x.ItemGroupName
                    }).ToListAsync();
                }

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
                response.data.storeItemGroupList = storeItemGroupList;
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
