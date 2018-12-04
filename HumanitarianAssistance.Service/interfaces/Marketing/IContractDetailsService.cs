using DataAccess.DbEntities.Marketing;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces.Marketing
{
    public interface IContractDetailsService
    {
        Task<APIResponse> GetContractDetailsById(int contractId, string userId);
        Task<APIResponse> GetAllContractDetails();
        Task<APIResponse> AddContractDetails(ContractDetailsModel model, string UserId);
        Task<APIResponse> EditContractDetails(ContractDetailsModel model, string UserId);
        Task<APIResponse> DeleteContractDetail(int model, string userId);
        Task<APIResponse> AddEditContractDetail(ContractDetailsModel model, string UserId);
        Task<APIResponse> ApproveContract(ApproveContractModel model, string UserId);
        Task<APIResponse> FilterContractList(FilterContractModel model, string UserId);
    }
}
