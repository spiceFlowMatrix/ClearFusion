using System;
using System.Collections.Generic;
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
    public class GetPermissionsOnSelectedRoleQueryHandler : IRequestHandler<GetPermissionsOnSelectedRoleQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetPermissionsOnSelectedRoleQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetPermissionsOnSelectedRoleQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (!string.IsNullOrEmpty(request.RoleId))
                {
                    List<RolePermissionViewModel> rolePermissionsList = new List<RolePermissionViewModel>();

                    rolePermissionsList = await _dbContext.RolePermissions
                                                                   .Where(x => x.IsDeleted == false && x.RoleId == request.RoleId)
                                                                   .Select(x => new RolePermissionViewModel
                                                                   {
                                                                       Edit = x.CanEdit,
                                                                       View = x.CanView,
                                                                       CurrentPermissionId = x.CurrentPermissionId,
                                                                       IsGrant = x.IsGrant,
                                                                       ModuleId = x.ModuleId,
                                                                       PageId = x.PageId,
                                                                       RoleId = x.RoleId,
                                                                       RolesPermissionId = x.RolesPermissionId,
                                                                       PageName = x.ApplicationPages.PageName

                                                                   }).ToListAsync();
                    List<ApproveRejectPermissionModel> approveRejectPermissionList = (from ur in _dbContext.ApproveRejectPermission
                                                                                      join at in _dbContext.ApplicationPages on ur.PageId equals at.PageId
                                                                                      where !ur.IsDeleted && !at.IsDeleted && ur.RoleId == request.RoleId
                                                                                      select (new ApproveRejectPermissionModel
                                                                                      {
                                                                                          Approve = ur.Approve,
                                                                                          Reject = ur.Reject,
                                                                                          Id = ur.Id,
                                                                                          PageId = ur.PageId,
                                                                                          RoleId = request.RoleId,
                                                                                          PageName = at.PageName
                                                                                      })).ToList();
                    List<AgreeDisagreePermissionModel> agreeDisagreePermissionList = (from ur in _dbContext.AgreeDisagreePermission
                                                                                      join at in _dbContext.ApplicationPages on ur.PageId equals at.PageId
                                                                                      where !ur.IsDeleted && !at.IsDeleted && ur.RoleId == request.RoleId
                                                                                      select (new AgreeDisagreePermissionModel
                                                                                      {
                                                                                          Agree = ur.Agree,
                                                                                          Disagree = ur.Disagree,
                                                                                          Id = ur.Id,
                                                                                          PageId = ur.PageId,
                                                                                          RoleId = request.RoleId,
                                                                                          PageName = at.PageName
                                                                                      })).ToList();
                    List<OrderSchedulePermissionModel> orderSchedulePermissionList = (from ur in _dbContext.OrderSchedulePermission
                                                                                      join at in _dbContext.ApplicationPages on ur.PageId equals at.PageId
                                                                                      where !ur.IsDeleted && !at.IsDeleted && ur.RoleId == request.RoleId
                                                                                      select (new OrderSchedulePermissionModel
                                                                                      {
                                                                                          OrderSchedule = ur.OrderSchedule,
                                                                                          Id = ur.Id,
                                                                                          PageId = ur.PageId,
                                                                                          RoleId = request.RoleId,
                                                                                          PageName = at.PageName
                                                                                      })).ToList();

                    response.data.OrderSchedulePermissionsInRole = orderSchedulePermissionList;
                    response.data.AgreeDisagreePermissionsInRole = agreeDisagreePermissionList;
                    response.data.PermissionsInRole = rolePermissionsList;
                    response.data.ApproveRejectPermissionsInRole = approveRejectPermissionList;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.NoDataFound;
                }
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