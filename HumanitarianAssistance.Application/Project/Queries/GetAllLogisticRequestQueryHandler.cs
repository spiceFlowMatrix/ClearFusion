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
                var reqlist = _dbContext.ProjectLogisticRequests.Where(x=>x.IsDeleted==false && x.ProjectId==request.ProjectId)
                .Select(y=>new LogisticsRequestsModel{
                    RequestId = y.LogisticRequestsId,
                    ProjectId = y.ProjectId,
                    RequestName = y.RequestName,
                    Status = y.Status,
                    //TotalCost = y.TotalCost
                }).AsQueryable();
                
                var count = reqlist.Count();
                var list = await reqlist.OrderByDescending(x => x.RequestId).Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToListAsync();
                foreach(var item in list) {
                    item.TotalCost =   _dbContext.ProjectLogisticItems.Where(x=>x.IsDeleted==false && x.LogisticRequestsId==item.RequestId).Sum(y=>y.EstimatedCost);
                }
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
    }
}
