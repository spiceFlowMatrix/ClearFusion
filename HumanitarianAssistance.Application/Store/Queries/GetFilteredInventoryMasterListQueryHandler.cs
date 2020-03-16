using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetFilteredInventoryMasterListQueryHandler : IRequestHandler<GetFilteredInventoryMasterListQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetFilteredInventoryMasterListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(GetFilteredInventoryMasterListQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {

                var inventoryMasterList = await _dbContext.StoreInventories.Where(x => x.IsDeleted == false &&
                                     (x.InventoryCode.ToLower().Contains(request.FilterValue.ToLower()) ||
                                     x.InventoryName.ToLower().Contains(request.FilterValue.ToLower())) && x.AssetType == request.AssetType).ToListAsync();

                response.Add("InventoryMasterList", inventoryMasterList);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}