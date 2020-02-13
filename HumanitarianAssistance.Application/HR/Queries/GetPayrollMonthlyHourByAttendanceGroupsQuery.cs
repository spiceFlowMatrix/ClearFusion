using MediatR;
using System;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetPayrollMonthlyHourByAttendanceGroupsQuery: IRequest<object>
    {
        public long AttendanceGroupId { get; set; }
    }
}