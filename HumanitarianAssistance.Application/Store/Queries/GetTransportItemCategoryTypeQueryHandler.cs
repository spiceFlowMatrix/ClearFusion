using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetTransportItemCategoryTypeQueryHandler: IRequestHandler<GetTransportItemCategoryTypeQuery, int?>
    {
        private HumanitarianAssistanceDbContext _dbContext;

            public GetTransportItemCategoryTypeQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }

        public async Task<int?> Handle(GetTransportItemCategoryTypeQuery request, CancellationToken cancellationToken)
        {
            int? transportItemCategoryType = 0;

            try
            {
                var item = await _dbContext.InventoryItems.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.ItemId == request.ItemId);

                if(item != null)
                {
                    transportItemCategoryType = item.ItemTypeCategory;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return transportItemCategoryType;
        }
    }
}