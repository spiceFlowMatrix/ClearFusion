namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class VoucherSummaryTransactionModel
    {
        public string TransactionDescription { get; set; }
        public string AccountName { get; set; }
        public string AccountCode { get; set; }
        public double? Amount { get; set; }
        public string CurrencyName { get; set; }
        public string TransactionType { get; set; }
    }
}