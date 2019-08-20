using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllBudgetFilterListQueryHandler : IRequestHandler<GetAllBudgetFilterListQuery, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllBudgetFilterListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllBudgetFilterListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            string budgetLineIdNoValue = null;
            string budgetCodeNoValue = null;
            string budgetNameNoValue = null;
            string projectJobIdValue = null;
            string initialbudgetNovalue = null;
            string projectJobName = null;

            string dateValue = null;

            if (!string.IsNullOrEmpty(request.FilterValue))
            {
                budgetLineIdNoValue = request.BudgetLineIdFlag ? request.FilterValue.ToLower().Trim() : null;
                budgetCodeNoValue = request.BudgetCodeFlag ? request.FilterValue.ToLower().Trim() : null;
                budgetNameNoValue = request.BudgetNameFlag ? request.FilterValue.ToLower().Trim() : null;
                projectJobIdValue = request.ProjectJobIdFlag ? request.FilterValue.ToLower().Trim() : null;
                initialbudgetNovalue = request.InitialBudgetFlag ? request.FilterValue.ToLower().Trim() : null;
                dateValue = request.DateFlag ? request.FilterValue.ToLower().Trim() : null;
                projectJobName = request.ProjectJobNameFlag ? request.FilterValue.ToLower().Trim() : null;
            }

            try
            {

                int totalCount = await _dbContext.ProjectBudgetLineDetail
                                       .Where(v => v.IsDeleted == false && v.ProjectId == request.ProjectId
                                               && !string.IsNullOrEmpty(request.FilterValue)
                                               ? (
                                               v.BudgetLineId.ToString().Trim().Contains(budgetLineIdNoValue) ||
                                               v.BudgetCode.Trim().ToLower().Contains(budgetCodeNoValue) ||
                                               v.BudgetName.Trim().ToLower().Contains(budgetNameNoValue) ||
                                               v.ProjectJobId.ToString().Contains(projectJobIdValue) ||
                                               v.InitialBudget.ToString().Contains(initialbudgetNovalue) ||
                                               v.ProjectJobDetail.ProjectJobName.Trim().ToLower().Contains(projectJobName) ||
                                               v.CreatedDate.ToString().Trim().Contains(dateValue)
                                               ) : v.ProjectId == request.ProjectId
                                       )
                                      .CountAsync();

                var budgetLineList = await _dbContext.ProjectBudgetLineDetail
                                              .Include(x => x.VoucherTransactions)
                                              .ThenInclude(x => x.VoucherDetails)
                                              .ThenInclude(x => x.CurrencyDetail)
                                              .Where(v => v.ProjectId == request.ProjectId && v.IsDeleted == false &&
                                                        !string.IsNullOrEmpty(request.FilterValue) ? (
                                                          v.BudgetLineId.ToString().Trim().Contains(budgetLineIdNoValue) ||
                                                          v.BudgetCode.Trim().ToLower().Contains(budgetCodeNoValue) ||
                                                          v.BudgetName.Trim().ToLower().Contains(budgetNameNoValue) ||
                                                          v.ProjectJobId.ToString().Contains(projectJobIdValue) ||
                                                          v.ProjectJobDetail.ProjectJobName.Trim().ToLower().Contains(projectJobName) ||
                                                          v.InitialBudget.ToString().Contains(initialbudgetNovalue) ||
                                                          v.CreatedDate.ToString().Trim().ToLower().Contains(dateValue)
                                                          ) : v.ProjectId == request.ProjectId
                                               )
                                      .OrderByDescending(x => x.CreatedDate)
                                      .Select(x => new ProjectBudgetLineDetailsModel
                                      {
                                          BudgetLineId = x.BudgetLineId,
                                          BudgetCode = x.BudgetCode,
                                          BudgetName = x.BudgetName,
                                          ProjectId = x.ProjectId,
                                          CurrencyId = x.CurrencyId,
                                          CurrencyName = x.CurrencyDetails.CurrencyName,
                                          InitialBudget = x.InitialBudget,
                                          ProjectJobCode = x.ProjectJobDetail.ProjectJobCode,
                                          ProjectJobId = x.ProjectJobId,
                                          ProjectJobName = x.ProjectJobDetail.ProjectJobName,
                                          CreatedDate = x.CreatedDate,
                                          DebitPercentage = ((x.VoucherTransactions.Where(y => y.IsDeleted == false &&
                                                                              y.VoucherDetails.CurrencyId == x.CurrencyId).Sum(s => s.Debit)) / x.InitialBudget) * 100,
                                          Expenditure = (x.VoucherTransactions.Where(y => y.IsDeleted == false && y.VoucherDetails.CurrencyId == x.CurrencyId).Sum(s => s.Debit))
                                      })
                                      .Skip(request.pageSize.Value * request.pageIndex.Value)
                                      .Take(request.pageSize.Value)
                                      .ToListAsync();

                response.data.ProjectBudgetLineList = budgetLineList.OrderByDescending(x => x.DebitPercentage).ToList();
                response.data.TotalCount = totalCount;
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
