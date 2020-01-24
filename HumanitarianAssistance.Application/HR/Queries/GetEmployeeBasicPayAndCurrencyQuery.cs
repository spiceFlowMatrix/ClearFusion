using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeBasicPayAndCurrencyQuery: IRequest<object>
    {
        public int EmployeeId { get; set; }
    }
}