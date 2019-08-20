using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class SPJournalReport
    {
        public DateTime TransactionDate { get; set; }
        public long VoucherNo { get; set; }
        public string ReferenceNo { get; set; }
        public string TransactionDescription { get; set; }
        public int Currency { get; set; }
        public int ChartOfAccountNewId { get; set; }
        public double CreditAmount { get; set; }
        public double DebitAmount { get; set; }
        public string AccountCode { get; set; }
        public int JournalCode { get; set; }
        public string AccountName { get; set; }
    }
}
