using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Domain.Entities.Project;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Helpers;
using System;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetSecurityConsiderationDetailQueryHandler: IRequestHandler<GetSecurityConsiderationDetailQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetSecurityConsiderationDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetSecurityConsiderationDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var SecurityConsiderationDetail =
                     await _dbContext.SecurityConsiderationDetail.Where(x => x.IsDeleted == false).Select(x => new SecurityConsiderationDetail
                     {
                         SecurityConsiderationId = x.SecurityConsiderationId,
                         SecurityConsiderationName = x.SecurityConsiderationName
                     })
                     .OrderBy(x => x.SecurityConsiderationName)
                     .ToListAsync();
                
                response.data.SecurityConsiderationDetail = SecurityConsiderationDetail;
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