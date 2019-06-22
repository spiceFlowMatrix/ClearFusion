using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class EmployeeAttendanceFilterModel
    {
        public int EmployeeId { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
    }

    public class EmployeeAttendanceFilterViewModel
    {
        public string SelectedDate { get; set; }
        public int? OfficeId { get; set; }
        public bool AttendanceStatus { get; set; }
        public long AttendanceGroupId { get; set; }
    }
}
