using System;
using System.Security.Claims;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Chat.Commands.Create;
using HumanitarianAssistance.Application.Chat.Commands.Delete;
using HumanitarianAssistance.Application.Chat.Commands.Update;
using HumanitarianAssistance.Application.Chat.Models;
using HumanitarianAssistance.Application.Chat.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.WebApi.SignalRHub;
using HumanitarianAssistance.WebApi.SignalRHub.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HumanitarianAssistance.WebApi.Controllers.Chat
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/Chat/[Action]")]
    [Authorize]
    public class ChatController : BaseController
    {

        private IHubContext<NotifyHub, INotifyHub> _hubContext;

        public ChatController(IHubContext<NotifyHub, INotifyHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<ApiResponse> AddMessage([FromBody]AddMessageCommand command)
        {

            ApiResponse Response = null;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != null)
            {
                command.CreatedById = userId;
                command.IsDeleted = false;
                command.CreatedDate = DateTime.UtcNow;
                Response = await _mediator.Send(command);
                command = (AddMessageCommand)Response.ResponseData;
                if (Response.StatusCode == 200)
                {
                    ChatModel obj = new ChatModel()
                    {
                        ChatId = command.ChatId,
                        EntityId = command.EntityId,
                        ChatSourceEntityId = command.ChatSourceEntityId,
                        Message = command.Message,
                        UserName = command.UserName,
                        EntitySourceDocumentId = command.EntitySourceDocumentId
                    };
                    await _hubContext.Clients.All.BroadcastMessage(obj);
                }
            }
            return Response;
        }

        [HttpPost]
        public async Task<ApiResponse> EditMessage([FromBody]EditMessageCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteMessage([FromBody] long chatId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteMessageCommand
            {
                chatId = chatId,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }

        [HttpPost]
        public async Task<ApiResponse> ListMessages([FromBody]ListMessagesQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}

