using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetEmployeeDetailByEmployeeIdQuery : IRequest<ApiResponse>
    {
        public int EmployeeId { get; set; } 
    }
}
