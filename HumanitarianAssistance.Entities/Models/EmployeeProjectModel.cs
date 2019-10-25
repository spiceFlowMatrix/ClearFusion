using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class EmployeeProjectModel
    {
		public int EmployeeId { get; set; }
		public int ProjectId { get; set; }
		public string ProjectName { get; set; }
		public long BudgetLineId { get; set; }
		public string BudgetLineName { get; set; }
		public double? ProjectPercentage { get; set; }
	}
}
