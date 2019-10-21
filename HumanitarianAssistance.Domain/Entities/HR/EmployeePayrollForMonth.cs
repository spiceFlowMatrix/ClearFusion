using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class EmployeePayrollForMonth:BaseEntity
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1)]
		public int EmployeePaymentTypesId { get; set; }
		public int OfficeId { get; set; }
		public int CurrencyId { get; set; }
		public DateTime FinancialYearDate { get; set; }
		public int EmployeeID { get; set; }
		public EmployeeDetail EmployeeDetail { get; set; }
		public string EmployeeName { get; set; }
		//public string PaymentType { get; set; }
		public int PaymentType { get; set; }
		public int WorkingDays { get; set; }
		public int PresentDays { get; set; }
		public int AbsentDays { get; set; }
		public int LeaveDays { get; set; }
		//public int TotalWorkedDays { get; set; }
		public int TotalWorkHours { get; set; }
		public double? HourlyRate { get; set; }
		public double? TotalGeneralAmount { get; set; }
		public double? TotalAllowance { get; set; }
		//public double? TotalEarning { get; set; }
		public double? TotalDeduction { get; set; }
		public double? GrossSalary { get; set; }
		public double? OverTimeHours { get; set; }
		public bool IsApproved { get; set; }
		public double? PensionRate { get; set; }
		//public List<EmployeeMonthlyPayroll> MonthlyPayrollList { get; set; }
		public double? PensionAmount { get; set; }
		public double? SalaryTax { get; set; }
		public double? NetSalary { get; set; }
		public double AdvanceAmount { get; set; }
		public bool IsAdvanceApproved { get; set; }
		public bool IsAdvanceRecovery { get; set; }
		public double AdvanceRecoveryAmount { get; set; }

	}
}
