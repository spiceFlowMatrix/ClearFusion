using HumanitarianAssistance.Entities.Models;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IChartAccoutDetail
    {
        Task<APIResponse> GetAllChartAccountDetail();
        Task<APIResponse> GetAllAccountLevel();
        Task<APIResponse> GetAllAccountTypeByCategory(int id);
        Task<APIResponse> GetAllAccountByAccountHeadTypeId(int id);
        Task<APIResponse> AddAccountType(AccountTypeModel model);
        Task<APIResponse> EditAccountType(AccountTypeModel model);
        Task<APIResponse> AddChartAccountDetail(ChartAccountDetailModel model);
        Task<APIResponse> EditChartAccountDetail(ChartAccountDetailModel model);

        
    }
}
