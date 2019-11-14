using System.Threading;
using System;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.Project.Models;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetPurchasedItemsListQueryHandler: IRequestHandler<GetPurchasedItemsListQuery, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;
        public GetPurchasedItemsListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetPurchasedItemsListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try 
            {
                var requestItems = await _dbContext.ProjectLogisticItems
                                .Where(x => x.IsDeleted == false && x.LogisticRequestsId == request.RequestId && x.PurchaseSubmitted == true)
                                .Select(x=>new LogisticItemModel{
                                    Id= x.LogisticItemId,
                                    Item = x.StoreInventoryItem.ItemName,
                                    Quantity = x.Quantity,
                                    EstimatedCost = x.FinalCost,
                                    ItemId = x.StoreInventoryItem.ItemId
                                })
                                .ToListAsync();
                    
                response.data.LogisticsItemList = requestItems;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch(Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message =ex.Message;
            }
            return response;

        }
    }
}