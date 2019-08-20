using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class JournalReportViewModel
    {
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal Balance { get; set; }
        public long ChartOfAccountNewId { get; set; }
    }
}
