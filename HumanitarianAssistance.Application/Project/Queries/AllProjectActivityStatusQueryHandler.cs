using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class AllProjectActivityStatusQueryHandler : IRequestHandler<AllProjectActivityStatusQuery, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public AllProjectActivityStatusQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(AllProjectActivityStatusQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                //Task<DateTime?> minStartDate = GetMinimumProjectActivityStartDate(projectId);
                //Task<DateTime?> maxEndDate = GetMaximumProjectActivityEndDate(projectId);

                //Task<int> ActivityOnSchedule = GetActivityOnSchedule(projectId);
                //Task<int> LateStart = GetLateStart(projectId);
                //Task<int> LateEnd = GetLateEnd(projectId);
                //Task<float> Progress = GetProgress(projectId);
                //Task<int> Slippage = GetSlippage(projectId);

                //DateTime minDate = await minStartDate ?? DateTime.UtcNow;
                //DateTime maxDate = await maxEndDate ?? DateTime.UtcNow;

                //ProjectActivityStatusModel obj = new ProjectActivityStatusModel
                //{
                //    ProjectDuration = (maxDate.Date - minDate.Date).Days,
                //    ActivityOnSchedule = await ActivityOnSchedule,
                //    LateStart = await LateStart,
                //    LateEnd = await LateEnd,
                //    Progress = await Progress,
                //    Slippage = await Slippage,
                //};

                ProjectActivityStatusModel obj = new ProjectActivityStatusModel
                {
                    ProjectDuration = 0,
                    ActivityOnSchedule = 0,
                    LateStart = 0,
                    LateEnd = 0,
                    Progress = 0,
                    Slippage = 0
                };

                response.data.ProjectActivityStatusModel = obj;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
                await Task.Delay(0);
            }
            return response;     
        }
    }
}
