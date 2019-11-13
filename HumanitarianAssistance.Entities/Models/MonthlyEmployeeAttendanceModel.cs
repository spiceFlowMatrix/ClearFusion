using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class MonthlyEmployeeAttendanceModel
    {
        public string Date { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string LateArrival { get; set; }
        public string EarlyOut { get; set; }
        public string AttendanceType { get; set; }
        public string Hours { get; set; }
        public string OverTimeHours { get; set; }
    }
}
