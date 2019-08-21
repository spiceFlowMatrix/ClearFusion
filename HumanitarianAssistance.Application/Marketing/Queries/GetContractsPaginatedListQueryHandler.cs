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

    public class GetContractsPaginatedListQueryHandler : IRequestHandler<GetContractsPaginatedListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetContractsPaginatedListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetContractsPaginatedListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var list = await _dbContext.ContractDetails.Where(x => x.IsDeleted == false).Skip(request.pageSize * request.pageIndex).Take(request.pageSize).ToListAsync();
                response.data.ContractDetails = list;
                response.StatusCode = 200;
                response.data.jobListTotalCount = _dbContext.ContractDetails.Count(x => x.IsDeleted == false);
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
