using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeHistoryOutsideOrganizationQuery : IRequest<ApiResponse>
    {
        public int EmployeeId { get; set; }
    }
}