using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class JournalVoucherViewModel
    {
        public int? JournalCode { get; set; }
        public long VoucherNo { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }

        public string TransactionDescription { get; set; }
        public int? CurrencyId { get; set; }
        public string Project { get; set; }
        public string BudgetLineDescription { get; set; }
        public double? CreditAmount { get; set; }
        public double? DebitAmount { get; set; }
        public string ReferenceNo { get; set; }
        public long ChartOfAccountNewId { get; set; }
    }
}
