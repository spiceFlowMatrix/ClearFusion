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
using HumanitarianAssistance.Common.Enums;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllLogisticRequestQueryHandler : IRequestHandler<GetAllLogisticRequestQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllLogisticRequestQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetAllLogisticRequestQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var reqlist = _dbContext.ProjectLogisticRequests
                .Where(x=>x.IsDeleted==false && x.ProjectId==request.ProjectId)
                .AsQueryable();
                
                var count = await reqlist.CountAsync();
                var list = await reqlist
                .Select(y=>new LogisticsRequestsModel{
                    RequestId = y.LogisticRequestsId,
                    TotalCost = y.TotalCost,
                    RequestCode = "REQ" + y.LogisticRequestsId,
                    BudgetLine = y.ProjectBudgetLineDetail.BudgetCode,
                    Currency = y.CurrencyDetails.CurrencyCode,
                    Office = y.OfficeDetail.OfficeName,
                    ProcessingType = getProcessingType(y.TotalCost, y.Status ,y.ComparativeStatus)["ProcessingType"],
                    Status = getProcessingType(y.TotalCost, y.Status ,y.ComparativeStatus)["Status"]
                })
                .OrderByDescending(x => x.RequestId).Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToListAsync();
                
                response.data.TotalCount = count;
                response.data.logisticRequestList = list;
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

        private Dictionary<string,string> getProcessingType(double totalCost, int Status,int ComparativeStatus) 
        {
            Dictionary<string,string> obj = new Dictionary<string,string>();
            
            if(totalCost < 200) {
                obj.Add("Status", ((LogisticRequestStatus)Status).GetDescription());
                obj.Add("ProcessingType", "Direct Purchase");
            }
            else if((totalCost >= 200) && (totalCost < 10000)) {
                obj.Add("Status", ((LogisticComparativeStatus)ComparativeStatus).GetDescription());
                obj.Add("ProcessingType", "Comparative Statement");
            } 
            else if((totalCost >= 10000) && (totalCost < 60000)) {
                obj.Add("Status", ((LogisticComparativeStatus)ComparativeStatus).GetDescription());
                obj.Add("ProcessingType", "Local Tender");
            }
            else if(totalCost >= 60000) {
                 obj.Add("Status", ((LogisticComparativeStatus)ComparativeStatus).GetDescription());
                 obj.Add("ProcessingType", "International Tender");
            }
            else 
            {
                obj.Add("Status", "");
                obj.Add("ProcessingType", "");
            }

            return obj;
        }

    }
}
