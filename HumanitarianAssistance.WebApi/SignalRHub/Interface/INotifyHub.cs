using HumanitarianAssistance.Application.Chat.Models;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.SignalRHub.Interface
{
    public interface INotifyHub
    {
        Task BroadcastMessage(ChatModel model);
        Task Send(string method, string message);
        Task ActivityPermissionChanged(string method, string message);
    }
}