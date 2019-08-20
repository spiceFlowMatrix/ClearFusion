namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class ExchangeRateDetailViewModel
    {
        public long ExchangeRateId { get; set; }
        public int FromCurrency { get; set; }
        public int ToCurrency { get; set; }
        public double Rate { get; set; }
    }
}