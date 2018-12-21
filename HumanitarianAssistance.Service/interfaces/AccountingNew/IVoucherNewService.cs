using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces.AccountingNew
{
    public interface IVoucherNewService
    {
        Task<APIResponse> GetAllNewVoucherList(VoucherNewFilterModel voucherNewFilterModel);
        Task<APIResponse> AddVoucherNewDetail(VoucherDetailModel model);
        Task<APIResponse> EditVoucherNewDetail(VoucherDetailModel model)
    }
}
