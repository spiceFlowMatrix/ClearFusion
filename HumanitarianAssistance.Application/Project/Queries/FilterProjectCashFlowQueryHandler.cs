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
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class FilterProjectCashFlowQueryHandler : IRequestHandler<FilterProjectCashFlowQuery, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public FilterProjectCashFlowQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(FilterProjectCashFlowQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            List<VoucherTransactions> transList = new List<VoucherTransactions>();

            try
            {

                //get Project Cashflow Report from sp get_projectcashflow by passing parameters
                var spProjectCashFlow = await _dbContext.LoadStoredProc("get_projectcashflow")
                                      .WithSqlParam("currency", request.CurrencyId)
                                      .WithSqlParam("projectid", request.ProjectId)
                                      .WithSqlParam("startdate", request.ProjectStartDate.ToString())
                                      .WithSqlParam("enddate", request.ProjectEndDate.ToString())
                                      .WithSqlParam("donorid", request.DonorID)
                                      .ExecuteStoredProc<SPProjectCashFlowModel>();

                if (spProjectCashFlow.Any())
                {
                    response.data.ProjectCashFlowModel = new ProjectCashFlowModel();
                    response.data.ProjectCashFlowModel.Expenditure = new List<double>();
                    response.data.ProjectCashFlowModel.Income = new List<double>();
                    response.data.ProjectCashFlowModel.Date = new List<DateTime>();
                    response.data.ProjectCashFlowModel.TotalExpectedBudget = new List<double?>();

                    var projectsExpectedBudget = await _dbContext.LoadStoredProc("get_totalexpectedprojectbudget")
                                     .WithSqlParam("currencyid", request.CurrencyId)
                                     .WithSqlParam("projectid", request.ProjectId)
                                     .WithSqlParam("comparisiondate", DateTime.UtcNow.ToString())
                                     .ExecuteStoredProc<ProjectExpectedBudget>();

                    double? totalExpectedBudget = 0.0;

                    if (projectsExpectedBudget.Any())
                    {
                        totalExpectedBudget = projectsExpectedBudget.FirstOrDefault().TotalExpectedProjectBudget;
                    }

                    DateTime startDate = request.ProjectStartDate;
                    DateTime endDate = request.ProjectEndDate;

                    List<DateTime> regularIntervalDates = AccountingUtility.GetRegularIntervalDates(startDate, endDate, 6);

                    if (regularIntervalDates != null && regularIntervalDates.Any())
                    {
                        foreach (var item in regularIntervalDates)
                        {
                            response.data.ProjectCashFlowModel.TotalExpectedBudget.Add(totalExpectedBudget);
                            response.data.ProjectCashFlowModel.Expenditure.Add(spProjectCashFlow.Where(x => x.VoucherDate <= item).Sum(x => x.Expenditure));
                            response.data.ProjectCashFlowModel.Income.Add(spProjectCashFlow.Where(x => x.BudgetLineDate <= item).Sum(x => x.Income));
                            response.data.ProjectCashFlowModel.Date.Add(item);
                        }
                    }
                }

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
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
