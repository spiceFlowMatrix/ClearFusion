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
                    Quantity = y.Quantity,
                    EstimatedCost = y.EstimatedCost,
                    Availability = (_dbContext.StoreItemPurchases.Where(a=>a.IsDeleted==false && a.InventoryItem==y.ItemId).Sum(v=>v.Quantity)),
                    ItemId = y.ItemId
                })
                .ToListAsync();

                // var purchaseitems= await _dbContext.StoreItemPurchases.Where(x=>x.IsDeleted==false)
                // .GroupBy(y=>y.InventoryItem).Select(z=> new {ItemId=z.Key,TotalPurchase=z.Sum(y=>y.Quantity)}).ToListAsync();
                
                // var issueditems= await _dbContext.StorePurchaseOrders.Where(x=>x.IsDeleted==false)
                // .GroupBy(y=>y.InventoryItem).Select(z=> new {ItemId=z.Key,TotalIssued=z.Sum(y=>y.IssuedQuantity)}).ToListAsync();
                
                // foreach(var item in itemlist){
                //     await purchaseitems.FirstOrDefaultAsync();
                // }
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
