using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;

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
                

            }
            catch(Exception ex)
            {
                throw ex;
            }


        }
    }
}