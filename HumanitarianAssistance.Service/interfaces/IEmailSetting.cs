using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IEmailSetting
    {
        Task<APIResponse> GetAllEmailSettingDetail();
        Task<APIResponse> AddEmailSettingDetail(EmailSettingModel model);
        Task<APIResponse> EditEmailSettingDetail(EmailSettingModel model);
        Task<APIResponse> GetAllEmailType();
    }
}
