using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeAssignLeaveQuery: IRequest<ApiResponse>
    {
        public long EmployeeId { get; set; }
    }
}