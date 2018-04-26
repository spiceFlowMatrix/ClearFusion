using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes
{
    public class PermissionService : IPermissions
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public PermissionService(IUnitOfWork uow, IMapper mapper)
        {
            this._uow = uow;
            this._mapper = mapper;
            
        }

       

        public async Task<APIResponse> AddPermission(PermissionsModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                Permissions p=_mapper.Map<Permissions>(model);
                
                await _uow.PermissionRepository.AddAsyn(p);
                await _uow.PermissionRepository.SaveAsync();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "New Permission Added";
            
            }
            catch(Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong +ex.Message;
            }

            return response;

        }

        public Task<APIResponse> GetPermissionsById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<APIResponse> GetPermissionsByRoleId(string roleId)
        {
            throw new NotImplementedException();
        }
    }
}
