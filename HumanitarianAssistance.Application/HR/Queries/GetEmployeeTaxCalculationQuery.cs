using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeTaxCalculationQuery: IRequest<ApiResponse>
    {
        public int OfficeId { get; set; }
        public int EmployeeId { get; set; }
        public List<int> FinancialYearId { get; set; }
        public int? CurrencyId { get; set; }
    }
}