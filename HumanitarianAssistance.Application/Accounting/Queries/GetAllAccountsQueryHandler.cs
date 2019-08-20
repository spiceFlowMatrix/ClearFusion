using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetAllAccountsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var accountcodelist = await _dbContext.ChartOfAccountNew
                                                .Where(x => x.IsDeleted == false)
                                                .Select(c => new AccountDetailModel
                                                {
                                                    AccountCode = c.ChartOfAccountNewId,
                                                    AccountName = c.ChartOfAccountNewCode + " - " + c.AccountName,
                                                    ChartOfAccountNewCode = c.ChartOfAccountNewCode,
                                                    AccountLevelId = c.AccountLevelId
                                                }).OrderBy(x => x.AccountName).ToListAsync();


                response.data.AccountDetailList = accountcodelist;
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