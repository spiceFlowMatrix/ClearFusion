using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class LogisticSupplierViewModel
    {
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public long Quantity { get; set; }
        public double FinalPrice { get; set; }
    }
}
