using System;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class ExchangeGainLossReportViewModel
    {
        public long AccountId {get; set;}
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountCodeName { get; set; }
        public decimal BalanceOnOriginalDate { get; set; }
        public decimal BalanceOnCurrentDate { get; set; }
        public decimal GainLossAmount { get; set; }
        public int GainLossStatus { get; set; }
    }
}