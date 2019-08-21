using System;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class SalaryTaxReportModel
    {
        public DateTime Date { get; set; }
		public string Office { get; set; }
		public string Currency { get; set; }
        public int? CurrencyId { get; set; }
        public double? TotalTax { get; set; }
    }
}