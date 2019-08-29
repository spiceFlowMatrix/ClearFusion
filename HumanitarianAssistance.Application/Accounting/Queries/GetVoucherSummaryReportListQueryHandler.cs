using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetVoucherSummaryReportListQueryHandler : IRequestHandler<GetVoucherSummaryReportListQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetVoucherSummaryReportListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetVoucherSummaryReportListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var spVoucherSummaryList = await _dbContext.LoadStoredProc("get_vouchersummaryreportvouchersbyfilter")
                                      .WithSqlParam("accounts", request.Accounts)
                                      .WithSqlParam("budgetlines", request.BudgetLines)
                                      .WithSqlParam("currencyid", request.Currency)
                                      .WithSqlParam("journals", request.Journals)
                                      .WithSqlParam("offices", request.Offices)
                                      .WithSqlParam("projectjobs", request.ProjectJobs)
                                      .WithSqlParam("projects", request.Projects)
                                      .WithSqlParam("recordtype", request.RecordType)
                                      .ExecuteStoredProc<SPVoucherSummaryReportModel>();


                var summaryList = spVoucherSummaryList.Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToList();

                response.ResponseData = summaryList;
                response.data.TotalCount = spVoucherSummaryList.Count;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception exception)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = exception.Message;
            }

            return response;
        }
    }
}