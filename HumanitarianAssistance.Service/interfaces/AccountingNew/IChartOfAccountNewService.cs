using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces.AccountingNew
{
    public interface IChartOfAccountNewService
    {
        Task<APIResponse> GetMainLevelAccount(long id);
        Task<APIResponse> GetAllAccountsByParentId(long parentId);
        Task<APIResponse> GetAllAccountsByAccountHeadTypeId(long id);
        Task<APIResponse> AddChartOfAccount(ChartOfAccountNewModel model);
        Task<APIResponse> GetAllAccountFilter();
        Task<APIResponse> EditChartOfAccount(ChartOfAccountNewModel model);
        Task<APIResponse> DeleteChartOfAccount(long accountId,string userId);




        //Task<APIResponse> GetAllProvinceDetails(int CountryId);
        //Task<APIResponse> GetAllNationality();
    }
}
