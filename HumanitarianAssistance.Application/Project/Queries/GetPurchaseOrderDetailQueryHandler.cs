using System.Threading;
using System;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.Project.Models;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetPurchaseOrderDetailQueryHandler: IRequestHandler<GetPurchaseOrderDetailQuery, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;
        public GetPurchaseOrderDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetPurchaseOrderDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try 
            {
                var _logisticReq = await _dbContext.ProjectLogisticRequests
                                .Where(x => x.IsDeleted == false && x.LogisticRequestsId == request.requestId)
                                .Select(x=>new PurchaseOrderDetailModel{
                                    PurchasedDate = x.PurchaseDate,
                                    Currency = x.CurrencyDetails.CurrencyCode,
                                    Office = x.OfficeDetail.OfficeName,
                                    OfficeId = x.OfficeId,
                                    BudgetLine = x.ProjectBudgetLineDetail.BudgetCode,
                                    ProjectJob = x.ProjectBudgetLineDetail.ProjectJobDetail.ProjectJobCode,
                                    Project = x.ProjectDetail.ProjectCode,
                                    TotalCost = _dbContext.ProjectLogisticItems.Where(y=>y.IsDeleted == false && y.LogisticRequestsId == request.requestId)
                                    .Select(y=>Convert.ToDouble(y.Quantity * y.FinalCost))
                                    .DefaultIfEmpty(0)
                                    .Sum()
                                })
                                .FirstOrDefaultAsync();
                    
                response.data.purchaseOrderDetail = _logisticReq;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch(Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message =ex.Message;
            }
            return response;

        }
    }
}