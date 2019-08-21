using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeePensionHistoryQuery: IRequest<ApiResponse>
    {
        public int OfficeId { get; set; }
        public int EmployeeId { get; set; }
    }
}