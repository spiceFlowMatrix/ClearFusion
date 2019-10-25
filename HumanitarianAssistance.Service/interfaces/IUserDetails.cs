using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
   public interface IUserDetails
    {
        Task<APIResponses.APIResponse> AddUser(UserDetailsModel model);
        Task<APIResponse> EditUser(UserDetailsModel model);
        Task<APIResponse> GetDepartmentsByOfficeCodeAsyn(string officeCode);
        Task<APIResponse> GetAllUserDetails();
        Task<APIResponse> GetUserDetailsByUserId(string UserId);
        Task<APIResponse> ChangePassword(ChangePasswordModel model);
        Task<APIResponse> ResetPassword(ResetPassword model);
        Task<APIResponse> GetUserRolesByUserId(string userid);
        Task<APIResponse> GetAllUserList();


    }
}
