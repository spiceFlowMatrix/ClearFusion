using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class LedgerReportViewModel
    {
        public LedgerReportViewModel()
        {
            LedgerList = new List<LedgerModel>();
            //AccountName = null;
            //DebitAmount = 0;
            //CreditAmount = 0;
        }

        public List<LedgerModel> LedgerList { get; set; }
        public string AccountName { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal Balance { get; set; }

    }
}
