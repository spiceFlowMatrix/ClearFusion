using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class BudgetLineEmployeesModel
    {
		public int OfficeId { get; set; }
		public int ProjectId { get; set; }
		public int BudgetLineId { get; set; }
		public int EmployeeId { get; set; }
		public string EmployeeName { get; set; }
		public bool IsActive { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
	}
}
