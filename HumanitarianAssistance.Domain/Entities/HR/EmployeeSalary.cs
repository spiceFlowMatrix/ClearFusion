using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public  class EmployeeSalary : BaseEntity
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1)]
		public int EmployeeSalaryId { get; set; }
		public int OfficeId { get; set; }
		public int CurrencyId { get; set; }
		public DateTime FinancialYearDate { get; set; }
		public int EmployeeID { get; set; }
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
	}
}
