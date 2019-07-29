using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.Service.interfaces.AccountingNew;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.Common;
using HumanitarianAssistance.WebApi.SignalRHub;
using HumanitarianAssistance.WebApi.SignalRHub.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HumanitarianAssistance.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Chat/[Action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IChat _iChat;
        private IHubContext<NotifyHub, INotifyHub> _hubContext;

        public ChatController(UserManager<AppUser> userManager, IChat iChat, IHubContext<NotifyHub, INotifyHub> hubContext)
        {
            _userManager = userManager;
            _iChat = iChat;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<APIResponse> AddMessage([FromBody]ChatModel model)
        {
            APIResponse apiResponse = null;

            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user != null)
            {
                model.CreatedById = user.Id;
                model.IsDeleted = false;
                model.CreatedDate = DateTime.UtcNow;

                apiResponse = await _iChat.AddMessage(model);

                model = (ChatModel)apiResponse.ResponseData;

                if (apiResponse.StatusCode == 200)
                {
                    await _hubContext.Clients.All.BroadcastMessage(model);
                }
            }

            return apiResponse;
        }

        [HttpPost]
        public async Task<APIResponse> EditMessage([FromBody]ChatModel model)
        {
            APIResponse apiResponse = null;

            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (user != null)
            {
                model.ModifiedById = user.Id;
                model.IsDeleted = false;
                model.ModifiedDate = DateTime.UtcNow;

                apiResponse = await _iChat.EditMessage(model);
            }
            return apiResponse;
        }

        [HttpPost]
        public async Task<APIResponse> DeleteMessage([FromBody] long chatId)
        {
            APIResponse apiResponse = null;

            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user != null)
            {
                apiResponse = await _iChat.DeleteMessage(chatId, user.Id);
            }
            return apiResponse;
        }

        [HttpPost]
        public async Task<APIResponse> ListMessages([FromBody] ChatModel model)
        {
            APIResponse apiResponse = null;
            apiResponse = await _iChat.ListMessages(model);
            return apiResponse;
        }
    }
}