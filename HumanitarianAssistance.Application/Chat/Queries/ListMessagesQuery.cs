using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Chat.Queries
{
    public class ListMessagesQuery : IRequest<ApiResponse>
    {
        public long? ChatId { get; set; }
        public string Message { get; set; }
        public int ChatSourceEntityId { get; set; }
        public int EntityId { get; set; }
        public long? EntitySourceDocumentId { get; set; }
        public string UserName { get; set; }
    }
}
