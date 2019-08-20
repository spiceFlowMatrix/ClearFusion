using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Models
{
    public class JobPriceModel
    {
        public long JobId { get; set; }
        public string JobName { get; set; }
        public string JobCode { get; set; }
        public double UnitRate { get; set; }
        public double FinalRate { get; set; }
        public double FinalPrice { get; set; }
        public double TotalPrice { get; set; }
        public double Discount { get; set; }
        public float DiscountPercent { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsAgreementApproved { get; set; }
        public long? ClientId { get; set; }
        public string ClientName { get; set; }
        public long? JobPriceId { get; set; }
        public string CurrencyCode { get; set; }
        public long? ContractId { get; set; }
        public long Minutes { get; set; }
        public bool IsApproved { get; set; }
    }
}
