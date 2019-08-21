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
    public class GetAllProjectJobDetailQueryHandler : IRequestHandler<GetAllProjectJobDetailQuery, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllProjectJobDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllProjectJobDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {

                var list = await _dbContext.ProjectJobDetail.AsNoTracking().Where(x => x.IsDeleted == false)
                                                                             .OrderByDescending(x => x.ProjectJobId)
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
