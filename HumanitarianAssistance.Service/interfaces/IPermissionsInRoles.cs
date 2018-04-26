using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IPermissionsInRoles
    {
        Task<APIResponses.APIResponse> AddPermissionsInRoles(List<PermissionsInRolesModel> model , string RoleId);
        Task<APIResponses.APIResponse> GetPermissionByRoleId(string roleid);
        Task<APIResponse> GetPermissionsAsync();
        

    }
}
