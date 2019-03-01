using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.AccountingNew
{
    public class GenerateExchangeRateViewModel
    {
        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set; }
        public double Rate { get; set; }
        public DateTime Date { get; set; }
    }

    public class ExchangeRateDetailModel
    {
        public  DateTime ExchangeRateDate { get; set; }
        public int OfficeId { get; set; }
    }
}
