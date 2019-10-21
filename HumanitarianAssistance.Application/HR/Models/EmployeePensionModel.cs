using System.Collections.Generic;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class EmployeePensionModel
    {
        public List<EmployeePensionReportModel> EmployeePensionReportList { get; set; }
		public double? PensionTotal { get; set; }
		public double? PreviousTotal { get; set; }
		public double? PreviousPensionRate { get; set; }
		public double? PreviousProfit { get; set; }
		public double? PreviousPensionDeduction { get; set; }
		public double? PensionDeductionTotal { get; set; }
		public double? PensionProfitTotal { get; set; }
        public string EmployeeCode { get; set; }
    }
}