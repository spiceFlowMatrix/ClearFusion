using System;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class GenerateExchangeRateModel
    {
        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set; }
        public double Rate { get; set; }
        public DateTime Date { get; set; }
    }
}