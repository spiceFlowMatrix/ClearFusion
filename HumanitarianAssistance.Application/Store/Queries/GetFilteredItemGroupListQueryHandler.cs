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
    public class GetFilteredItemGroupListQueryHandler : IRequestHandler<GetFilteredItemGroupListQuery, object>
    {
       private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetFilteredItemGroupListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(GetFilteredItemGroupListQuery request, CancellationToken cancellationToken)
        {
           Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {

                var list = await _dbContext.StoreItemGroups.Where(x => x.IsDeleted == false &&
                                     (x.ItemGroupCode.ToLower().Contains(request.FilterValue.ToLower()) ||
                                     x.ItemGroupName.ToLower().Contains(request.FilterValue.ToLower())) && x.InventoryId == request.InventoryId).ToListAsync();

                response.Add("ItemGroupList", list);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}