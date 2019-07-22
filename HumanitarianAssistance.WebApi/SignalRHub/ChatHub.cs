using DataAccess.DbEntities;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models.Common;
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

        public async Task AddMessage(ChatModel model)
        {
            var user = await _userManager.FindByNameAsync(_HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user != null)
            {
                var id = user.Id;
                model.CreatedById = id;
                model.IsDeleted = false;
                model.CreatedDate = DateTime.UtcNow;
                await _IChatService.AddMessage(model);
            }

            await Clients.All.SendAsync("ReceiveMessage", model);
        }

        public async Task EditMessage(ChatModel model)
        {
            var user = await _userManager.FindByNameAsync(_HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user != null)
            {
                var id = user.Id;
                model.CreatedById = id;
                model.IsDeleted = false;
                model.CreatedDate = DateTime.UtcNow;
                await _IChatService.EditMessage(model);
            }

            await Clients.All.SendAsync("ReceiveMessage", model);
        }

        public async Task DeleteMessage(long chatId)
        {
            var user = await _userManager.FindByNameAsync(_HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user != null)
            {
                var id = user.Id;
                await _IChatService.DeleteMessage(chatId, id);
            }

            await Clients.All.SendAsync("ReceiveMessage", chatId);
        }

        public async Task ListMessages(ChatModel model)
        {

            List<ChatModel> chatList = new List<ChatModel>();

            var user = await _userManager.FindByNameAsync(_HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user != null)
            {
                var id = user.Id;
                model.CreatedById = id;
                model.IsDeleted = false;
                model.CreatedDate = DateTime.UtcNow;
                var data = await _IChatService.ListMessages(model);
                chatList = data.ResponseData != null? (List<ChatModel>)data.ResponseData : null;
            }

            await Clients.All.SendAsync("ReceiveMessage", chatList);
        }
    }
}
