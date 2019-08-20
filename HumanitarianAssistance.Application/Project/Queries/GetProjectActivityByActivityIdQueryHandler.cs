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
    public class GetProjectActivityByActivityIdQueryHandler : IRequestHandler<GetProjectActivityByActivityIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetProjectActivityByActivityIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetProjectActivityByActivityIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var activityDetail = await _dbContext.ProjectActivityDetail
                                                              .Include(x => x.ProjectBudgetLineDetail)
                                                              .Include(x => x.EmployeeDetail)
                                                              .Include(x => x.ActivityStatusDetail)
                                                              .Include(x => x.ProjectActivityProvinceDetail)
                                                              .Include(x => x.ProjectActivityProvinceDetail)
                                                              .FirstOrDefaultAsync(v => v.IsDeleted == false &&
                                                                                  v.ParentId == null &&
                                                                                  v.ActivityId == request.activityId
                                                              );
                ProjectActivityModel obj = new ProjectActivityModel();

                if (activityDetail != null)
                {
                    obj.ActivityId = activityDetail.ActivityId;
                    obj.ActivityName = activityDetail.ActivityName;
                    obj.ActivityDescription = activityDetail.ActivityDescription;
                    obj.BudgetLineId = activityDetail.ProjectBudgetLineDetail.BudgetLineId;
                    obj.BudgetName = activityDetail.ProjectBudgetLineDetail.BudgetName;
                    obj.EmployeeID = activityDetail.EmployeeDetail.EmployeeID;
                    obj.EmployeeName = activityDetail.EmployeeDetail.EmployeeName;
                    obj.StatusId = activityDetail.ActivityStatusDetail.StatusId;
                    obj.StatusName = activityDetail.ActivityStatusDetail.StatusName;
                    obj.PlannedStartDate = activityDetail.PlannedStartDate;
                    obj.PlannedEndDate = activityDetail.PlannedEndDate;
                    obj.Recurring = activityDetail.Recurring;
                    obj.RecurringCount = activityDetail.RecurringCount;
                    obj.RecurrinTypeId = activityDetail.RecurrinTypeId;
                    obj.ActualStartDate = activityDetail.ActualStartDate;
                    obj.ActualEndDate = activityDetail.ActualEndDate;
                    obj.ProvinceId = activityDetail.ProjectActivityProvinceDetail.Select(x => x.ProvinceId);
                    obj.DistrictID = activityDetail.ProjectActivityProvinceDetail.Select(x => x.DistrictID);
                }

                response.data.ProjectActivityDetails = obj;
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
