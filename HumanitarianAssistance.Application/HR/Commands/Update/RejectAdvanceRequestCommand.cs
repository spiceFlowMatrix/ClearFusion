using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class RejectAdvanceRequestCommand: BaseModel, IRequest<object>
    {
        public long AdvanceId { get; set; }
    }
}