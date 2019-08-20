using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
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
    public class GetAllItemsOrderQueryHandler : IRequestHandler<GetAllItemsOrderQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAllItemsOrderQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllItemsOrderQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                //var orders = await _uow.PurchaseOrderRepository.FindAllAsync(x => x.InventoryItem == ItemId && x.IsDeleted == false);
                var orders = await _dbContext.StorePurchaseOrders.Include(x => x.StoreInventoryItem).Where(x => x.InventoryItem == request.ItemId && x.IsDeleted == false).ToListAsync();

                //var user = await _userManager.
                //var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);




                var ordersList = orders.Select(x => new ItemOrderModel
                {
                    InventoryItem = x.InventoryItem,
                    IssueDate = x.IssueDate,
                    IssuedQuantity = x.IssuedQuantity,
                    IssuedToEmployeeId = x.IssuedToEmployeeId,
                    MustReturn = x.MustReturn,
                    Returned = x.Returned,
                    OrderId = x.OrderId,
                    Purchase = x.Purchase,
                    ReturnedDate = x.ReturnedDate,
                    IssedToLocation = x.IssedToLocation,
                    IssueVoucherNo = x.IssueVoucherNo.ToString(),
                    Project = x.Project,
                    Remarks = x.Remarks,
                    StatusAtTimeOfIssue = x.StatusAtTimeOfIssue
                    //InventoryName = x.StoreInventoryItem.Inventory.InventoryName,
                    //InventoryItemName = x.StoreInventoryItem.ItemName,
                }).ToList();

                response.data.ItemOrderModelList = ordersList;
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
