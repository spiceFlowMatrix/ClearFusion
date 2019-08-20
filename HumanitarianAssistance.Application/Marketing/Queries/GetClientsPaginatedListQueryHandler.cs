using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetClientsPaginatedListQueryHandler : IRequestHandler<GetClientsPaginatedListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetClientsPaginatedListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
         
        public async Task<ApiResponse> Handle(GetClientsPaginatedListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var list = _dbContext.ClientDetails.Where(x => x.IsDeleted == false).Skip((request.pageSize * request.pageIndex)).Take(request.pageSize).ToList();
                response.data.ClientDetails = list.OrderByDescending(x => x.ClientId).ToList();
                response.StatusCode = 200;
                response.Message = "Success";
                response.data.TotalCount = await _dbContext.ClientDetails.CountAsync(x => x.IsDeleted == false);
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
