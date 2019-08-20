using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectProposalReportQueryHandler : IRequestHandler<GetProjectProposalReportQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetProjectProposalReportQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetProjectProposalReportQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            List<SPProjectProposalReportModel> proposalReport = new List<SPProjectProposalReportModel>();

            try
            {

                string startDate = request.StartDate == null ? string.Empty : request.StartDate.ToString();
                string dueDate = request.DueDate == null ? string.Empty : request.DueDate.ToString();

                //get Project Proposal Report from sp get_projectproposalreport by passing parameters
                var spProposalReport = await _dbContext.LoadStoredProc("get_projectproposalreport")
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
                                      .ExecuteStoredProc<SPProjectProposalReportModel>();

                var total = spProposalReport.Count();

                response.data.TotalCount = total;
                response.data.ProjectProposalReportList = spProposalReport.Skip(request.PageIndex.Value * request.PageSize.Value).Take(request.PageSize.Value).ToList();
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
