using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class EmployeePayrollModel: BaseModel
    {
        public long PayrollId { get; set; }
        public int HeadTypeId { get; set; }
        public int SalaryHeadId { get; set; }
        public int EmployeeId { get; set; }
        public int CurrencyId { get; set; }
        public int PaymentType { get; set; }
        public string SalaryHeadType { get; set; }
        public string SalaryHead { get; set; }
        public double MonthlyAmount { get; set; }
		public double? PensionRate { get; set; }
        public double? BasicPay { get; set; }
        public long? AccountNo { get; set; }
        public int? TransactionTypeId { get; set; }
    }
}