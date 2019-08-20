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
using System.Collections.Generic;
using HumanitarianAssistance.Persistence.Extensions;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Accounting;

namespace HumanitarianAssistance.Application.Project.Queries
{

    public class GetProjectProposalAmountSummaryQueryHandler : IRequestHandler<GetProjectProposalAmountSummaryQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetProjectProposalAmountSummaryQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetProjectProposalAmountSummaryQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            List<ProjectProposalAmountSummary> projectProposalAmountSummary = new List<ProjectProposalAmountSummary>();

            try
            {

                string startDate = request.StartDate == null ? string.Empty : request.StartDate.ToString();
                string dueDate = request.DueDate == null ? string.Empty : request.DueDate.ToString();

                //get GetProjectProposalAmountSummary from sp get_projectproposalreport by passing parameters
                var spAmountSummaryInCommonCurrency = await _dbContext.LoadStoredProc("get_projectproposalreportamountsummary")
                                      .WithSqlParam("projectname", request.ProjectName)
                                      .WithSqlParam("startdate", startDate)
                                      .WithSqlParam("enddate", dueDate)
                                      .WithSqlParam("startdatefilteroption", request.StartDateFilterOption)
                                      .WithSqlParam("duedatefilteroption", request.DueDateFilterOption)
                                      .WithSqlParam("currencyid", request.CurrencyId)
                                      .WithSqlParam("amount", request.Amount)
                                      .WithSqlParam("amountfilteroption", request.AmountFilterOption)
                                      .WithSqlParam("iscompleted", request.IsCompleted)
                                      .WithSqlParam("islate", request.IsLate)
                                      .ExecuteStoredProc<SPProjectProposalReportAmountSummaryModel>();

                var currencyTask = _dbContext.CurrencyDetails.ToListAsync();

                if (spAmountSummaryInCommonCurrency.Any())
                {

                    int amountSummaryCurrencyId = spAmountSummaryInCommonCurrency.FirstOrDefault().ProjectCurrency;
                    double totalAmount = spAmountSummaryInCommonCurrency.Sum(x => x.ProjectAmount);

                    List<CurrencyDetails> currencies = await currencyTask;

                    foreach (CurrencyDetails currency in currencies)
                    {
                        ExchangeRateDetail exchangeRate = await _dbContext.ExchangeRateDetail.OrderByDescending(x => x.Date).Where(x => x.FromCurrency == amountSummaryCurrencyId && x.ToCurrency == currency.CurrencyId).FirstOrDefaultAsync();

                        if (exchangeRate != null)
                        {
                            ProjectProposalAmountSummary amountSummary = new ProjectProposalAmountSummary
                            {
                                CurrencyId = currency.CurrencyId,
                                ProposalAmount = totalAmount * (double)exchangeRate.Rate
                            };
                            projectProposalAmountSummary.Add(amountSummary);
                        }
                        else
                        {
                            throw new Exception("Exchange Rate not defined");
                        }
                    }

                    response.data.ProjectProposalAmountSummary = projectProposalAmountSummary;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex;
            }

            return response;
        }
    }
}
