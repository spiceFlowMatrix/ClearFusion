using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces.Marketing
{
    public interface IJobDetailsService
    {
        Task<APIResponse> GetAllJobDetails();
        Task<APIResponse> AddJobDetails(JobDetailsModel model, string UserId);
        Task<APIResponse> EditJobDetails(JobDetailsModel model, string UserId);
        Task<APIResponse> DeleteJobDetail(JobDetails model);
        Task<APIResponse> AddEditJobDetail(JobDetailsModel model, string UserId);
    }
}
