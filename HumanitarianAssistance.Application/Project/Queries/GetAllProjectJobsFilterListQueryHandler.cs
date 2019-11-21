using System;
using System.Collections.Generic;
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
                                                                     .Select(x => new ProjectJobDetailModel
                                                                     {
                                                                         ProjectJobId = x.ProjectJobId,
                                                                         ProjectJobCode = x.ProjectJobCode,
                                                                         ProjectJobName = x.ProjectJobName,
                                                                         ProjectId = x.ProjectId,
                                                                     }

                                                                     )
                                                                     .Skip(request.PageSize.Value * request.PageIndex.Value)
                                                                     .Take(request.PageSize.Value)
                                                                     .ToListAsync();


               
                List<long?> ProjectJobIdList = list.Select(y => y.ProjectJobId).ToList();
                // all id 
                List<long?> selectedJobId = _dbContext.ProjectBudgetLineDetail
                                                        .Where(x => x.IsDeleted == false &&
                                                                    x.ProjectId == request.ProjectId &&
                                                                    x.ProjectJobId != null)
                                                        .Select(y => y.ProjectJobId)
                                                        .ToList();

                if (selectedJobId.Any())
                {
                    // to get common indicator id which are selected in budgetLine 
                    ProjectJobIdList = ProjectJobIdList.Intersect(selectedJobId).ToList();
 
                    foreach (var item in ProjectJobIdList)
                    {
                        var findIndex = list.FindIndex(x => x.ProjectJobId == item);
                        list[findIndex].CanDelete = true;
                    }
                }
                response.data.ProjectJobDetailModel = list;
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
