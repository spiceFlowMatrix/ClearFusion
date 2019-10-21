using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetPermissionByRoleIdQueryHandler : IRequestHandler<GetPermissionByRoleIdQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetPermissionByRoleIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetPermissionByRoleIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var permissionlist = (from pir in await _dbContext.PermissionsInRoles.ToListAsync()
                                      join p in await _dbContext.Permissions.ToListAsync() on pir.PermissionId equals p.Id
                                      where pir.RoleId == request.RoleId && pir.IsGrant
                                      select new PermissionsInRolesModel
                                      {
                                          PermissionId = pir.PermissionId,
                                          PermissionName = p.Name
                                      }).ToList();
                response.data.PermissionsInRolesList = permissionlist.ToList();
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