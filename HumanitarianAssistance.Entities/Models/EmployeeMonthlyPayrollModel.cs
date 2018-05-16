using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class EmployeeMonthlyPayrollModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        //public string PaymentType { get; set; }
        public int PaymentType { get; set; }
        public int WorkingDays { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public int LeaveDays { get; set; }
		public int LeaveHours { get; set; }
		//public int TotalWorkedDays { get; set; }
		public int TotalWorkHours { get; set; }
        public double? HourlyRate { get; set; }
        public double? TotalGeneralAmount { get; set; }
        public double? TotalAllowance { get; set; }
        //public double? TotalEarning { get; set; }
        public double? TotalDeduction { get; set; }
        public double? GrossSalary { get; set; }
        public double? OverTimeHours { get; set; }
		public double? NetSalary { get; set; }

		public long PayrollId { get; set; }
		public int HeadTypeId { get; set; }
		public int SalaryHeadId { get; set; }
		public int CurrencyId { get; set; }
		public string SalaryHeadType { get; set; }
		public string SalaryHead { get; set; }
		public double MonthlyAmount { get; set; }
		public double? PensionRate { get; set; }

        public double? PensionAmount { get; set; }

        public bool IsApproved { get; set; }
		public DateTime Date { get; set; }

		public double? SalaryTax { get; set; }
		public double? AdvanceAmount { get; set; }
		public bool IsDeductionApproved { get; set; }

		public List<EmployeePayrollModel> employeepayrolllist { get; set; }
        
    }
}
