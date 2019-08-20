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
        public class GetallProjectActivityDetailQueryHandler : IRequestHandler<GetallProjectActivityDetailQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            public GetallProjectActivityDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<ApiResponse> Handle(GetallProjectActivityDetailQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {

                var activityList = await _dbContext.ProjectActivityDetail.AsNoTracking()
                                          .Where(v => v.IsDeleted == false &&
                                                      v.ParentId == null &&
                                                      v.ProjectBudgetLineDetail.ProjectId == request.ProjectId
                                          )
                                          .OrderByDescending(x => x.ActivityId)
                                          .Select(b => new ProjectActivityModel
                                          {

                                              ActivityId = b.ActivityId,
                                              ActivityName = b.ActivityName,
                                              ActivityDescription = b.ActivityDescription,
                                              BudgetLineId = b.ProjectBudgetLineDetail.BudgetLineId,
                                              BudgetName = b.ProjectBudgetLineDetail.BudgetName,
                                              EmployeeID = b.EmployeeDetail.EmployeeID,
                                              EmployeeName = b.EmployeeDetail.EmployeeName,
                                              StatusId = b.ActivityStatusDetail.StatusId,
                                              StatusName = b.ActivityStatusDetail.StatusName,
                                              PlannedStartDate = b.PlannedStartDate,
                                              PlannedEndDate = b.PlannedEndDate,
                                              ActualStartDate = b.ActualStartDate,
                                              ActualEndDate = b.ActualStartDate,
                                              Recurring = b.Recurring,
                                              RecurringCount = b.RecurringCount,
                                              RecurrinTypeId = b.RecurrinTypeId                                             
                                          })
                                          .ToListAsync();

                response.data.ProjectActivityList = activityList;
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
