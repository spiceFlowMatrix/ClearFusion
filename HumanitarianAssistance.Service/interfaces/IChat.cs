using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IChat
    {
        Task<APIResponse> AddMessage(ChatModel model);
        Task<APIResponse> EditMessage(ChatModel model);
        Task<APIResponse> DeleteMessage(long chatId, string userId);
        Task<APIResponse> ListMessages(ChatModel model);
    }
}
