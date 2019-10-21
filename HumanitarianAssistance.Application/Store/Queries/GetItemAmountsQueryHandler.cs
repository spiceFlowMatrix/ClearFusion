using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
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
    public class GetItemAmountsQueryHandler : IRequestHandler<GetItemAmountsQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetItemAmountsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetItemAmountsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                //int procuredAmount, spentAmount;
                var procuredAmount = await _dbContext.StoreItemPurchases.Where(x => x.InventoryItem == request.ItemId && x.IsDeleted == false).ToListAsync();

                //NOTE: x.MustReturn == false --> Use to keep track if Employee Returned the Item or not.
                var spentAmount = await _dbContext.StorePurchaseOrders.Where(x => x.InventoryItem == request.ItemId && x.IsDeleted == false && x.Returned == false).ToListAsync();


                response.ItemAmount.ProcuredAmount = procuredAmount != null ? procuredAmount.Sum(x => x.Quantity) : 0;
                response.ItemAmount.SpentAmount = spentAmount != null ? spentAmount.Sum(x => x.IssuedQuantity) : 0;
                response.ItemAmount.CurrentAmount = response.ItemAmount.ProcuredAmount - response.ItemAmount.SpentAmount;
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
