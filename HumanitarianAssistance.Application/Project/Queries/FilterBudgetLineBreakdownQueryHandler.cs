using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
        public class FilterBudgetLineBreakdownQueryHandler : IRequestHandler<FilterBudgetLineBreakdownQuery, ApiResponse>
        {

            private HumanitarianAssistanceDbContext _dbContext;

            public FilterBudgetLineBreakdownQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<ApiResponse> Handle(FilterBudgetLineBreakdownQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {

                //get Budget line Breakdown from sp get_budgetlinebreakdown by passing parameters
                var spBudgetLineBreakdown = await _dbContext.LoadStoredProc("get_budgetlinebreakdown")
                                      .WithSqlParam("currency", request.CurrencyId)
                                      .WithSqlParam("projectid", request.ProjectId)
                                      .WithSqlParam("budgetlinestartdate", request.BudgetLineStartDate.ToString())
                                      .WithSqlParam("budgetlineenddate", request.BudgetLineEndDate.ToString())
                                      .WithSqlParam("budgetlineids", request.BudgetLineId)
                                      .ExecuteStoredProc<SPBudgetLineBeakdown>();

                if (spBudgetLineBreakdown.Any())
                {
                    response.data.BudgetLineBreakdownModel = new BudgetLineBreakdownModel();
                    response.data.BudgetLineBreakdownModel.Expenditure = new List<double>();
                    response.data.BudgetLineBreakdownModel.Date = new List<DateTime>();
                    response.data.BudgetLineBreakdownModel.TotalExpectedBudget = new List<double?>();

                    List<long> projects = new List<long>
                    {
                        request.ProjectId
                    };

                    var projectsExpectedBudget = await _dbContext.LoadStoredProc("get_totalexpectedprojectbudget")
                                     .WithSqlParam("currencyid", request.CurrencyId)
                                     .WithSqlParam("projectid", projects)
                                     .WithSqlParam("comparisiondate", DateTime.UtcNow.ToString())
                                     .ExecuteStoredProc<ProjectExpectedBudget>();

                    double? totalExpectedBudget = 0.0;

                    if (projectsExpectedBudget.Any())
                    {
                        totalExpectedBudget = projectsExpectedBudget.FirstOrDefault().TotalExpectedProjectBudget;
                    }

                    DateTime budgetLineStartDate = request.BudgetLineStartDate;
                    DateTime budgetLineEndDate = request.BudgetLineEndDate;

                    List<DateTime> regularIntervalDates = AccountingUtility.GetRegularIntervalDates(budgetLineStartDate, budgetLineEndDate, 6);

                    if (regularIntervalDates.Any())
                    {

                        foreach (var item in regularIntervalDates)
                        {
                            response.data.BudgetLineBreakdownModel.Expenditure.Add(spBudgetLineBreakdown.Where(x => x.VoucherDate < item).Sum(x => x.Expenditure));
                            response.data.BudgetLineBreakdownModel.TotalExpectedBudget.Add(totalExpectedBudget);
                            response.data.BudgetLineBreakdownModel.Date.Add(item);
                        }
                    }
                }

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;

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
