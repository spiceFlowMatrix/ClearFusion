using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllProjectJobByProjectIdQueryHandler : IRequestHandler<GetAllProjectJobByProjectIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAllProjectJobByProjectIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllProjectJobByProjectIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request.ProjectJobId != 0)
                {
                    var JobDertailList = await _dbContext.ProjectJobDetail.Where(x => x.IsDeleted == false &&
                                                                                               x.ProjectJobId == request.ProjectJobId)
                                                                .OrderBy(x => x.ProjectJobId)
                                                                .Select(x => new ProjectJobDetailModel
                                                                {
                                                                    ProjectId = x.ProjectId,
                                                                    ProjectJobName = x.ProjectJobName,
                                                                    ProjectJobCode = x.ProjectJobCode,
                                                                    ProjectJobId = x.ProjectJobId,
                                                                }).ToListAsync();


                    response.data.ProjectJobDetailModel = JobDertailList;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.notFoundCode;
                }
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
