using System;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class DisplayEmployeeAttendanceModel
    {
        public long? attendanceId { get; set; }
        public int employeeID { get; set; }
        public string text { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int? OverTimeHours { get; set; }
        public DateTime? Date { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }


    }
}