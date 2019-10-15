using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using HumanitarianAssistance.Application.Project.Models;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectJobsByMultipleBudgetLineIdsQueryHandler : IRequestHandler<GetProjectJobsByMultipleBudgetLineIdsQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetProjectJobsByMultipleBudgetLineIdsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetProjectJobsByMultipleBudgetLineIdsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var JobDetailList = await _dbContext.ProjectBudgetLineDetail.Where(x=>x.IsDeleted==false && request.budgetLineIds.Contains(x.BudgetLineId))
                                    .Select(x => new ProjectJobDetailModel
                                    {
                                        ProjectId=x.ProjectId,
                                        ProjectJobName=x.ProjectJobDetail.ProjectJobName,
                                        ProjectJobCode = x.ProjectJobDetail.ProjectJobCode,
                                        ProjectJobId = x.ProjectJobId,
                                    })
                                    .ToListAsync();
                response.data.ProjectJobDetailModel = JobDetailList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";

            }
            catch(Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}