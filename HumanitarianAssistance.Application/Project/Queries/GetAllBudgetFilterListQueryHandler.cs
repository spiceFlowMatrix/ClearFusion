using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;
using System.Globalization;

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
            DateTime parsedDateTime;
            double formatvalue;
            long formatBudgetId = 0;
            string budgetLineFilterValue = null;

            string budgetLineIdNoValue = null;
            string budgetCodeNoValue = null;
            string budgetNameNoValue = null;
            string projectJobIdValue = null;
            string initialbudgetNovalue = null;
            string projectJobName = null;
            string value = string.Empty;

            string dateValue = null;
            string formattedDate = null;

            if (!string.IsNullOrEmpty(request.FilterValue))
            {
                budgetLineFilterValue = request.BudgetLineIdFlag ? request.FilterValue.ToLower().Trim() : null;

                budgetLineIdNoValue = request.BudgetLineIdFlag ? request.FilterValue.ToLower().Trim() : null;

                if (Int64.TryParse(budgetLineIdNoValue, out formatBudgetId))
                {
                    budgetLineIdNoValue = formatBudgetId.ToString();
                }
                else
                {
                    budgetLineIdNoValue = "";
                }

                budgetCodeNoValue = request.BudgetCodeFlag ? request.FilterValue.ToLower().Trim() : null;
                budgetNameNoValue = request.BudgetNameFlag ? request.FilterValue.ToLower().Trim() : null;
                projectJobIdValue = request.ProjectJobIdFlag ? request.FilterValue.ToLower().Trim() : null;
                if (Int64.TryParse(projectJobIdValue, out formatBudgetId))
                {
                    projectJobIdValue = formatBudgetId.ToString();
                }
                else
                {
                    projectJobIdValue = "";
                }
                initialbudgetNovalue = request.InitialBudgetFlag ? request.FilterValue.ToLower().Trim() : null;
                if (double.TryParse(initialbudgetNovalue, out formatvalue))
                {
                    initialbudgetNovalue = formatvalue.ToString();
                }
                else
                {
                    initialbudgetNovalue = "";
                }
                dateValue = request.DateFlag ? request.FilterValue.ToLower().Trim() : null;
                projectJobName = request.ProjectJobNameFlag ? request.FilterValue.ToLower().Trim() : null;
                if (DateTime.TryParseExact(dateValue, "dd/mm/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out parsedDateTime))
                {
                    formattedDate = parsedDateTime.ToString("yyyy-mm-dd");
                    // Note If filter value is date then bugetcode and budgetname value will be emplty
                    budgetCodeNoValue = "";
                    budgetNameNoValue = "";
                }
                else
                {
                    formattedDate = "";
                }
            }

            try
            {

              
                var spbudgetLineList = await _dbContext.LoadStoredProc("get_budget_line_list")
                                      .WithSqlParam("project_id", request.ProjectId)
                                      .WithSqlParam("budget_line_id", budgetLineIdNoValue == null ? "" : budgetLineIdNoValue)
                                      .WithSqlParam("budget_code", budgetCodeNoValue == null ? "" : budgetCodeNoValue)
                                      .WithSqlParam("budget_name", budgetNameNoValue == null ? "" : budgetNameNoValue)
                                      .WithSqlParam("project_job_id", projectJobIdValue == null ? "" : projectJobIdValue)
                                      .WithSqlParam("initial_budget", initialbudgetNovalue == null ? "" : initialbudgetNovalue)
                                      .WithSqlParam("date_value", formattedDate == null ? "" : formattedDate)
                                      .WithSqlParam("project_job_name", projectJobName == null ? "" : projectJobName)
                                      .ExecuteStoredProc<spProjectBudgetLineDetailsModel>();

                var budgetLineList = spbudgetLineList.Select(b => new spProjectBudgetLineDetailsModel
                {
                    BudgetLineId = b.BudgetLineId,
                    BudgetCode = b.BudgetCode,
                    BudgetName = b.BudgetName,
                    InitialBudget = b.InitialBudget,
                    ProjectId = b.ProjectId,
                    ProjectJobId = b.ProjectJobId,
                    ProjectJobName = b.ProjectJobName,
                    ProjectJobCode = b.ProjectJobCode,
                    CurrencyId = b.CurrencyId,
                    CurrencyName = b.CurrencyName,
                    DebitPercentage = b.DebitPercentage,
                    CreatedDate = b.CreatedDate,
                    Expenditure = b.Expenditure
                }).Skip(request.pageSize.Value * request.pageIndex.Value)
                                      .Take(request.pageSize.Value)
                                      .ToList();

                int totalCount = spbudgetLineList.Count();
                response.data.spProjectBudgetLineList = budgetLineList.OrderByDescending(x => x.DebitPercentage).ToList();
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
