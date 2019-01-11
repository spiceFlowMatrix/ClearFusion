using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.SPModels
{
    public class SPLedgerReport
    {
        public long ChartOfAccountNewId { get; set; }
        public string AccountName { get; set; }
        public long VoucherNo { get; set; }
        public string Description { get; set; }
        public string VoucherReferenceNo { get; set; }
        public string CurrencyName { get; set; }
        public long DebitAmount { get; set; }
        public long CreditAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int CurrencyId { get; set; }
        public string ChartOfAccountNewCode { get; set; }
    }
}
