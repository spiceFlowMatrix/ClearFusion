using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class UpdatePurchaseInvoiceModel
    {
        public long PurchaseId { get; set; }
        public string Invoice { get; set; }
    }
}
