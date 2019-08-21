using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Commands.Update
{
    public class UpdatePermissionsOnSelectedRoleCommandHandler : IRequestHandler<UpdatePermissionsOnSelectedRoleCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UpdatePermissionsOnSelectedRoleCommandHandler(HumanitarianAssistanceDbContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        public async Task<ApiResponse> Handle(UpdatePermissionsOnSelectedRoleCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {

                // UpdateRole
                IdentityResult identityResult = new IdentityResult();

                var roleExists = await _roleManager.FindByIdAsync(request.RoleId);

                if (roleExists != null)
                {
                    roleExists.Name = request.RoleName;
                    identityResult = await _roleManager.UpdateAsync(roleExists);
                }
                if (!identityResult.Succeeded)
                {
                    throw new Exception(StaticResource.SomethingWentWrong);
                }


                // update permission
                if (request != null)
                {
                    //get all permissions that exists for the role
                    List<RolePermissions> rolePermissionsList = await _dbContext.RolePermissions.Where(x => x.IsDeleted == false && x.RoleId == request.RoleId).ToListAsync();
                    List<RolePermissions> removedPermissions = rolePermissionsList.Where(x => !request.Permissions.Select(y => y.PageId).Contains(x.PageId.Value)).ToList();
                    List<ApproveRejectPermission> approveRejectRolePermissionsList = await _dbContext.ApproveRejectPermission.Where(x => x.IsDeleted == false && x.RoleId == request.RoleId).ToListAsync();
                    List<AgreeDisagreePermission> agreeDisagreeRolePermissionsList = await _dbContext.AgreeDisagreePermission.Where(x => x.IsDeleted == false && x.RoleId == request.RoleId).ToListAsync();
                    List<OrderSchedulePermission> orderScheduleRolePermissionsList = await _dbContext.OrderSchedulePermission.Where(x => x.IsDeleted == false && x.RoleId == request.RoleId).ToListAsync();
                    List<ApproveRejectPermission> approveRejectRemovePermissions = approveRejectRolePermissionsList.Where(x => !request.Permissions.Select(y => y.PageId).Contains(x.PageId)).ToList();
                    List<AgreeDisagreePermission> agreeDisagreeRemovePermissions = agreeDisagreeRolePermissionsList.Where(x => !request.Permissions.Select(y => y.PageId).Contains(x.PageId)).ToList();
                    List<OrderSchedulePermission> orderScheduleRemovePermissions = orderScheduleRolePermissionsList.Where(x => !request.Permissions.Select(y => y.PageId).Contains(x.PageId)).ToList();
                    removedPermissions.ForEach(x => x.IsDeleted = true);
                    approveRejectRemovePermissions.ForEach(x => x.IsDeleted = true);
                    agreeDisagreeRemovePermissions.ForEach(x => x.IsDeleted = true);
                    orderScheduleRemovePermissions.ForEach(x => x.IsDeleted = true);
                    _dbContext.RolePermissions.UpdateRange(removedPermissions);
                    _dbContext.ApproveRejectPermission.UpdateRange(approveRejectRemovePermissions);
                    _dbContext.AgreeDisagreePermission.UpdateRange(agreeDisagreeRemovePermissions);
                    _dbContext.OrderSchedulePermission.UpdateRange(orderScheduleRemovePermissions);
                    _dbContext.SaveChanges();

                    foreach (ApplicationPagesModel item in request.Permissions)
                    {
                        if (item.View == true || item.Edit == true)
                        {
                            //get the previous permission set for the pageId if exists
                            RolePermissions rolePermissions = rolePermissionsList.FirstOrDefault(x => x.PageId == item.PageId);

                            //If permission for the page does not exist then initialize object
                            rolePermissions = rolePermissions ?? new RolePermissions();

                            rolePermissions.CanEdit = item.Edit;
                            rolePermissions.CanView = item.View;
                            rolePermissions.CreatedDate = rolePermissions.CreatedDate ?? DateTime.Now;
                            rolePermissions.IsDeleted = false;
                            rolePermissions.PageId = item.PageId;
                            rolePermissions.RoleId = request.RoleId;
                            rolePermissions.ModuleId = item.ModuleId;

                            //save a new entry in the rolepermissions table
                            if (rolePermissions.RolesPermissionId == 0)
                            {
                                _dbContext.RolePermissions.Add(rolePermissions);
                                _dbContext.SaveChanges();
                                _dbContext.Entry<RolePermissions>(rolePermissions).State = EntityState.Detached;
                            }
                            else//update existing permissions record for the page
                            {
                                rolePermissions.ModifiedDate = DateTime.Now;
                                _dbContext.RolePermissions.Update(rolePermissions);
                                _dbContext.SaveChanges();
                                _dbContext.Entry<RolePermissions>(rolePermissions).State = EntityState.Detached;
                            }
                        }
                        if (item.Approve == true || item.Reject == true)
                        {
                            ApproveRejectPermission approveRejectRolePermissions = approveRejectRolePermissionsList.FirstOrDefault(x => x.PageId == item.PageId);

                            //If permission for the page does not exist then initialize object
                            approveRejectRolePermissions = approveRejectRolePermissions ?? new ApproveRejectPermission();
                            approveRejectRolePermissions.Approve = item.Approve;
                            approveRejectRolePermissions.Reject = item.Reject;
                            approveRejectRolePermissions.CreatedDate = approveRejectRolePermissions.CreatedDate ?? DateTime.UtcNow;
                            approveRejectRolePermissions.IsDeleted = false;
                            approveRejectRolePermissions.PageId = item.PageId;
                            approveRejectRolePermissions.RoleId = request.RoleId;

                            //save a new entry in the rolepermissions table
                            if (approveRejectRolePermissions.Id == 0)
                            {
                                _dbContext.ApproveRejectPermission.Add(approveRejectRolePermissions);
                                _dbContext.SaveChanges();
                                _dbContext.Entry<ApproveRejectPermission>(approveRejectRolePermissions).State = EntityState.Detached;
                            }
                            else//update existing permissions record for the page
                            {
                                approveRejectRolePermissions.ModifiedDate = DateTime.Now;
                                _dbContext.ApproveRejectPermission.Update(approveRejectRolePermissions);
                                _dbContext.SaveChanges();
                                _dbContext.Entry<ApproveRejectPermission>(approveRejectRolePermissions).State = EntityState.Detached;
                            }
                        }
                        if (item.Agree == true || item.Disagree == true)
                        {
                            AgreeDisagreePermission agreeDisagreeRolePermissions = agreeDisagreeRolePermissionsList.FirstOrDefault(x => x.PageId == item.PageId);

                            //If permission for the page does not exist then initialize object
                            agreeDisagreeRolePermissions = agreeDisagreeRolePermissions ?? new AgreeDisagreePermission();
                            agreeDisagreeRolePermissions.Agree = item.Agree;
                            agreeDisagreeRolePermissions.Disagree = item.Disagree;
                            agreeDisagreeRolePermissions.CreatedDate = agreeDisagreeRolePermissions.CreatedDate ?? DateTime.UtcNow;
                            agreeDisagreeRolePermissions.IsDeleted = false;
                            agreeDisagreeRolePermissions.PageId = item.PageId;
                            agreeDisagreeRolePermissions.RoleId = request.RoleId;

                            //save a new entry in the rolepermissions table
                            if (agreeDisagreeRolePermissions.Id == 0)
                            {
                                _dbContext.AgreeDisagreePermission.Add(agreeDisagreeRolePermissions);
                                _dbContext.SaveChanges();
                                _dbContext.Entry<AgreeDisagreePermission>(agreeDisagreeRolePermissions).State = EntityState.Detached;
                            }
                            else//update existing permissions record for the page
                            {
                                agreeDisagreeRolePermissions.ModifiedDate = DateTime.Now;
                                _dbContext.AgreeDisagreePermission.Update(agreeDisagreeRolePermissions);
                                _dbContext.SaveChanges();
                                _dbContext.Entry<AgreeDisagreePermission>(agreeDisagreeRolePermissions).State = EntityState.Detached;
                            }
                        }
                        OrderSchedulePermission orderScheduleRolePermissions = orderScheduleRolePermissionsList.FirstOrDefault(x => x.PageId == item.PageId);

                        //If permission for the page does not exist then initialize object
                        orderScheduleRolePermissions = orderScheduleRolePermissions ?? new OrderSchedulePermission();
                        orderScheduleRolePermissions.OrderSchedule = item.OrderSchedule;
                        orderScheduleRolePermissions.CreatedDate = orderScheduleRolePermissions.CreatedDate ?? DateTime.UtcNow;
                        orderScheduleRolePermissions.IsDeleted = false;
                        orderScheduleRolePermissions.PageId = item.PageId;
                        orderScheduleRolePermissions.RoleId = request.RoleId;

                        //save a new entry in the rolepermissions table
                        if (orderScheduleRolePermissions.Id == 0)
                        {
                            _dbContext.OrderSchedulePermission.Add(orderScheduleRolePermissions);
                            _dbContext.SaveChanges();
                            _dbContext.Entry<OrderSchedulePermission>(orderScheduleRolePermissions).State = EntityState.Detached;
                        }
                        else//update existing permissions record for the page
                        {
                            orderScheduleRolePermissions.ModifiedDate = DateTime.Now;
                            _dbContext.OrderSchedulePermission.Update(orderScheduleRolePermissions);
                            _dbContext.SaveChanges();
                            _dbContext.Entry<OrderSchedulePermission>(orderScheduleRolePermissions).State = EntityState.Detached;
                        }
                    }

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
                response.Message = ex.Message;

            }
            return response;
        }
    }
}