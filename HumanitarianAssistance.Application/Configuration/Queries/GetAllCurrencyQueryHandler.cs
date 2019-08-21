using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using System.Linq;
using MediatR;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllCurrencyQueryHandler: IRequestHandler<GetAllCurrencyQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetAllCurrencyQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllCurrencyQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var currencylist = await (from c in _dbContext.CurrencyDetails
                                    orderby c.CurrencyName ascending
                                    select new CurrencyModel
                                    {
                                        CurrencyId = c.CurrencyId,
                                        CurrencyCode = c.CurrencyCode,
                                        CurrencyName = c.CurrencyName,
                                        CreatedById = c.CreatedById,
                                        CreatedDate = c.CreatedDate,
                                        ModifiedById = c.ModifiedById,
                                        ModifiedDate = c.ModifiedDate,
                                    }).ToListAsync();
                                    
                response.data.CurrencyList = currencylist;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
        
    }
}