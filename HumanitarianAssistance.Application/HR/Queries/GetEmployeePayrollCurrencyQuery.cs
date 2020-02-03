using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeePayrollCurrencyQuery: IRequest<object>
    {
        public int EmployeeId { get; set; }
    }
}