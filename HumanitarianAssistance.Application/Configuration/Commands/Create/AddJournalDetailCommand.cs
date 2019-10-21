using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddJournalDetailCommand: BaseModel, IRequest<ApiResponse>
    {
         public int JournalCode { get; set; }
        public string JournalName { get; set; }
        public byte? JournalType { get; set; }
        
    }
}