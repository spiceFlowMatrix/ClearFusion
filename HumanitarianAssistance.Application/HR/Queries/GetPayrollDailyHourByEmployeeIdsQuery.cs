using MediatR;
using System;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetPayrollDailyHourByEmployeeIdsQuery: IRequest<object>
    {
        public int[] EmpIds { get; set; }
        public DateTime Date { get; set; }
        public int OfficeId { get; set; }
    }

    public class ExistingAttendanceDetailModel
    {
        public int EmployeeID { get; set; }
        public long? AttendanceGroupId { get; set; }
        public string AttendanceGroupName { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public int AttendanceType { get; set; }
    }
}