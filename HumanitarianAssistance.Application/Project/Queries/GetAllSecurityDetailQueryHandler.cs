using System.Linq;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Domain.Entities.Project;
using MediatR;
using HumanitarianAssistance.Common.Helpers;
using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllSecurityDetailQueryHandler: IRequestHandler<GetAllSecurityDetailQuery, ApiResponse>
    {
        
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllSecurityDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllSecurityDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var SecurityDetail = await _dbContext.SecurityDetail
                                               .Where(x => x.IsDeleted == false).Select(x => new SecurityDetail
                                                {
                                                    SecurityId = x.SecurityId,
                                                    SecurityName = x.SecurityName
                                                })
                                                .OrderBy(x => x.SecurityName)
                                                .ToListAsync();

                response.data.SecurityDetail = SecurityDetail;
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