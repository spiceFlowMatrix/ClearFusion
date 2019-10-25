using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface ITaskAndActivity
    {
        Task<APIResponse> AddTask(TaskMasterModel model);
        Task<APIResponse> EditTask(TaskMasterModel model);
        Task<APIResponse> GetAllTask(int projectid);
        Task<APIResponse> AddActivityDetail(ActivityMasterModel model);
        Task<APIResponse> EditActivityDetail(ActivityMasterModel model);
        Task<APIResponse> GetAllActivity();
        Task<APIResponse> AddAssignActivityDetail(AssignActivityModel model);
        Task<APIResponse> GetAllAssignActivityDetailByCondition(long? ProjectId, int? TaskId, int? ActivityId);
    }
}
