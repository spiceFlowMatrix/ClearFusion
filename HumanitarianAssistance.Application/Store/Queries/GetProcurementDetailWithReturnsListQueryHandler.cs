using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetProcurementDetailWithReturnsListQueryHandler: IRequestHandler<GetProcurementDetailWithReturnsListQuery, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetProcurementDetailWithReturnsListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetProcurementDetailWithReturnsListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbContext.StorePurchaseOrders
                                      .Include(x=> x.ReturnProcurementDetailList)
                                      .Where(x=> x.IsDeleted == false)

            }
            catch(Exception ex)
            {
                throw ex;
            }


        }
    }
}