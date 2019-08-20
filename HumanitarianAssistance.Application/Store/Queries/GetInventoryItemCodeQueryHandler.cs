using HumanitarianAssistance.Application.Infrastructure;
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
    public class GetInventoryItemCodeQueryHandler : IRequestHandler<GetInventoryItemCodeQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetInventoryItemCodeQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetInventoryItemCodeQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            string InventoryItemCode = "";

            try
            {
                if (request.Id != 0)
                {
                    StoreInventoryItem storeInventoryItem = await _dbContext.InventoryItems
                                                                      .OrderByDescending(x => x.CreatedDate)
                                                                      .Include(x => x.StoreItemGroup)
                                                                      .FirstOrDefaultAsync(x => x.IsDeleted == false
                                                                      && x.ItemGroupId == request.Id);


                    if (storeInventoryItem != null)
                    {
                        int count = Convert.ToInt32(storeInventoryItem.ItemCode.Substring(7));
                        InventoryItemCode = storeInventoryItem.StoreItemGroup.ItemGroupCode + "-" + String.Format("{0:D2}", ++count);
                    }
                    else
                    {
                        StoreItemGroup storeItemGroup = await _dbContext.StoreItemGroups.OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync(x => x.IsDeleted == false && x.ItemGroupId == request.Id);
                        InventoryItemCode = storeItemGroup.ItemGroupCode + "-" + String.Format("{0:D2}", 1);
                    }
                }

                response.data.InventoryItemCode = InventoryItemCode;
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
