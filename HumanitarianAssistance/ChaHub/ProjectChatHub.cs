using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebAPI.ChaHub
{
  public class ProjectChatHub : Hub
  {
    public async Task Send(string message)
    {
      await Clients.All.SendAsync("Send", message);
    }
    public async Task ActivityPermissionChanged(string message)
    {
      await Clients.All.SendAsync("activityPermissionChanged", message);
    }
  }


}
