using System;
using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class EmployeeAttendanceModel: BaseModel
    {
        public long? AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public string TotalWorkTime { get; set; }
        public int? HoverTimeHours { get; set; }
        public int AttendanceTypeId { get; set; } //1 = P, 2 = A, 3 = L
        public DateTime Date { get; set; }
        public bool LeaveStatus { get; set; }
        public int? FinancialYearId { get; set; }
        public int? OfficeId { get; set; }
        public long? AttendanceGroupId { get; set; }
        public int? WorkTimeMinutes { get; set; }
        public int? OvertimeMinutes { get; set; }
    }
}