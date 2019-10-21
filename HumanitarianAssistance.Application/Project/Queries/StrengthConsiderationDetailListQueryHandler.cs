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
    public class StrengthConsiderationDetailListQueryHandler: IRequestHandler<StrengthConsiderationDetailListQuery, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;

        public StrengthConsiderationDetailListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(StrengthConsiderationDetailListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var list = await _dbContext.StrengthConsiderationDetail.Where(x => !x.IsDeleted).ToListAsync();
                response.data.StrengthConsiderationDetail = list;
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