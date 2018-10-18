using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class EmployeeAttendanceModel : BaseModel
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
        //public string AttendanceTypeName { get; set; }
        //public int? LeaveReasonId { get; set; }
        //public string LeaveReasonName { get; set; }
        public DateTime Date { get; set; }
        //public string Remarks { get; set; }
        public bool LeaveStatus { get; set; }
        public int? FinancialYearId { get; set; }
        public int? OfficeId { get; set; }
    }

    public class DisplayEmployeeAttendanceModel
    {
        public long? attendanceId { get; set; }

        public int employeeID { get; set; }
        public string text { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }

        public int? OverTimeHours { get; set; }
    }
}
