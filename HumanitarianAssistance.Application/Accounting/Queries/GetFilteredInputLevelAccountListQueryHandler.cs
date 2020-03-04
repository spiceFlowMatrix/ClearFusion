using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetFilteredInputLevelAccountListQueryHandler: IRequestHandler<GetFilteredInputLevelAccountListQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetFilteredInputLevelAccountListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetFilteredInputLevelAccountListQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {
                if(request.FilterValue == null || request.FilterValue == "null")
                {
                    var accountList = await _dbContext.ChartOfAccountNew.Where(x=> x.IsDeleted == false &&
                                  x.AccountLevelId == (int)AccountLevels.InputLevel)
                                    .OrderByDescending(x=>x.CreatedDate).Take(15).ToListAsync();

                    response.Add("AccountList", accountList);
                }
                else 
                {
                    var accountList = await _dbContext.ChartOfAccountNew.Where(x=> x.IsDeleted == false &&
                                  (x.ChartOfAccountNewCode.ToLower().Contains(request.FilterValue.ToLower()) ||
                                  x.AccountName.ToLower().Contains(request.FilterValue.ToLower())) &&
                                  x.AccountLevelId == (int)AccountLevels.InputLevel).ToListAsync();

                    response.Add("AccountList", accountList);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return response;      
        }
    }
}