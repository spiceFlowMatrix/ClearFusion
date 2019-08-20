using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetCurrencyByCurrencyCodeQueryHandler: IRequestHandler<GetCurrencyByCurrencyCodeQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetCurrencyByCurrencyCodeQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(GetCurrencyByCurrencyCodeQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            
            try
            {
                CurrencyModel obj = new CurrencyModel();
                var currencylist = await (from c in _dbContext.CurrencyDetails
                                    where c.CurrencyCode == request.CurrencyCode
                                    select new CurrencyModel
                                    {
                                        CurrencyId = c.CurrencyId,
                                        CurrencyCode = c.CurrencyCode,
                                        CurrencyName = c.CurrencyName,
                                        CreatedById = c.CreatedById,
                                        CreatedDate = c.CreatedDate,
                                        ModifiedById = c.ModifiedById,
                                        ModifiedDate = c.ModifiedDate
                                    }).ToListAsync();
                                    
                response.data.CurrencyList = currencylist;
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