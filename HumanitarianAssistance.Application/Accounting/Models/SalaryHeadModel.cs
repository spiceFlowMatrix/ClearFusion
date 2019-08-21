using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class SalaryHeadModel : BaseModel
    {
        public int SalaryHeadId { get; set; }
        public int HeadTypeId { get; set; }
        public string HeadName { get; set; }
        public string Description { get; set; }
        public long AccountNo { get; set; }
        public int? TransactionTypeId { get; set; }
        public decimal? MonthlyAmount { get; set; }
        public bool SaveForAll { get; set; }
    }
}