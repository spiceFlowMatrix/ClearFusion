using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Marketing
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

    public class JobPriceModel
    {
        public string CreatedBy { get; set; }
        public string ClientName { get; set; }
        public long? ClientId { get; set; }
        public long? ContractId { get; set; }
        public long? JobPriceId { get; set; }
        public double UnitRate { get; set; }
        public double FinalPrice { get; set; }
        public double Discount { get; set; }
        public float DiscountPercent { get; set; }
        public double TotalPrice { get; set; }
        public long JobId { get; set; }
        public double FinalRate { get; set; }
        public DateTime EndDate { get; set; }
        public string JobName { get; set; }
        public string JobCode { get; set; }
        public long Minutes { get; set; }
        public bool IsApproved { get; set; }
        public bool IsAgreementApproved { get; set; }
        public string CurrencyCode { get; set; }
    }
}
