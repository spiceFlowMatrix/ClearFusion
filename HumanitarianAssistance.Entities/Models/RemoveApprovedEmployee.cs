using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
	public class RemoveApprovedEmployee 
	{		
		public DateTime FinancialYearDate { get; set; }
		public int PaymentType { get; set; }
		public int OfficeId { get; set; }
	}
}
