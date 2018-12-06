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
    }
}
