using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetFilteredBudegtListQueryHandler : IRequestHandler<GetFilteredBudegtListQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetFilteredBudegtListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(GetFilteredBudegtListQuery request, CancellationToken cancellationToken)
        {
             Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {

                var budgetList = await _dbContext.ProjectBudgetLineDetail.Where(x => x.IsDeleted == false &&
                                     (x.BudgetCode.ToLower().Contains(request.FilterValue.ToLower()) ||
                                     x.BudgetName.ToLower().Contains(request.FilterValue.ToLower())) && x.ProjectId == request.ProjectId).ToListAsync();

                response.Add("BudgetLineList", budgetList);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}