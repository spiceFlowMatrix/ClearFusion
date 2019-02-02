using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces.Marketing
{
    public interface IPolicyService
    {
        Task<APIResponse> AddEditPolicy(PolicyModel model, string UserId);
        Task<APIResponse> GetAllPolicyList();
        Task<APIResponse> DeletePolicy(int model, string UserId);
    }
}
