using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebAPI.ChaHub
{
  public class LoopyHub : Hub
  {
    public Task Send(string data)
    {
      return Clients.All.SendAsync("Send", data);
    }
  }
}
