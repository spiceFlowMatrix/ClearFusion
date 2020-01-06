using System;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class ProcurementDetailModel
    {
        public long OrderId { get; set; }
        public long PurchaseId { get; set; }
        public long ItemId { get; set; }
        public int IssuedQuantity { get; set; }
        public bool MustReturn { get; set; }
        public int IssuedToEmployeeId { get; set; }
        public DateTime IssueDate { get; set; }
        public long? VoucherNo { get; set; }
        public long ProjectId { get; set; }
        public long IssedToLocation { get; set; }
        public long StatusAtTimeOfIssue { get; set; }
        public int? OfficeId { get; set; }
        public int RemainingQuantity { get; set; }
    }
}