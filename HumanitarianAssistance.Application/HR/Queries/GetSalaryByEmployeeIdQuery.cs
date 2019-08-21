using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetSalaryByEmployeeIdQuery: IRequest<ApiResponse>
    {
        public int EmployeeId { get; set; }
    }
}