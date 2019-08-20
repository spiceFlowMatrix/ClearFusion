using System;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class SP_TrialBalanceModel
    {
        public long ChartOfAccountNewId { get; set; }
       // public string ChartAccountName { get; set; }
        public string ChartOfAccountNewCode { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string AccountName { get; set; }
        public DateTime TransactionDate { get; set; }
        public double DebitAmount { get; set; }
        public double CreditAmount { get; set; }
        public string Description { get; set; }
    }
}