namespace HumanitarianAssistance.Application.HR.Models
{
    public class PensionPaymentModel
    {
        public int? EmployeeId { get; set; }
        public decimal TotalPensionAmount { get; set; }
        public decimal PensionAmountPaid { get; set; }
        public decimal BalancePensionAmount { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public int? CurrencyId { get; set; }
    }
}