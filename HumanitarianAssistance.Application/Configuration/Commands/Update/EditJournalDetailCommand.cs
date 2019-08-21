using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditJournalDetailCommand: BaseModel, IRequest<ApiResponse>
    {
        public int JournalCode { get; set; }
        public string JournalName { get; set; }
        public byte? JournalType { get; set; }
    }
}