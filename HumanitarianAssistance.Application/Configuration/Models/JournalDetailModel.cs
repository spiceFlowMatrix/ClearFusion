using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.Configuration.Models
{
    public class JournalDetailModel: BaseModel
    {
        public int JournalCode { get; set; }
        public string JournalName { get; set; }
        public byte? JournalType { get; set; }
    }
}