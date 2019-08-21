using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeesAttendanceByDateQuery: IRequest<ApiResponse>
    {
        public string SelectedDate { get; set; }
        public int? OfficeId { get; set; }
        public bool AttendanceStatus { get; set; }
        public long AttendanceGroupId { get; set; }
    }
}