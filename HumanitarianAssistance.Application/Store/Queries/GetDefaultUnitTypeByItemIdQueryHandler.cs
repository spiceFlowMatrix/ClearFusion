using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetDefaultUnitTypeByItemIdQueryHandler: IRequestHandler<GetDefaultUnitTypeByItemIdQuery, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetDefaultUnitTypeByItemIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetDefaultUnitTypeByItemIdQuery request, CancellationToken cancellationToken)
        {
            int? unitTypeId = null;

            try
            {
                StoreInventoryItem item = await _dbContext.InventoryItems.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.ItemId == request.Id);

                if(item != null)
                {
                    unitTypeId = item.DefaultUnitType;
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }

            return unitTypeId;
        }
    }
}