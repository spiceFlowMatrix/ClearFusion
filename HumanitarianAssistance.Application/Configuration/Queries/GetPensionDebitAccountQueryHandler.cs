using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetPensionDebitAccountQueryHandler : IRequestHandler<GetPensionDebitAccountQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetPensionDebitAccountQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetPensionDebitAccountQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                PensionDebitAccountMaster pensionDebitAccount = await _dbContext.PensionDebitAccountMaster.FirstOrDefaultAsync(x => x.IsDeleted == false);

                if (pensionDebitAccount != null)
                {
                    response.data.PensionDebitAccountId = pensionDebitAccount.ChartOfAccountNewId;
                }

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
