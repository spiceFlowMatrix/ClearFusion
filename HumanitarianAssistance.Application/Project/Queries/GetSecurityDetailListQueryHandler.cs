using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetSecurityDetailListQueryHandler: IRequestHandler<GetSecurityDetailListQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        
        public GetSecurityDetailListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(GetSecurityDetailListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var list = await _dbContext.SecurityDetail.Where(x => !x.IsDeleted).ToListAsync();
                response.data.SecurityDetail = list;
                response.StatusCode = 200;
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