using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
	public class FinancialReportModel
	{				
		public int? currencyid { get; set; }
		public int? financialreporttype { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }

	}
}
