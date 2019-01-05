using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Service.Classes
{
    public class PermissionsInRolesService : IPermissionsInRoles
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public PermissionsInRolesService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<APIResponse> AddPermissionsInRoles(List<PermissionsInRolesModel> model, string RoleId)
        {
            APIResponse response = new APIResponse();
            List<PermissionsInRoles> list = new List<PermissionsInRoles>();
            try
            {
                var notexitlist1 = await _uow.PermissionsInRolesRepository.FindAllAsync(x => x.RoleId == RoleId);
                foreach (var v in notexitlist1)
                {
                    v.IsGrant = false;
                    v.ModifiedById = model[0].CreatedById;
                    v.ModifiedDate = DateTime.UtcNow;
                    await _uow.PermissionsInRolesRepository.UpdateAsyn(v, v.RoleId, v.PermissionId);
                }


                if (model.Any())
                {
                    var roleId = model[0].RoleId;

                    var existPermissionsInRoles = await _uow.GetDbContext().PermissionsInRoles.AnyAsync(x => x.RoleId == roleId);

                    //Edit
                    if (existPermissionsInRoles)
                    {
                        var existingPermission = await _uow.PermissionsInRolesRepository.FindAllAsync(x => x.RoleId == roleId);
                        foreach (var i in existingPermission)
                        {
                            await _uow.PermissionsInRolesRepository.DeleteAsyn(i);
                        }
                    }

                    for (int i = 0; i < model.Count; i++)
                    {
                        PermissionsInRoles p = _mapper.Map<PermissionsInRoles>(model[i]);
                        p.IsGrant = true;
                        p.RoleId = model[0].RoleId;
                        await _uow.PermissionsInRolesRepository.AddAsyn(p);
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "New Permission In Role Added";
                    }
                }

                //for (int i = 0; i < model.Count; i++)
                //{

                //    var existPermissionsInRoles = await _uow.PermissionsInRolesRepository.FindAsync(x => x.PermissionId == model[i].PermissionId && x.RoleId == model[i].RoleId && x.IsDeleted == false);
                //    if (existPermissionsInRoles == null)
                //    {
                //        PermissionsInRoles p = _mapper.Map<PermissionsInRoles>(model[i]);
                //        p.IsGrant = true;
                //        await _uow.PermissionsInRolesRepository.AddAsyn(p);
                //        response.StatusCode = StaticResource.successStatusCode;
                //        response.Message = "New Permission In Role Added";
                //    }
                //    else
                //    {
                //        existPermissionsInRoles.PermissionId = model[i].PermissionId;
                //        existPermissionsInRoles.IsGrant = true;
                //        existPermissionsInRoles.ModifiedById = model[i].CreatedById;
                //        existPermissionsInRoles.ModifiedDate = DateTime.UtcNow;

                //        await _uow.PermissionsInRolesRepository.UpdateAsyn(existPermissionsInRoles, existPermissionsInRoles.RoleId, existPermissionsInRoles.PermissionId);

                //        response.StatusCode = StaticResource.successStatusCode;
                //        response.Message = "Permission Updated";
                //    }
                //}


            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetPermissionByRoleId(string roleid)
        {
            APIResponse response = new APIResponse();
            try
            {
                var permissionlist = (from pir in await _uow.PermissionsInRolesRepository.GetAllAsyn()
                                      join p in await _uow.PermissionRepository.GetAllAsyn() on pir.PermissionId equals p.Id
                                      where pir.RoleId == roleid && pir.IsGrant
                                      select new PermissionsInRolesModel
                                      {
                                          PermissionId = pir.PermissionId,
                                          PermissionName = p.Name
                                      }).ToList();
                response.data.PermissionsInRolesList = permissionlist.ToList();
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

        public async Task<APIResponse> GetPermissionsAsync()
        {
            APIResponse response = new APIResponse();
            try
            {
                await Task.Run(() =>
                {
                    IList<PermissionsModel> list = _uow.PermissionRepository
                    .GetAll().Where(x => x.IsDeleted == false)
                    .Select(x => new PermissionsModel { Name = x.Name, Id = x.Id }).ToList();
                    response.data.PermissionsList = list;
                }
                );

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SomethingWentWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Get All Pages that are in use in Application
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetAllApplicationPages()
        {
            APIResponse response = new APIResponse();

            try
            {
                List<ApplicationPages> applicationPagesList = await _uow.GetDbContext().ApplicationPages.Where(x => x.IsDeleted == false).ToListAsync();

                if (applicationPagesList.Any())
                {
                    response.data.ApplicationPagesList = applicationPagesList;
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

        /// <summary>
        /// Add Role With Page Permissions
        /// </summary>
        /// <param name="rolesWithPagePermissionsModel"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddRoleWithPagePermissions(RolesWithPagePermissionsModel rolesWithPagePermissionsModel, string RoleId)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (rolesWithPagePermissionsModel !=null)
                {
                   List<RolePermissions> rolePermissionsList = new List<RolePermissions>();

                    foreach (ApplicationPagesModel item in rolesWithPagePermissionsModel.Permissions)
                    {
                        RolePermissions rolePermissions = new RolePermissions();
                        rolePermissions.CanEdit = item.Edit;
                        rolePermissions.CanView = item.View;
                        rolePermissions.CreatedDate = DateTime.Now;
                        rolePermissions.IsDeleted = false;
                        rolePermissions.PageId = item.PageId;
                        rolePermissions.RoleId = RoleId;
                        rolePermissions.ModuleId = item.ModuleId;
                        _uow.GetDbContext().RolePermissions.Add(rolePermissions);
                        _uow.GetDbContext().SaveChanges();
                        _uow.GetDbContext().Entry<RolePermissions>(rolePermissions).State = EntityState.Detached;
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
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Get Permissions For Selected Role
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns>Permissions List</returns>
        public async Task<APIResponse> GetPermissionsOnSelectedRole(string RoleId)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (!string.IsNullOrEmpty(RoleId))
                {
                    List<RolePermissionViewModel> rolePermissionsList = new List<RolePermissionViewModel>();

                    rolePermissionsList = await _uow.GetDbContext().RolePermissions
                                                                   .Where(x => x.IsDeleted == false && x.RoleId == RoleId)
                                                                   .Select(x=> new RolePermissionViewModel {
                                                                       Edit= x.CanEdit,
                                                                       View= x.CanView,
                                                                       CurrentPermissionId= x.CurrentPermissionId,
                                                                       IsGrant= x.IsGrant,
                                                                       ModuleId= x.ModuleId,
                                                                       PageId= x.PageId,
                                                                       RoleId= x.RoleId,
                                                                       RolesPermissionId= x.RolesPermissionId,
                                                                       PageName= x.ApplicationPages.PageName

                                                                   }) .ToListAsync();

                    response.data.PermissionsInRole = rolePermissionsList;
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
        
        /// <summary>
        /// Update the permissions for role
        /// </summary>
        /// <param name="rolesWithPagePermissionsModel"></param>
        /// <returns>Success/Failure</returns>
        public async Task<APIResponse> UpdatePermissionsOnSelectedRole(RolesWithPagePermissionsModel rolesWithPagePermissionsModel)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (rolesWithPagePermissionsModel != null)
                {
                    //get all permissions that exists for the role
                    List<RolePermissions> rolePermissionsList = await _uow.GetDbContext().RolePermissions.Where(x=> x.IsDeleted== false && x.RoleId== rolesWithPagePermissionsModel.RoleId).ToListAsync();

                    foreach (ApplicationPagesModel item in rolesWithPagePermissionsModel.Permissions)
                    {
                        //get the previous permission set for the pageId if exists
                        RolePermissions rolePermissions = rolePermissionsList.FirstOrDefault(x => x.PageId == item.PageId);

                        //If permission for the page does not exist then initialize object
                        rolePermissions = rolePermissions ?? new RolePermissions() ;

                        rolePermissions.CanEdit = item.Edit;
                        rolePermissions.CanView = item.View;
                        rolePermissions.CreatedDate = rolePermissions.CreatedDate?? DateTime.Now;
                        rolePermissions.IsDeleted = false;
                        rolePermissions.PageId = item.PageId;
                        rolePermissions.RoleId = rolesWithPagePermissionsModel.RoleId;
                        rolePermissions.ModuleId = item.ModuleId;

                        //save a new entry in the rolepermissions table
                        if (rolePermissions.RolesPermissionId==0)
                        {
                            _uow.GetDbContext().RolePermissions.Add(rolePermissions);
                            _uow.GetDbContext().SaveChanges();
                            _uow.GetDbContext().Entry<RolePermissions>(rolePermissions).State = EntityState.Detached;
                        }
                        else//update existing permissions record for the page
                        {
                            rolePermissions.ModifiedDate = DateTime.Now;
                            _uow.GetDbContext().RolePermissions.Update(rolePermissions);
                            _uow.GetDbContext().SaveChanges();
                            _uow.GetDbContext().Entry<RolePermissions>(rolePermissions).State = EntityState.Detached;
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
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

    }
}
