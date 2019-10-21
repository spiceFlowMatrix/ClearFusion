using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Helpers;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetAllClientQueryHandler : IRequestHandler<GetAllClientQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAllClientQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext; 
        }
        public async Task<ApiResponse> Handle(GetAllClientQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var list = await _dbContext.ClientDetails.Where(x => !x.IsDeleted).ToListAsync();
                //updated list order By AS 07/06/2019 
                response.data.ClientDetails = list.OrderByDescending(x => x.ClientId).ToList();
                response.StatusCode = 200;
                response.data.jobListTotalCount = await _dbContext.ClientDetails.CountAsync(x => x.IsDeleted == false);
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
