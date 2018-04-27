using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class SalaryTaxReportModel
    {
		public DateTime Date { get; set; }
		public int CurrencyId { get; set; }
		public double? TotalTax { get; set; }
	}
}
