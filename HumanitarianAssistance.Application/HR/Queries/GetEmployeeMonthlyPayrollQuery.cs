using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeMonthlyPayrollQuery:IRequest<object>
    {
        public int EmployeeId { get; set; }
        public int Month { get; set; }
    }
}