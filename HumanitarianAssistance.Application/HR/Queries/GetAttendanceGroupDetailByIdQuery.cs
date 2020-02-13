using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAttendanceGroupDetailByIdQuery: IRequest<object>
    {
        public long AttendanceGroupId { get; set; }
    }
}