using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetExchangeGainLossFilterAccountListQueryHandler: IRequestHandler<GetExchangeGainLossFilterAccountListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetExchangeGainLossFilterAccountListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetExchangeGainLossFilterAccountListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                GainLossCalculatorConfiguration gainLossSelectedAccounts = await _dbContext.GainLossCalculatorConfiguration.FirstOrDefaultAsync(x => x.IsDeleted == false && x.UserId == request.UserId);
                response.data.GainLossSelectedAccounts = gainLossSelectedAccounts.SelectedAccounts;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "success";
            }
            catch (Exception exception)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = exception.Message;
            }

            return response;
        }
    }
}