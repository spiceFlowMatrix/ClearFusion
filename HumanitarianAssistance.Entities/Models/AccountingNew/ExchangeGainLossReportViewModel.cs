using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.AccountingNew
{
    public class ExchangeGainLossReportViewModel
    {
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountCodeName { get; set; }
        public decimal BalanceOnOriginalDate { get; set; }
        public decimal BalanceOnCurrentDate { get; set; }
        public decimal GainLossAmount { get; set; }
    }
}
