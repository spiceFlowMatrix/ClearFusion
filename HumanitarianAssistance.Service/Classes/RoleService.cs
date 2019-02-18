using HumanitarianAssistance.Service.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using System.Threading.Tasks;

using HumanitarianAssistance.Common.Helpers;
using System.Security.Claims;
using AutoMapper;
using System.Linq;

namespace HumanitarianAssistance.Service.Classes
{
    public class RoleService : IRole
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private APIResponse response;
        private List<Roles> _roles;
        private readonly IMapper _mapper;
        public RoleService(RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            response = new APIResponse();
            _roles = new List<Roles>();
            
        }

        public async Task<APIResponse> CreateRoleAsync(RoleViewModel model)
        {
            try
            {
                bool IsRoleExist = await _roleManager.RoleExistsAsync(model.RoleName);
                if (!IsRoleExist)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = model.RoleName;
                    await _roleManager.CreateAsync(role);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.RoleCreated;
                }
                else
                {
                    response.Message = StaticResource.RoleAlreadyExist;
                    response.StatusCode = StaticResource.failStatusCode;

                }
            }
            catch
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong;

            }
            return response;
        }

        public async Task<APIResponse> CreateRoleClaimAsync(RoleClaimViewModel model)
        {
            try
            {
                var adminRole = await _roleManager.FindByNameAsync(model.RoleName);
                if (adminRole != null)
                {
                    foreach (var i in model.ClaimValues)
                    {

                        await _roleManager.AddClaimAsync(adminRole, new Claim(i.ClaimType, i.ClaimValue));
                    }
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.ClaimAddedToRole;
                    return response;

                }
                else
                {
                    response.Message = StaticResource.InvalidRole;
                    response.StatusCode = StaticResource.failStatusCode;
                }

            }
            catch
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong;
            }
            return response;
        }

        public async Task<APIResponse> GetRoleClaims(string roleId)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(roleId);
            var roleClaims = await _roleManager.GetClaimsAsync(role);
            // List<RoleClaimViewModel> roleClaimData = _mapper.Map<List<RoleClaimViewModel>>(roleClaims);
            // response.data.roleClaimsList = roleClaimData;
            return response;
        }

        public async Task<APIResponse> DeleteRole(string Id)
        {
            try
            {
                IdentityRole role = await _roleManager.FindByIdAsync(Id);
                if (role != null)
                {
                    await _roleManager.DeleteAsync(role);
                    response.Message = StaticResource.RoleDeleted;
                    response.StatusCode = StaticResource.successStatusCode;
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.RoleCannotDeleted;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong;
            }
            return response;
        }

        public async Task<APIResponse> EditRole(string Id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(Id);
                
                RoleViewModel roleData = _mapper.Map<RoleViewModel>(role);
                response.data.RoleData = roleData;
                response.StatusCode = StaticResource.successStatusCode;
            }
            catch
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong;
            }
            return response;
        }

        public async Task<APIResponse> UpdateRole(EditRoleViewModel model)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(model.Id);
                if (role != null)
                {
                    role.Name = model.RoleName;
                    role.NormalizedName = model.RoleName.ToUpper();
                    await _roleManager.UpdateAsync(role);
                    // await _roleManager.UpdateNormalizedRoleNameAsync(role);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.RoleUpdated;
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.RoleNotUpdated;
                }
            }
            catch
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong;
            }
            return response;
        }

        public APIResponse GetRoles()
        {
            try
            {
                var roles = _roleManager.Roles.Where(x => x.Name.ToLower() != "superadmin").ToList();
                 //_roleManager.Roles.
                _roles = _mapper.Map<List<Roles>>(roles);
                response.data.RoleList = _roles;
                response.StatusCode = StaticResource.successStatusCode;
            }
            catch
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong;
            }
            return response;
        }
    }
}
