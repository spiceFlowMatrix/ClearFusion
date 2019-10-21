using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class EmployeeSummaryDetails
    {
		public int Currency { get; set; }
		public double? TotalGrossSalary { get; set; }
		public double? TotalAllowance { get; set; }
		public double? TotalDeduction { get; set; }
	}
}
