using DataAccess.DbEntities;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models.Common;
using HumanitarianAssistance.WebApi.SignalRHub.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.SignalRHub
{
    public class ChatHub : Hub
    {
        private readonly IChat _IChatService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public ChatHub(IChat iChatService, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _IChatService = iChatService;
            _userManager = userManager;
            _HttpContextAccessor = httpContextAccessor;
        }

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
