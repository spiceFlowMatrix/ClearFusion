using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class JournalVoucherViewModel
    {
        public string JournalCode { get; set; }
        public long VoucherNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public long AccountCode { get; set; }
        public string AccountName { get; set; }

        public string TransactionDescription { get; set; }
        public int? CurrencyId { get; set; }
        public string Project { get; set; }
        public string BudgetLineDescription { get; set; }
        public double? CreditAmount { get; set; }
        public double? DebitAmount { get; set; }
    }
}
