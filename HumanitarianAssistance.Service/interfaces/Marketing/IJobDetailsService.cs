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
        Task<APIResponse> DeleteJobDetail(int model, string userId);
        Task<APIResponse> FilterJobList(JobFilterModel model, string userId);
        Task<APIResponse> FilterJobsList(FilterJobModel model, string userId);
        Task<APIResponse> AddEditJobDetail(JobDetailsModel model, string UserId);
        Task<APIResponse> GetJobDetailsById(int model, string UserId);
        Task<APIResponse> GetJobsPaginatedList(JobPaginationModel model, string UserId);
        Task<APIResponse> ApproveJob(int model, string UserId);
        Task<APIResponse> AcceptAgreement(int model, string UserId);
        Task<APIResponse> CreatePDF(int JobId);
        Task<APIResponse> GenerateInvoice(int jobId, string userId);
        Task<APIResponse> FetchInvoice(int jobId, string userId);
        Task<APIResponse> ApproveInvoice(int jobId, string userId);
    }
}
