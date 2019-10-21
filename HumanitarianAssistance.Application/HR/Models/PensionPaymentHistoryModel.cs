using System;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class PensionPaymentHistoryModel
    {
        public string Employee { get; set; }
        public decimal PensionPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public long VoucherNo { get; set; }
        public string VoucherReferenceNo { get; set; }
    }
}