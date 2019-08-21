using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetSerialNumberQueryHandler : IRequestHandler<GetSerialNumberQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetSerialNumberQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetSerialNumberQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var checkSerialNumber = await _dbContext.StoreItemPurchases.FirstOrDefaultAsync(x => x.SerialNo == request.serialNumber);
                if (checkSerialNumber != null)
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Serial Number already exits";
                    return response;
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
                return response;
            }
            return response;
        }
    }
}
