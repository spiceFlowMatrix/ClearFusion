using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models.Project;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces.ProjectManagement
{
    public interface IProjectPeopleService
    {
        Task<APIResponse> GetOpportunityControlList(long projectId);
        Task<APIResponse> AddOpportunityControl(OpportunityControlAddModel model, string userId);
        Task<APIResponse> EditOpportunityControl(OpportunityControlEditModel model, string userId);
        Task<APIResponse> DeleteOpportunityControl(long opportunityControlId, string userId);

        Task<APIResponse> GetLogisticsControlList(long projectId);
        Task<APIResponse> AddLogisticsControl(LogisticsControlAddModel model, string userId);
        Task<APIResponse> EditLogisticsControl(LogisticsControlEditModel model, string userId);
        Task<APIResponse> DeleteLogisticsControl(long logisticsControlId, string userId);


        Task<APIResponse> GetActivitiesControlList(long projectId);
        Task<APIResponse> AddActivitiesControl(ActivitiesControlAddModel model, string userId);
        Task<APIResponse> EditActivitiesControl(ActivitiesControlEditModel model, string userId);
        Task<APIResponse> DeleteActivitiesControl(long activitiesControlId, string userId);


        Task<APIResponse> GetHiringControlList(long projectId);
        Task<APIResponse> AddHiringControl(HiringControlAddModel model, string userId);
        Task<APIResponse> EditHiringControl(HiringControlEditModel model, string userId);
        Task<APIResponse> DeleteHiringControl(long hiringControlId, string userId);

    }
}
