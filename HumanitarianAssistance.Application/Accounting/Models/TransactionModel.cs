using System;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class TransactionModel
    {
        public int TransactionNo { get; set; }
        public string AccountName { get; set; }
        public DateTime TransactionDate { get; set; }
        public double DebitAmount { get; set; }
        public double CreditAmount { get; set; }
        public long VoucherNo { get; set; }
        public string Description { get; set; }
        public string AccountType { get; set; }
    }
}