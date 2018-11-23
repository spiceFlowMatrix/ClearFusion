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
        Task<APIResponse> GetMainLevelAccount(int id);
        Task<APIResponse> GetAllAccountsByParentId(long parentId);
        Task<APIResponse> AddChartOfAccount(ChartOfAccountNewModel model);

        //Task<APIResponse> GetAllProvinceDetails(int CountryId);
        //Task<APIResponse> GetAllNationality();
    }
}
