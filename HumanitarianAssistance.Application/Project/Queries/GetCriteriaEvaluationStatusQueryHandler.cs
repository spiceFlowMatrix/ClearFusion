using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetCriteriaEvaluationStatusQueryHandler : IRequestHandler<GetIsApprovedCriteriaEvaluationStatusQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetCriteriaEvaluationStatusQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetIsApprovedCriteriaEvaluationStatusQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            ProjectDetail projectDetail = await _dbContext.ProjectDetail
                                                        .FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId &&
                                                                                  x.IsDeleted == false);
            if (projectDetail != null)
            {
                response.data.IsApprovedCriteriaEvaluation = projectDetail.IsCriteriaEvaluationSubmit ?? false;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            else
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.projectNotFoundText;
            }

            return response;
        }
    }
}
