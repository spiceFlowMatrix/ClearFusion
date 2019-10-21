using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectJobsByMultipleProjectIdsQueryHandler : IRequestHandler<GetProjectJobsByMultipleProjectIdsQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;

            public GetProjectJobsByMultipleProjectIdsQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<ApiResponse> Handle(GetProjectJobsByMultipleProjectIdsQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                var list = await _dbContext.ProjectJobDetail.Where(x => x.IsDeleted == false &&
                                                                                 request.projectIds.Contains(x.ProjectId))
                                                                     .OrderByDescending(x => x.ProjectJobName)
                                                                     .ToListAsync();
                response.data.ProjectJobDetail = list;
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
