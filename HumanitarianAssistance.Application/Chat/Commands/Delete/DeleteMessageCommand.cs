using MediatR;
using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.Chat.Commands.Delete
{
    public class DeleteMessageCommand : BaseModel, IRequest<ApiResponse>
    {
        public long chatId { get; set; }
    }
}
