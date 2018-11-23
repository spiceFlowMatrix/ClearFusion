using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IClientDetails
    {
        #region Client
        Task<APIResponse> GetClientDetailsById(int ClientId, string UserId);
        Task<APIResponse> AddEditClientDetails(ClientDetailModel model, string UserId);
        Task<APIResponse> EditClientDetails(ClientDetailModel model, string UserId);
        Task<APIResponse> GetAllClient();
        Task<APIResponse> DeleteClientDetails(int model, string UserId);
        #endregion

        #region Category
        Task<APIResponse> GetAllCategory();
        Task<APIResponse> AddCategory(CategoryModel model, string UserId);
        Task<APIResponse> EditCategory(CategoryModel model, string UserId);
        #endregion
        
    }
}
