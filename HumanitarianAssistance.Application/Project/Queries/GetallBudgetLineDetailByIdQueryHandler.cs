using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetallBudgetLineDetailByIdQueryHandler : IRequestHandler<GetallBudgetLineDetailByIdQuery, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public GetallBudgetLineDetailByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetallBudgetLineDetailByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<ProjectBudgetLineDetail> budgetList = await _dbContext.ProjectBudgetLineDetail
                                          .Include(c => c.CurrencyDetails)
                                          .Include(j => j.ProjectJobDetail)
                                          .Where(x => x.IsDeleted == false && x.ProjectId == request.ProjectId)
                                          .OrderBy(x => x.ProjectId)
                                          .ToListAsync();

                List<ProjectBudgetLineDetailsModel> budgetDetaillist = budgetList.Select(b => new ProjectBudgetLineDetailsModel
                {
                    BudgetLineId = b.BudgetLineId,
                    BudgetCode = b.BudgetCode,
                    BudgetName = b.BudgetName,
                    CurrencyId = b.CurrencyDetails?.CurrencyId ?? null,
                    InitialBudget = b.InitialBudget,
                    ProjectId = b.ProjectId,
                    ProjectJobName = b.ProjectJobDetail?.ProjectJobName ?? null,
                    ProjectJobCode = b.ProjectJobDetail?.ProjectJobCode ?? null,
                    CurrencyName = b.CurrencyDetails?.CurrencyName ?? null,
                    ProjectJobId = b.ProjectJobDetail?.ProjectJobId ?? null,
                    BudgetCodeName = b.BudgetCode + "-" + b.BudgetName
                }).ToList();
                response.data.ProjectBudgetLineDetailList = budgetDetaillist.OrderByDescending(x => x.BudgetLineId).ToList();
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
