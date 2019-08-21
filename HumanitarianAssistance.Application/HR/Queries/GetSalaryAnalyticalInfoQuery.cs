using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetSalaryAnalyticalInfoQuery : IRequest<ApiResponse>
    {
        public int EmployeeId { get; set; }

    }
}
