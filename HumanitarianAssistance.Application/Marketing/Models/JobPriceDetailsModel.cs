using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Models
{
    public class JobPriceDetailsModel
    {
        public long JobPriceId { get; set; }
        public double UnitRate { get; set; }
        public int Units { get; set; }
        public double FinalRate { get; set; }
        public double FinalPrice { get; set; }
        public double Discount { get; set; }
        public float DiscountPercent { get; set; }
        public double TotalPrice { get; set; }
        public bool IsInvoiceApproved { get; set; }
        public long JobId { get; set; }
        public long Minutes { get; set; }
    }
}
