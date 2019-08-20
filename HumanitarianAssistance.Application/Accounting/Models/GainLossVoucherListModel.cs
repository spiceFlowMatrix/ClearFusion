using System;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class GainLossVoucherListModel
    {
        public long VoucherId { get; set; }
        public string VoucherName { get; set; }
        public string JournalName { get; set; }
        public DateTime? VoucherDate { get; set; } 
    }
}