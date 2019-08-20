using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class MonthlyEmployeeAttendanceReportQuery: IRequest<ApiResponse>
    {
        public int employeeid { get; set; }
		public int year { get; set; }
		public int month { get; set; }
		public int OfficeId { get; set; }
    }
}