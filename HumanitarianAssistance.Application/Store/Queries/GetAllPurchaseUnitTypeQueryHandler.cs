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
    public class GetAllPurchaseUnitTypeQueryHandler : IRequestHandler<GetAllPurchaseUnitTypeQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAllPurchaseUnitTypeQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllPurchaseUnitTypeQuery request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                //var inventoryItemsList = await Task.Run(() =>
                //	_uow.GetDbContext().InventoryItemType
                //		.Where(c => c.IsDeleted == false )
                //		.OrderByDescending(c => c.CreatedDate));

                var purchaseUnitTypeList = await _dbContext.PurchaseUnitType.Where(x => x.IsDeleted == false).ToListAsync();

                var invModelList = purchaseUnitTypeList.Select(v => new PurchaseUnitType
                {
                    UnitTypeName = v.UnitTypeName,
                    UnitTypeId = v.UnitTypeId
                }).ToList();
                response.data.PurchaseUnitTypeList = invModelList;
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
