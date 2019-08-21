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
        public class GetActivitiesControlListQueryHandler : IRequestHandler<GetActivitiesControlListQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;

            public GetActivitiesControlListQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<ApiResponse> Handle(GetActivitiesControlListQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                var activitiesList = await _dbContext.ProjectActivitiesControl
                                                               .Where(x => x.IsDeleted == false && x.ProjectId == request.projectId)
                                                               .Select(x => new ActivitiesControlViewModel
                                                               {
                                                                   Id = x.Id,
                                                                   ProjectId = x.ProjectId,
                                                                   RoleId = x.RoleId,
                                                                   UserId = x.UserID,
                                                                   DateAdded = x.CreatedDate ?? DateTime.UtcNow
                                                               })
                                                               .ToListAsync();

                response.data.ActivitiesControlList = activitiesList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
