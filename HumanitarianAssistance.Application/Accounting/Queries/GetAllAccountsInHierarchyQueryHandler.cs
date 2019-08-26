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

                var nodes = DisplayTree(accountList);

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

        private ChartOfAccountNode CreateHierarchy(IEnumerable<ChartOfAccountModel> objects)
        {
            var families = objects.ToLookup(x => x.ParentID);
            Console.WriteLine(families[0]);
            var topmost = families[0].SingleOrDefault();

            Func<long, IList<ChartOfAccountNode>> Children = null;

            Children = (parentID) => families[parentID]
                .OrderBy(x => x.ChartOfAccountId)
                .Select(x => new ChartOfAccountNode(x, Children(x.ChartOfAccountId))).ToList();

            return new ChartOfAccountNode(topmost, Children(topmost.ChartOfAccountId));
        }

        private List<dynamic> DisplayTree(IEnumerable<ChartOfAccountModel> elements)
        {
            var res = new List<dynamic>();
            foreach (var element in elements)
            {
                var children = DisplayTree(elements.Where(e => e.ParentID == element.ChartOfAccountId)).ToArray();
                if (children.Length != 0)
                {
                    res.Add(new
                    {
                        name = element.AccountName
                    ,
                        children = children
                    });
                }
                else
                {
                    res.Add(new
                    {
                        name = element.AccountName
                    });
              }
            }
            return res;
        }

    }
}