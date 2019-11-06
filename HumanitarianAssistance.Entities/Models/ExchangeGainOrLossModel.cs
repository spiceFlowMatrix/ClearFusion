using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class ExchangeGainOrLossModel
    {
        public List<TransactionsModel> TransactionsModel { get; set; }
        //public double? Total { get; set; }
    }

    public class TransactionsModel
    {

        public DateTime? TransactionDate { get; set; }
        public int? OriginalCurrency { get; set; }
        public double? TransactionAmount { get; set; }
        public double? OriginalExchangeValue { get; set; }
        public double? CurrentExchangeValue { get; set; }
        public double? GainLossAmount { get; set; }
        public string CreditOrDebit { get; set; }


        //public long ChartOfAccountCode { get; set; }
        //public double? OriginalAmount { get; set; }
        //public double? CurrentAmount { get; set; }
        //public double? Balance { get; set; }
    }
}
