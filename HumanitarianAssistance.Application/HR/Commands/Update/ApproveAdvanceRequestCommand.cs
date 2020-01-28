using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class ApproveAdvanceRequestCommand: BaseModel, IRequest<object>
    {
        public long AdvanceId { get; set; }
        public double AdvanceAmount { get; set; }
    }
}