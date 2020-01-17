using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class ApproveRejectLeaveCommand : BaseModel, IRequest<object>
    {
        public long Id { get; set; }
        public bool Approved { get; set; }
        public int EmployeeId { get; set; }
    }
}