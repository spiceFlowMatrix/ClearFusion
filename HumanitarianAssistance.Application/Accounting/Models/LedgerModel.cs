using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class LedgerModel
    {
        public long ChartOfAccountNewId { get; set; }
        public string ChartAccountName { get; set; }
        public string CurrencyName { get; set; }
        public string MainLevel { get; set; }
        public string ControlLevel { get; set; }
        public string SubLevel { get; set; }
        public double Amount { get; set; }

        public string TransactionType { get; set; }
        public string VoucherReferenceNo { get; set; }
        public int TransactionNo { get; set; }
        public string AccountName { get; set; }
        public DateTime? TransactionDate { get; set; }
        public double? DebitAmount { get; set; }
        public double? CreditAmount { get; set; }
        public string VoucherNo { get; set; }
        public string Description { get; set; }
        public List<TransactionModel> Transactionlist { get; set; }
		public double TotalCredit { get; set; }
		public double TotalDebit { get; set; }
        public string ChartOfAccountNewCode { get; set; }
    }
}