using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Marketing
{
    public class InvoiceApprovalModel
    {
        public long InvoiceApprovalId { get; set; }
        public long? JobId { get; set; }
        public bool IsInvoiceApproved { get; set; }
    }
}
