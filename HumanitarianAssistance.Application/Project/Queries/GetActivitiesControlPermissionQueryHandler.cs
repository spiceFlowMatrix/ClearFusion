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
using HumanitarianAssistance.Domain.Entities.Project;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Queries
{
        public class GetActivitiesControlPermissionQueryHandler : IRequestHandler<GetActivitiesControlPermissionQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;

            public GetActivitiesControlPermissionQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<ApiResponse> Handle(GetActivitiesControlPermissionQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                var userDetail = await _dbContext.UserDetails
                                           .FirstOrDefaultAsync(x => x.AspNetUserId == request.userId && x.IsDeleted == false);

                if (userDetail == null)
                {
                    throw new Exception(StaticResource.UserNotExist);
                }

                ICollection<ProjectActivitiesControl> permissions = await _dbContext.ProjectActivitiesControl
                                                                                .Where(x => x.ProjectId == request.projectId &&
                                                                                                   x.UserID == userDetail.UserID &&
                                                                                                   x.IsDeleted == false).ToListAsync();

                List<ProjectActivityPermissionModel> permissionList = permissions.Select(x => new ProjectActivityPermissionModel
                {
                    Id = x.Id,
                    ProjectId = x.ProjectId,
                    RoleId = x.RoleId,
                    UserId = x.UserID,
                    DateAdded = x.CreatedDate
                }).ToList();


                response.data.ProjectActivityPermissionList = permissionList;
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
