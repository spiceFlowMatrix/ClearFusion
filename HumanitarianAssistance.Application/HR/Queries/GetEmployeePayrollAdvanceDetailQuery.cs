using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeePayrollAdvanceDetailQuery:IRequest<object>
    {
        public int EmployeeId { get; set; }
        public int Month {get; set;}
    }
}