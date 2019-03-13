using HumanitarianAssistance.Service.APIResponses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces.Marketing
{
    public interface ISchedulerService
    {
        Task<APIResponse> GetAllPolicyScheduleList();
        Task<APIResponse> GetScheduleDetailsById(int Id);
    }
}
