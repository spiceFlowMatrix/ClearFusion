using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Delete
{
    public class DeleteJournalDetailCommand: BaseModel, IRequest<ApiResponse>
    {
        public int JournalCode { get; set; }
    }
}