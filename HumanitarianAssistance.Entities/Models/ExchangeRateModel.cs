using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class ExchangeRateModel : BaseModel
    {
        public long ExchangeRateId { get; set; }
        public DateTime Date { get; set; }
        public int FromCurrency { get; set; }
        public int ToCurrency { get; set; }
        public double Rate { get; set; }
        public string FromCurrencyName { get; set; }
        public string ToCurrencyName { get; set; }
    }
}
