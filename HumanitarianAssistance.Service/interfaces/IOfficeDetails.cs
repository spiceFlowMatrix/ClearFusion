using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IOfficeDetails
    {
        Task<APIResponse> AddOfficeDetails(OfficeDetailModel model);
        Task<APIResponse> EditOfficeDetails(OfficeDetailModel model);
        Task<APIResponse> DeleteOfficeDetails(OfficeDetailsModelDelete model);
        Task<APIResponse> GetAllOfficeDetails();
        Task<APIResponse> GetOfficeDetailsByOfficeCode(string OfficeCode);
    }
}
