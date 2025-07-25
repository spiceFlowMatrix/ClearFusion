using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using HumanitarianAssistance.Application.Project.Models;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetBudgetLinesByMultipleProjectIdsQueryHandler : IRequestHandler<GetBudgetLinesByMultipleProjectIdsQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetBudgetLinesByMultipleProjectIdsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetBudgetLinesByMultipleProjectIdsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var list = await _dbContext.ProjectBudgetLineDetail.Where(x=>x.IsDeleted == false && request.projectIds.Contains(x.ProjectId))
                        .Select(x => new ProjectBudgetLineDetailsModel{
                                BudgetCode = x.BudgetCode,
                                BudgetCodeName = x.BudgetCode + "-" + x.BudgetName,
                                BudgetLineId = x.BudgetLineId,
                                BudgetName = x.BudgetName,
                                CurrencyId = x.CurrencyId,
                                InitialBudget = x.InitialBudget,
                                ProjectId = x.ProjectId,
                                ProjectJobId = x.ProjectJobId,                                                                          
                        }).ToListAsync();
                response.data.ProjectBudgetLineDetailList = list;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch(Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}