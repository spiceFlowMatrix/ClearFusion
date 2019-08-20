using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public  class EmployeePaymentTypes : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int EmployeePaymentTypesId { get; set; }
        public int? OfficeId { get; set; }
        public int? CurrencyId { get; set; }
        public DateTime? FinancialYearDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int? EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public EmployeeDetail EmployeeDetail { get; set; }
        public string EmployeeName { get; set; }
        //public string PaymentType { get; set; }
        public int? PaymentType { get; set; }
        public int? WorkingDays { get; set; }
        public int? PresentDays { get; set; }
        public int? AbsentDays { get; set; }
        public int? LeaveDays { get; set; }
        //public int TotalWorkedDays { get; set; }
        public int? TotalWorkHours { get; set; }
        public double? HourlyRate { get; set; }
        public double? TotalGeneralAmount { get; set; }
        public double? TotalAllowance { get; set; }
        //public double? TotalEarning { get; set; }
        public double? TotalDeduction { get; set; }
        public double? GrossSalary { get; set; }
        public double? OverTimeHours { get; set; }
        public bool? IsApproved { get; set; }
        public double? PensionRate { get; set; }
        //public List<EmployeeMonthlyPayroll> MonthlyPayrollList { get; set; }
        public double? PensionAmount { get; set; }//PensionDeduction
        public double? SalaryTax { get; set; }//SalaryTaxDeduction
        public double? NetSalary { get; set; }
        public double? AdvanceAmount { get; set; }
        public bool? IsAdvanceApproved { get; set; }
        public bool? IsAdvanceRecovery { get; set; }
        public double? AdvanceRecoveryAmount { get; set; }
        public string OfficeCode { get; set; }
        public string CurrencyCode { get; set; }
        public int? PayrollYear { get; set; }
        public int? PayrollMonth { get; set; }
        public int? Attendance { get; set; }
        public int? Absent { get; set; }
        public int? TotalDuration { get; set; }
        public float? BasicPay { get; set; }
        public int? AdvanceId { get; set; }
        [ForeignKey("AdvanceId")]
        public Advances AdvanceDetail { get; set; }
    }
}
