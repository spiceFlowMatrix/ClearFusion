using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models.Project;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces.ProjectManagement
{
   public interface IHiringRequestService
    {
        Task<APIResponse> AddProjectHiringRequest(ProjectHiringRequestModel model, string userId);
        Task<APIResponse> GetallHiringRequestDetail();
        Task<APIResponse> EditProjectHiringRequest(ProjectHiringRequestModel model, string userId);

    }
}
