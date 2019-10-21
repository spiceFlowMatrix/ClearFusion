using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Chat.Models
{
    public class ChatModel
    {
        public long? ChatId { get; set; }
        public string Message { get; set; }
        public int ChatSourceEntityId { get; set; }
        public int EntityId { get; set; }
        public long? EntitySourceDocumentId { get; set; }
        public string UserName { get; set; }
    }
}
