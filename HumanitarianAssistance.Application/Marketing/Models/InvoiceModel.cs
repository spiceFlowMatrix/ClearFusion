using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Models
{
    public class InvoiceModel
    {
        public long? InvoiceId { get; set; }
        public long JobId { get; set; }
        public long? TotalRunningMinutes { get; set; }
        public long? TotalMinutes { get; set; }
        public double JobRate { get; set; }
        public double? FinalPrice { get; set; }
        public string JobName { get; set; }
        public string ClientName { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsApproved { get; set; }
        public bool IsScheduleExist { get; set; }
    }
}
