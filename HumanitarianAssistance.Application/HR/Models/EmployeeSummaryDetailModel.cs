namespace HumanitarianAssistance.Application.HR.Models
{
    public class EmployeeSummaryDetailModel
    {
        public int? Currency { get; set; }
		public double? TotalGrossSalary { get; set; }
		public double? TotalAllowance { get; set; }
		public double? TotalDeduction { get; set; }
    }
}