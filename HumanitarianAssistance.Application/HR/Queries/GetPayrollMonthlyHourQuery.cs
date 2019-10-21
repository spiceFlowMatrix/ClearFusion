using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetPayrollMonthlyHourQuery: IRequest<ApiResponse>
    {
        public int Year { get; set; }
        public int OfficeId { get; set; }
        public long AttendanceGroupId { get; set; }
    }
}