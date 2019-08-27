using System;
using System.Collections.Generic;
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
    public class GetAllAccountsInHierarchyQueryHandler : IRequestHandler<GetAllAccountsInHierarchyQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetAllAccountsInHierarchyQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllAccountsInHierarchyQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                List<ChartOfAccountModel> sdf = new List<ChartOfAccountModel>();

                var accountList = await _dbContext.ChartOfAccountNew
                                                  .Where(x => !x.IsDeleted)
                                                  .Select(x => new ChartOfAccountModel
                                                  {
                                                      ChartOfAccountId = x.ChartOfAccountNewId,
                                                      ChartOfAccountCode = x.ChartOfAccountNewCode,
                                                      AccountName = x.AccountName,
                                                      ParentID = x.ParentID,
                                                      AccountLevelId = x.AccountLevelId,
                                                  }).ToListAsync();

                // var nodes = CreateHierarchy(accountList);

                var nodes = GetAccountsHierarchy(accountList);

                response.ResponseData = nodes;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

        private List<ChartOfAccountNode> GetAccountsHierarchy(IEnumerable<ChartOfAccountModel> elements, long id = 0)
        {

            return elements
                    .Where(c => c.ParentID == id)
                    .Select(c => new ChartOfAccountNode
                    {
                        ChartOfAccountId = c.ChartOfAccountId,
                        ChartOfAccountCode = c.ChartOfAccountCode,
                        AccountName = c.AccountName,
                        ParentID = c.ParentID,
                        AccountLevelId = c.AccountLevelId,
                        ChildAccounts = GetAccountsHierarchy(elements, c.ChartOfAccountId)
                    })
                    .ToList();
        }

    }
}