using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAppliedLeaveDatesQuery: BaseModel, IRequest<object>
    {
        public int LeaveReasonId { get; set; }
        public int EmployeeId { get; set; }
    }
}