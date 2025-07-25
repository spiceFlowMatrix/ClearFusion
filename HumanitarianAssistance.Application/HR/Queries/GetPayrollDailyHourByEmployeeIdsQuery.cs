using MediatR;
using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetPayrollDailyHourByEmployeeIdsQuery: IRequest<object>
    {
        public int[] EmpIds { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<int> OfficeIds { get; set; }
    }

    public class ExistingAttendanceDetailModel
    {
        public int EmployeeID { get; set; }
        public long? AttendanceGroupId { get; set; }
        public string AttendanceGroupName { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public int AttendanceType { get; set; }
        public int OfficeId { get; set; }
    }

    public class PayrollExceptionModel {
        public string Office { get; set; }
        public string AttendanceGroup { get; set; }
    }
}