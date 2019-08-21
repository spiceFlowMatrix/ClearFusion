using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.Configuration.Models
{
    public class CurrencyModel: BaseModel
    {
        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public float? CurrencyRate { get; set; }
		public bool? Status { get; set; }
    }
}