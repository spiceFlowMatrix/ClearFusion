using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
   public interface IRole
    {
        Task<APIResponse> CreateRoleClaimAsync(RoleClaimViewModel model);
        Task<APIResponse> EditRole(string Id);
        Task<APIResponse> DeleteRole(string Id);
        Task<APIResponse> CreateRoleAsync(RoleViewModel model);
        Task<APIResponse> UpdateRole(EditRoleViewModel model);
        APIResponse GetRoles();

    }
}
