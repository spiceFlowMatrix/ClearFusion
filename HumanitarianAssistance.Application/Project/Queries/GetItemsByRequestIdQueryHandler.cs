using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using HumanitarianAssistance.Application.Project.Models;

namespace HumanitarianAssistance.Application.Project.Queries
{
        public class GetItemsByRequestIdQueryHandler : IRequestHandler<GetItemsByRequestIdQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;

            public GetItemsByRequestIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<ApiResponse> Handle(GetItemsByRequestIdQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                var itemlist= await _dbContext.ProjectLogisticItems.Where(x=>x.IsDeleted==false && x.LogisticRequestsId==request.RequestId)
                .Select(y=> new LogisticItemModel{
                    Id = y.LogisticItemId,
                    Item = y.StoreInventoryItem.ItemName,
                    ItemCode= y.StoreInventoryItem.ItemCode,
                    Quantity = y.Quantity,
                    EstimatedCost = y.EstimatedUnitCost,
                    ItemId = y.ItemId
                })
                .ToListAsync();

                
                foreach(var item in itemlist){
                    var purchaseitems = _dbContext.StoreItemPurchases.Where(a=>a.IsDeleted==false && a.InventoryItem== item.ItemId).Select(v=>v.Quantity).DefaultIfEmpty(0).Sum();
                    var issueditems = _dbContext.StorePurchaseOrders.Where(a=>a.IsDeleted==false && a.InventoryItem== item.ItemId).Select(v=>v.IssuedQuantity).DefaultIfEmpty(0).Sum();
                    item.Availability = purchaseitems - issueditems;
                }
                response.data.LogisticsItemList = itemlist;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
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
