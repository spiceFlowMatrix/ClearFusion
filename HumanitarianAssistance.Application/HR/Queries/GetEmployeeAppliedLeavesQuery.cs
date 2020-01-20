using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeAppliedLeavesQuery: IRequest<object>
    {
        public int EmployeeId { get; set; }
    }
}