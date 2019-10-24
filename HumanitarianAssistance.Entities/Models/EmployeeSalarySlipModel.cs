using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class EmployeeSalarySlipModel
    {
		public string EmployeeCode { get; set; }
		public string EmployeeName { get; set; }
		public string Designation { get; set; }
		public string Type { get; set; }
		public string Office { get; set; }
		public string Sex { get; set; }



		public long BudgetLine { get; set; }
		public string Program { get; set; }
		public int ProjectId { get; set; }
		public int? JobId { get; set; }
		public int? Sector { get; set; }
		public string Area { get; set; }
		public int? Account { get; set; }
		public double? SalaryPercentage { get; set; }
		public double? Salary { get; set; }



		public double? BasicSalary { get; set; }
		public string CurrencyCode { get; set; }
		public int Attendance { get; set; }
		public int Absentese { get; set; }


		//public double Food { get; set; }
		//public double Tr { get; set; }
		//public double Medical { get; set; }
		//public double OtherAdvance1 { get; set; }

		//public List<EmployeePayrollModel> EmployeePayrollModelLst { get; set; }

		public long PayrollId { get; set; }
		public int HeadTypeId { get; set; }
		public int SalaryHeadId { get; set; }
		public int EmployeeId { get; set; }
		public int CurrencyId { get; set; }
		public int PaymentType { get; set; }


		public string SalaryHeadType { get; set; }
		public string SalaryHead { get; set; }
		public double MonthlyAmount { get; set; }
		public double? PensionRate { get; set; }



		//public double Advance { get; set; }
		//public double SalaryTax { get; set; }
		//public double Fine { get; set; }

		//public double OtherDeduction1 { get; set; }
		//public double Security { get; set; }


		public double? GrossSalary { get; set; }
		public double? NetSalary { get; set; }


		public string Description { get; set; }


	}
}
