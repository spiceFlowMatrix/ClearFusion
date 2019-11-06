using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.SPModels
{
    public class ExchangeGainOrLossTransaction
    {
        public Int64 VoucherNo { get; set; }
        public int AccountNo { get; set; }
        public int FinancialYearId { get; set; }
        public DateTime TransactionDate { get; set; }
        public Double  Debit { get; set; }
        public int CurrencyId { get; set; }
    }
}
