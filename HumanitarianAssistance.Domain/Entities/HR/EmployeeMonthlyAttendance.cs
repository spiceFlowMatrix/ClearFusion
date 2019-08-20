using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public  class EmployeeMonthlyAttendance : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int MonthlyAttendanceId { get; set; }
        public int? EmployeeId { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public int? AttendanceHours { get; set; }
        public int? OvertimeHours { get; set; }
        public int? LeaveHours { get; set; }
        public int? AbsentHours { get; set; }
        public int? DeputationHours { get; set; }
        public bool? Status { get; set; }
        public bool? Sent { get; set; }
        public int? TotalDuration { get; set; }
        public int? OfficeId { get; set; }
        public int? CurrencyId { get; set; }
        public int? PaymentType { get; set; }
        public double? HourlyRate { get; set; }
        public double? TotalGeneralAmount { get; set; }
        public double? TotalAllowance { get; set; }
        public double? TotalDeduction { get; set; }
        public double? GrossSalary { get; set; }
        public double? PensionRate { get; set; }
        public double? PensionAmount { get; set; }
        public double? SalaryTax { get; set; }
        public double? NetSalary { get; set; }
        public double AdvanceAmount { get; set; }
        public bool IsAdvanceApproved { get; set; }
        public bool IsAdvanceRecovery { get; set; }
        public double AdvanceRecoveryAmount { get; set; }
        public bool IsApproved { get; set; }
        public int? AdvanceId { get; set; }
        public int AttendanceMinutes { get; set; }
        public int OverTimeMinutes { get; set; }
        [ForeignKey("AdvanceId")]
        public Advances Advance { get; set; }
        [ForeignKey("EmployeeId")]
        public EmployeeDetail EmployeeDetails { get; set; }

    }
}
