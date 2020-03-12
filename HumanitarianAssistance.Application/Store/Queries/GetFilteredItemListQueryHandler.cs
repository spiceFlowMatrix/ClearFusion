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
    public class GetFilteredItemListQueryHandler : IRequestHandler<GetFilteredItemListQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetFilteredItemListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(GetFilteredItemListQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {

                var list = await _dbContext.InventoryItems.Where(x => x.IsDeleted == false &&
                                     (x.ItemCode.ToLower().Contains(request.FilterValue.ToLower()) ||
                                     x.ItemName.ToLower().Contains(request.FilterValue.ToLower())) && x.ItemGroupId == request.ItemGroupId).ToListAsync();

                response.Add("ItemList", list);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}