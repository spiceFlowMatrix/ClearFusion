using HumanitarianAssistance.Application.Chat.Models;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.WebApi.SignalRHub.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.SignalRHub
{
    public class NotifyHub : Hub<INotifyHub>
    {
        public async Task BroadcastMessage(ChatModel model)
        {
            await Clients.All.BroadcastMessage(model);
        }

        public async Task Send(string message)
        {
            await Clients.All.Send("Send", message);
        }

        public async Task ActivityPermissionChanged(string message)
        {
            await Clients.All.ActivityPermissionChanged("activityPermissionChanged", message);
        }

    }
}