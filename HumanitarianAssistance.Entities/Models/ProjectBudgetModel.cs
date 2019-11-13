using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class ProjectBudgetModel: BaseModel
	{	
		public long BudgetId { get; set; }
		[Required]
		public long ProjectId { get; set; }
		[Required]
		public double ReceivableAmount { get; set; }
		[Required]
		public double PayableAmount { get; set; }
		[Required]
		public double CurrentBalance { get; set; }
		[Required]
		public DateTime StartDate { get; set; }
		[Required]
		public DateTime EndDate { get; set; }
	}
}
