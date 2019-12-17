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

namespace HumanitarianAssistance.Application.Project.Queries
{
        public class GetLogisticSupplierListQueryHandler : IRequestHandler<GetLogisticSupplierListQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;

            public GetLogisticSupplierListQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<ApiResponse> Handle(GetLogisticSupplierListQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                var logisticsList = await _dbContext.ProjectLogisticSuppliers
                                                               .Where(x => x.IsDeleted == false && x.LogisticRequestsId == request.requestId)
                                                               .Select(x => new LogisticSupplierViewModel
                                                                {
                                                                   SupplierId = x.SupplierId,
                                                                   //SupplierName = x.SupplierName,
                                                                   Quantity = x.Quantity,
                                                                   //FinalPrice = x.FinalPrice
                                                                })
                                                                .ToListAsync();

                response.data.LogisticsSupplierList = logisticsList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
