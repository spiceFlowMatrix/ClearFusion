using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models.Project;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
   public interface IProjectActivityService
    {
        Task<APIResponse> GetallProjectActivityDetail();
        Task<APIResponse> AddProjectActivityDetail(ProjectActivityModel model, string UserId);
        Task<APIResponse> EditProjectActivityDetail(ProjectActivityModel model, string UserId);
        Task<APIResponse> DeleteProjectActivity(long activityId, string userId);
    }
}
