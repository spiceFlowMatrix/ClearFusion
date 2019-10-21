using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public  class EmployeeAttendance : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeDetail EmployeeDetails { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public string TotalWorkTime { get; set; }       
        public int? HoverTimeHours { get; set; }
        public int AttendanceTypeId { get; set; } //1 = P, 2 = A, 3 = L
        public int? LeaveReasonId { get; set; }
        public LeaveReasonDetail LeaveReasonDetail { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public long? HolidayId { get; set; }
        public int WorkTimeMinutes { get; set; }
        public int OverTimeMinutes { get; set; }
        public HolidayDetails HolidayDetails { get; set; }

        public int? FinancialYearId { get; set; }
        public FinancialYearDetail FinancialYearDetail { get; set; }
    }
}
