using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class EmployeeContractModel
    {
		public string EmployeeName { get; set; }
		public string FatherName { get; set; }
		public string EmployeeCode { get; set; }
		public string Designation { get; set; }
		public DateTime? ContractStartDate { get; set; }
		public DateTime? ContractEndDate { get; set; }
		public int DurationOfContract{ get; set; }
		public double? Salary { get; set; }
		public int? Grade { get; set; }
		public string ProjectName { get; set; }
		public long ProjectCode { get; set; }
		public string DutyStation { get; set; }
		public string Province { get; set; }
		public string BudgetLine { get; set; }
		public int? JobId { get; set; }
		public int WorkTime { get; set; }
		public int? WorkDayHours { get; set; }
		public string ContentEnglish { get; set; }
		public string ContentDari { get; set; }
		public string EmployeeImage { get; set; }

	}
}
