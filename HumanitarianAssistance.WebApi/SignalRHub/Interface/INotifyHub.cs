using HumanitarianAssistance.ViewModels.Models.Common;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.SignalRHub.Interface
{
    public interface INotifyHub
    {
        Task BroadcastMessage(ChatModel model);
    }
}