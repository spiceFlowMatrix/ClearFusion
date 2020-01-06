using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class LogisticSupplierViewModel
    {
        public long SupplierId { get; set; }
        public string StoreSourceDescription { get; set; }
        public long SourceCodeId { get; set; }
        public string SourceCode { get; set; }
        public long Quantity { get; set; }
        public double FinalUnitPrice { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public string InvoiceUrl { get; set; }
        public string InvoiceName { get; set; }
        public string WarrantyUrl { get; set; }
        public string WarrantyName { get; set; }
        public string ItemCode { get; set; }
        public string CurrencyCode { get; set; }
    }
}
