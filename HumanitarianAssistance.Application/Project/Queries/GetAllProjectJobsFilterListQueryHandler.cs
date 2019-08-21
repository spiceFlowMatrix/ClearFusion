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
    public class GetAllProjectJobsFilterListQueryHandler : IRequestHandler<GetAllProjectJobsFilterListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAllProjectJobsFilterListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllProjectJobsFilterListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {

                int totalCount = await _dbContext.ProjectJobDetail.Where(x => x.IsDeleted == false &&
                                                                                       x.ProjectId == request.ProjectId)
                                                                                 .CountAsync();

                var list = await _dbContext.ProjectJobDetail.Where(x => x.IsDeleted == false &&
                                                                                 x.ProjectId == request.ProjectId)
                                                                     .OrderByDescending(x => x.ProjectJobId)
                                                                     .Skip(request.PageSize.Value * request.PageIndex.Value)
                                                                     .Take(request.PageSize.Value)
                                                                     .ToListAsync();
                response.data.ProjectJobDetail = list;
                response.data.TotalCount = totalCount;
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
