using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetMainLevelAccountQueryHandler : IRequestHandler<GetMainLevelAccountQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetMainLevelAccountQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetMainLevelAccountQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse(); 
            try
            {
                var mainLevelList = await _dbContext.ChartOfAccountNew
                                                    .Where(x => x.AccountHeadTypeId == request.Id &&
                                                                x.AccountLevelId == (int)AccountLevels.MainLevel && x.IsDeleted == false)
                                                    .OrderBy(x => x.ChartOfAccountNewId)
                                                    .ToListAsync();

                response.data.MainLevelAccountList = mainLevelList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
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
