using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectWinLossStatusQueryHandler: IRequestHandler<GetProjectWinLossStatusQuery, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;
        public GetProjectWinLossStatusQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetProjectWinLossStatusQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            var projectDetail = await _dbContext.WinProjectDetails
                                                .FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId);
            if (projectDetail != null)
            {
                response.data.ProjectWinLoss = projectDetail.IsWin ?? false;
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