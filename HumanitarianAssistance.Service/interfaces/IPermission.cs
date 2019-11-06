using DataAccess.DbEntities;
using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IPermissions
    {
        Task<APIResponses.APIResponse> GetPermissionsById(string id);
        Task<APIResponses.APIResponse> AddPermission(PermissionsModel model);
        Task<APIResponses.APIResponse> GetPermissionsByRoleId(string roleId);
     
    }

}
