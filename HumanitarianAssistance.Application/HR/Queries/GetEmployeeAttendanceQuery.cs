using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeAttendanceQuery: IRequest<ApiResponse>
    {
        public int EmployeeId { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
    }
}