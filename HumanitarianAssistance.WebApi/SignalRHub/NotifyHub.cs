using DataAccess.DbEntities;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models.Common;
using HumanitarianAssistance.WebApi.SignalRHub.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.SignalRHub
{
    public class NotifyHub: Hub<INotifyHub>
    {
        private readonly IChat _IChatService;
        private readonly UserManager<AppUser> _userManager;

        public NotifyHub(IChat iChatService, UserManager<AppUser> userManager)
        {
            _IChatService = iChatService;
            _userManager = userManager;
        }

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