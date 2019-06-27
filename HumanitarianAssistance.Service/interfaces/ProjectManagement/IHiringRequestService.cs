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
        Task<APIResponse> GetAllEmployeeList();

        Task<APIResponse> AddHiringRequestCandidate(HiringRequestCandidateModel model, string userId);
        Task<APIResponse> GetAllCandidateList(ProjectHiringCandidateDetailModel model);

        Task<APIResponse> AddCandidateInterviewDetail(CandidateInterViewModel model, string userId); 
        Task<APIResponse> EditHiringRequestCandidate(ProjectHiringCandidateDetailModel model, string userId);
        Task<APIResponse> HiringRequestSelectCandidate(HiringSelectCandidateModel model, string userId);
        Task<APIResponse> CompleteHiringRequest(long hiringRequestId, string userId);



    }
}
