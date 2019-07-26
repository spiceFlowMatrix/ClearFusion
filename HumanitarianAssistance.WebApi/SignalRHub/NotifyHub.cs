using HumanitarianAssistance.ViewModels.Models.Common;
using HumanitarianAssistance.WebApi.SignalRHub.Interface;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.SignalRHub
{
    public class NotifyHub: Hub<INotifyHub>
    {
        public async Task BroadcastMessage(ChatModel model)
        {
            await Clients.All.BroadcastMessage(model);
        }

    }
}