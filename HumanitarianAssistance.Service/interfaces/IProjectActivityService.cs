using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models.Project;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IProjectActivityService
    {
        Task<APIResponse> GetallProjectActivityDetail(long projectId);
        Task<APIResponse> GetProjectActivityAdvanceFilterList(ActivityAdvanceFilterModel model);

        Task<APIResponse> AddProjectActivityDetail(ProjectActivityModel model, string UserId);
        Task<APIResponse> EditProjectActivityDetail(ProjectActivityModel model, string UserId);
        Task<APIResponse> DeleteProjectActivity(long activityId, string userId);
        //Task<APIResponse> StartProjectActivity(long activityId, string UserId);
        //Task<APIResponse> EndProjectActivity(long activityId, string UserId);
        //Task<APIResponse> MarkImplementationAsCompleted(long activityId, string UserId);
        //Task<APIResponse> MarkMonitoringAsCompleted(long activityId, string UserId);
        Task<APIResponse> AllProjectActivityStatus(long projectId);
        Task<APIResponse> UploadProjectActivityDocumentFile(IFormFile file, string UserId, long activityId, string fileName, string logginUserEmailId, string ext, int statusID);
        Task<APIResponse> GetUploadedDocument(long activityId);
        Task<APIResponse> UploadFileDemo(IFormFile file, string UserId, string userName);
        Task<APIResponse> DeleteActivityDocument(long activityDocumentId, string userId);
        Task<APIResponse> GetProjectSubActivityDetails(int parentId);
        Task<APIResponse> AddProjectSubActivityDetail(ProjectActivityModel model, string UserId);
        Task<APIResponse> EditProjectSubActivityDetail(ProjectSubActivityListModel model, string UserId);
        Task<APIResponse> ProjectSubActivityIscomplete(long activityId, string UserId);
        Task<APIResponse> StartProjectSubActivity(long activityId, string UserId);
        Task<APIResponse> EndProjectSubActivity(long activityId, string UserId);

        Task<APIResponse> GetProjectActivityByActivityId(long activityId, string UserId);




        Task<APIResponse> AddProjectMonitoringReview(ProjectMonitoringViewModel model, string UserId);
        Task<APIResponse> GetProjectMonitoringList(long activityId);
        
        Task<APIResponse> GetProjectActivityExtension(long activityId);
        Task<APIResponse> AddProjectActivityExtension(ProjectExtensionModel model, string userId);
        Task<APIResponse> EditProjectActivityExtension(ProjectExtensionModel model, string userId);
        Task<APIResponse> DeleteProjectActivityExtension(long extensionId, string userId);
        Task<APIResponse> GetProjectMonitoringByMonitoringId(long Id);
        Task<APIResponse> EditProjectMonitoringByMonitoringId(ProjectMonitoringViewModel model, string UserId);



    }
}
