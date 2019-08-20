using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class EmployeeSalaryBudget:BaseEntity
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1)]
		public int EmployeeSalaryBudgetId { get; set; }
		public string Year { get; set; }
		public int CurrencyId { get; set; }
		public double SalaryBudget { get; set; }
		public double BudgetDisbursed { get; set; }
		public int EmployeeID { get; set; }
		public EmployeeDetail EmployeeDetail { get; set; }
	}
}
