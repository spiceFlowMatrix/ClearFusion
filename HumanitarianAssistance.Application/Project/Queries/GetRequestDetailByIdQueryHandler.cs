using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using HumanitarianAssistance.Application.Project.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetRequestDetailByIdQueryHandler: IRequestHandler<GetRequestDetailByIdQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetRequestDetailByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetRequestDetailByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var req = await _dbContext.ProjectLogisticRequests.Where(x=>x.IsDeleted==false && x.LogisticRequestsId==request.RequestId).Select(y=>new LogisticsRequestsDetailModel{
                    RequestId = y.LogisticRequestsId,
                    ProjectId = y.ProjectId,
                    Description = y.Description,
                    Status = y.Status,
                    TotalCost = y.TotalCost,
                    BudgetLine = y.ProjectBudgetLineDetail.BudgetCode,
                    Currency = y.CurrencyDetails.CurrencyCode,
                    Office = y.OfficeDetail.OfficeName,
                    ComparativeStatus = y.ComparativeStatus,
                    TenderStatus = y.TenderStatus,
                    BudgetLineId = y.ProjectBudgetLineDetail.BudgetLineId,
                    CurrencyId = y.CurrencyDetails.CurrencyId,
                    OfficeId = y.OfficeDetail.OfficeId
                }).FirstOrDefaultAsync();
                
                if(req == null) {
                    throw new Exception("Request doesnot exist!");
                }

                response.data.logisticRequest = req;
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