using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetFilteredBudgetLineListQueryHandler: IRequestHandler<GetFilteredBudgetLineListQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetFilteredBudgetLineListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetFilteredBudgetLineListQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {
                if(String.IsNullOrEmpty(request.FilterValue))
                {
                    var budgetLineList = await _dbContext.ProjectBudgetLineDetail.Where(x=> x.IsDeleted == false
                                  && x.ProjectId == request.ProjectId)
                                  .OrderByDescending(x=>x.CreatedDate)
                                  .Select(x=> new {
                                      BudgetLineId= x.BudgetLineId,
                                      BudgetLineName=  x.BudgetName,
                                      BudgetLineCode= x.BudgetCode
                                  })
                                  .Take(15)
                                  .ToListAsync();

                    response.Add("budgetLineList", budgetLineList);
                }
                else 
                {
                    var budgetLineList = await _dbContext.ProjectBudgetLineDetail.Where(x=> x.IsDeleted == false &&
                                  ((x.BudgetCode.ToLower().Contains(request.FilterValue.ToLower()) ||
                                  x.BudgetName.ToLower().Contains(request.FilterValue.ToLower())) && x.ProjectId == request.ProjectId
                                  ))
                                  .Select(x=> new {
                                      BudgetLineId= x.BudgetLineId,
                                      BudgetLineName=  x.BudgetName,
                                      BudgetLineCode= x.BudgetCode
                                  })
                                  .ToListAsync();

                    response.Add("budgetLineList", budgetLineList);
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