using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Marketing
{
    public class InvoiceModel
    {
        public long JobId { get; set; }
        public long? TotalRunningMinutes { get; set; }
        public long? TotalMinutes { get; set; }
        public double JobRate { get; set; }
        public double? FinalPrice { get; set; }
        public string JobName { get; set; }
        public string ClientName { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime EndDate { get; set; }
    }
}
