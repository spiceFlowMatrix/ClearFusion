using System;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class EmployeePensionReportModel
    {
        public int CurrencyId { get; set; }
		public DateTime Date { get; set; }
		public double? GrossSalary { get; set; }
		public double? PensionRate { get; set; }
		public double? PensionDeduction { get; set; }
		public double? Profit { get; set; }
		public double? Total { get; set; }
    }
}