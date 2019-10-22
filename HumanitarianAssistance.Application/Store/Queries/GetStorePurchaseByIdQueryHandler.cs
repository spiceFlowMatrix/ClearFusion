using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetStorePurchaseByIdQueryHandler: IRequestHandler<GetStorePurchaseByIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetStorePurchaseByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetStorePurchaseByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                StorePurchaseModel model= new StorePurchaseModel();

                model = _dbContext.StoreItemPurchases
                                  .Include(x=> x.)



            }
            catch(Exception exception)
            {
                throw exception;
            }
        }
    }
}