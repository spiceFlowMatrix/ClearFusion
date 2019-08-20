using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
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
    public class GetInventoryCodeQueryHandler : IRequestHandler<GetInventoryCodeQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetInventoryCodeQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetInventoryCodeQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                StoreInventory storeInventories = await _dbContext.StoreInventories.OrderByDescending(x => x.InventoryCode).FirstOrDefaultAsync(x => x.AssetType == request.Id && x.IsDeleted == false);

                if (storeInventories != null)
                {
                    int InventoryNumber = Convert.ToInt32(storeInventories.InventoryCode.Substring(1));

                    if (request.Id == (int)InventoryMasterType.Consumables)
                    {
                        response.data.InventoryCode = "C" + String.Format("{0:D2}", ++InventoryNumber);
                    }
                    else if (request.Id == (int)InventoryMasterType.Expendables)
                    {
                        response.data.InventoryCode = "E" + String.Format("{0:D2}", ++InventoryNumber);
                    }
                    else
                    {
                        response.data.InventoryCode = "N" + String.Format("{0:D2}", ++InventoryNumber);
                    }
                }
                else
                {
                    if (request.Id == (int)InventoryMasterType.Consumables)
                    {
                        response.data.InventoryCode = "C" + String.Format("{0:D2}", 1);
                    }
                    else if (request.Id == (int)InventoryMasterType.Expendables)
                    {
                        response.data.InventoryCode = "E" + String.Format("{0:D2}", 1);
                    }
                    else
                    {
                        response.data.InventoryCode = "N" + String.Format("{0:D2}", 1);
                    }
                }

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
