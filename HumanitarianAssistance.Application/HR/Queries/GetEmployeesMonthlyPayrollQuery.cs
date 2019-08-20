using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeesMonthlyPayrollQuery: IRequest<ApiResponse>
    {
        public int OfficeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int PaymentType { get; set; }
        public int CurrencyId { get; set; }
    }
}