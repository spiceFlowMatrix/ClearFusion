using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Common
{
    public class ChatModel : BaseModel
    {
        public long? ChatId { get; set; }
        public string Message { get; set; }
        public int ChatSourceEntityId { get; set; }
        public int EntityId { get; set; }
        public long? EntitySourceDocumentId { get; set; }
        public string UserName { get; set; }
    }
}
