using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using HumanitarianAssistance.Application.Marketing.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetContractByClientIdQueryHandler : IRequestHandler<GetContractByClientIdQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;

            public GetContractByClientIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<ApiResponse> Handle(GetContractByClientIdQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                var contracts = await (from j in _dbContext.ContractDetails.AsNoTracking().AsQueryable()
                                       join jp in _dbContext.ClientDetails on j.ClientId equals jp.ClientId
                                       where !j.IsDeleted && !jp.IsDeleted && j.ClientId == request.ClientId

                                       select new ContractByClient
                                       {
                                           ClientId = jp.ClientId,
                                           ClientName = jp.ClientName,
                                           ContractId = j.ContractId
                                       }).ToListAsync();

                response.data.ContractByClientList = contracts;
                response.StatusCode = 200;
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
