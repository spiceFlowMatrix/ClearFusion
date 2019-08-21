using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class PayrollHeadModel : BaseModel
    {
        public int PayrollHeadId { get; set; }
        public int PayrollHeadTypeId { get; set; }
        public string PayrollHeadName { get; set; }
        public string Description { get; set; }
        public long? AccountNo { get; set; }
        public int? TransactionTypeId { get; set; }
        public decimal? Amount { get; set; }
        public int? OfficeId { get; set; }
    }
}