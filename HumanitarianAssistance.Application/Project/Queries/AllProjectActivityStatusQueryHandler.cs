using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class AllProjectActivityStatusQueryHandler : IRequestHandler<AllProjectActivityStatusQuery, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;

        public AllProjectActivityStatusQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(AllProjectActivityStatusQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                // Task<DateTime?> minStartDate = GetMinimumProjectActivityStartDate(request.ProjectId);
                //  Task<DateTime?> maxEndDate = GetMaximumProjectActivityEndDate(request.ProjectId);

                Task<int> ActivityOnSchedule = GetActivityOnSchedule(request.ProjectId);
                //Task<int> LateStart = GetLateStart(request.ProjectId);
                // Task<int> LateEnd = GetLateEnd(request.ProjectId);
                //  Task<float> Progress = GetProgress(request.ProjectId);
                // Task<int> Slippage = GetSlippage(request.ProjectId);

                //  DateTime minDate = await minStartDate ?? DateTime.UtcNow;
                // DateTime maxDate = await maxEndDate ?? DateTime.UtcNow;

                // ProjectActivityStatusModel obj = new ProjectActivityStatusModel
                // {
                //    // ProjectDuration = (maxDate.Date - minDate.Date).Days,
                //     ActivityOnSchedule = await ActivityOnSchedule,
                //     // LateStart = await LateStart,
                //     // LateEnd = await LateEnd,
                //     LateStart = 0,
                //     LateEnd = 0,
                //     // ActivityOnSchedule = 0,
                //   //  Progress = await Progress,
                //    // Slippage = await Slippage,
                // };

                ProjectActivityStatusModel obj = new ProjectActivityStatusModel
                {
                    ProjectDuration = 0,
                    // ActivityOnSchedule = 0,
                    ActivityOnSchedule = await ActivityOnSchedule,
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

        public async Task<DateTime?> GetMinimumProjectActivityStartDate(long projectId)
        {
            return await _dbContext.ProjectActivityDetail.Where(x => x.ProjectId == projectId &&
                                                                              x.ParentId == null &&
                                                                              x.IsDeleted == false)
                                                                  .MinAsync(x => x.PlannedStartDate);
        }

        public async Task<DateTime?> GetMaximumProjectActivityEndDate(long projectId)
        {
            return await _dbContext.ProjectActivityDetail.Where(x => x.ProjectId == projectId &&
                                                                              x.ParentId == null &&
                                                                              x.IsDeleted == false)
                                                                  .MaxAsync(x => x.PlannedEndDate);
        }

        public async Task<int> GetActivityOnSchedule(long projectId)
        {
            var subActivityList = await _dbContext.ProjectActivityDetail.Where(x => x.IsDeleted == false && x.ProjectId == projectId).ToListAsync();
            Console.WriteLine(subActivityList);
            int totalCount = await _dbContext.ProjectActivityDetail
                                                      .CountAsync(a => a.IsDeleted == false &&
                                                                       a.ProjectBudgetLineDetail.ProjectId == projectId &&
                                                                       a.ParentId == null &&
                                                                       a.PlannedStartDate ==
                                                                       (a.ProjectSubActivityList.Min(y => y.ActualStartDate.Value.Date) != null ?
                                                                            a.ProjectSubActivityList.Min(y => y.ActualStartDate.Value.Date) : DateTime.UtcNow.Date) &&
                                                                       a.PlannedEndDate >= (a.ProjectSubActivityList.Max(y => y.ActualEndDate.Value.Date) != null ?
                                                                            a.ProjectSubActivityList.Max(y => y.ActualEndDate.Value.Date) : DateTime.UtcNow.Date)
                  
                                                                       );
            Console.WriteLine(totalCount);
            return totalCount;

        }
        // public async Task<int> GetLateStart(long projectId)
        // {
        //     //NOTE: PlannedStart < ActualStartDate
        //     int totalCount = await _dbContext.ProjectActivityDetail.CountAsync(a => a.IsDeleted == false &&
        //                                                                           a.ProjectId == projectId &&
        //                                                                           a.ParentId == null &&
        //                                                                           a.PlannedStartDate.Value.Date < (a.ProjectSubActivityList.Min(x => x.ActualStartDate.Value.Date) != null ?
        //                                                                                                            a.ProjectSubActivityList.Min(x => x.ActualStartDate.Value.Date) : DateTime.UtcNow.Date)
        //                                                                       );
        //     return totalCount;
        // }

        // public async Task<int> GetLateEnd(long projectId)
        // {
        //     //NOTE: PlannedEndDate < ActualEndDate
        //     int totalCount = await _dbContext.ProjectActivityDetail.CountAsync(a => a.IsDeleted == false &&
        //                                                                            a.ProjectId == projectId &&
        //                                                                            a.ParentId == null &&
        //                                                                            a.PlannedEndDate.Value.Date < (a.ProjectSubActivityList.Min(x => x.ActualEndDate.Value.Date) != null ?
        //                                                                                                           a.ProjectSubActivityList.Min(x => x.ActualEndDate.Value.Date) : DateTime.UtcNow.Date)
        //                                                                       );
        //     return totalCount;
        // }
        public async Task<float> GetProgress(long projectId)
        {
            float avg = 0;

            Task<long> totalProjectsTask = _dbContext.ProjectActivityDetail
                                                    .LongCountAsync(a => a.IsDeleted == false &&
                                                                         a.ProjectId == projectId &&
                                                                         a.ParentId == null);
            Task<long> completedProjectsTask = _dbContext.ProjectActivityDetail
                                                    .LongCountAsync(a => a.IsDeleted == false &&
                                                                         a.ProjectId == projectId &&
                                                                         a.ParentId == null &&
                                                                         a.StatusId == (int)ProjectPhaseType.Completed);
            long totalProjects = await totalProjectsTask;
            long completedProjects = await completedProjectsTask;
            if (totalProjects == 0 || completedProjects == 0)
            {
                avg = 0;
            }
            else
            {
                //Note: Here typecasting is important, else it will always return 0 
                avg = (float)completedProjects / totalProjects * 100;
            }
            return avg;
        }

        public async Task<int> GetSlippage(long projectId)
        {
            // NOTE: PlannedEndDate - ActualEndDate
            int slippage = await _dbContext.ProjectActivityDetail.CountAsync(a => a.IsDeleted == false &&
                                                                                      a.ProjectId == projectId &&
                                                                                      a.ParentId == null
                                                                                      &&
                                                                                      (a.ActualEndDate != null ? a.ActualEndDate.Value.Date : DateTime.UtcNow.Date) >
                                                                                      (a.PlannedEndDate != null ? a.PlannedEndDate.Value.Date : DateTime.UtcNow.Date)
                                                                                      );
            return slippage;
        }
    }
}
