using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces.Marketing
{
    public interface ISchedulerService
    {
        Task<APIResponse> GetAllPolicyScheduleList();
        Task<APIResponse> GetAllScheduleList();
        Task<APIResponse> FilterScheduleList(FilterSchedulerModel model);
        Task<APIResponse> GetScheduleDetailsById(int model);
        Task<APIResponse> AddEditSchedule(SchedulerModel model, string userId);
        Task<APIResponse> DeleteSchedule(int model, string userId);
    }
}
