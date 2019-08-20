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
        public class GetAllInventoryItemsTypeQueryHandler : IRequestHandler<GetAllInventoryItemsTypeQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;

            public GetAllInventoryItemsTypeQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<ApiResponse> Handle(GetAllInventoryItemsTypeQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {                
                var inventoryItemsList = await _dbContext.InventoryItemType.Where(x => x.IsDeleted == false).ToListAsync();

                var invModelList = inventoryItemsList.Select(v => new InventoryItemTypeModel
                {
                    TypeName = v.TypeName,
                    ItemType = v.ItemType
                }).ToList();
                response.data.InventoryItemTypeList = invModelList;
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
