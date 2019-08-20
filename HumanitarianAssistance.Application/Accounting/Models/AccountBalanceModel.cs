namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class AccountBalanceModel
    {
        public long AccountId { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public string AccountCode { get; set; }
    }
}