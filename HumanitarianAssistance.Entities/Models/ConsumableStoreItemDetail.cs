using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Entities.Models
{
    public partial class ConsumableStoreItemDetail
    {
        public long StoreItemId { get; set; }
        public string StoreItemCode { get; set; }
        public string OfficeCode { get; set; }
        public string Bline { get; set; }
        public string Area { get; set; }
        public string Sector { get; set; }
        public string CostBook { get; set; }
        public string Project { get; set; }
        public string Program { get; set; }
        public string Job { get; set; }
        public string VoucherNo { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string PurchaseOrderNo { get; set; }
        public DateTime? PurchaseOrderDate { get; set; }
        public string ModelType { get; set; }
        public string Maker { get; set; }
        public string EngineNo { get; set; }
        public string IdentificationNo { get; set; }
        public string ChasisNo { get; set; }
        public string RegistrationNo { get; set; }
        public string InvoiceNo { get; set; }
        public string AssetType { get; set; }
        public float? Amount { get; set; }
        public string Unit { get; set; }
        public float? Quantity { get; set; }
        public float? ExistingQuantityAmount { get; set; }
        public float? ExistingQuantity { get; set; }
        public float? IssuedQuantity { get; set; }
        public string CurrencyCode { get; set; }
        public float? Price { get; set; }
        public string ReceiptVoucherNo { get; set; }
        public DateTime? ReceiptVoucherDate { get; set; }
        public long? ReceiptFromEmployeeId { get; set; }
        public string ReceiptFromLocation { get; set; }
        public string IssueVoucherNo { get; set; }
        public DateTime? IssueVoucherDate { get; set; }
        public long? IssuedToEmployeeId { get; set; }
        public string IssuedToLocation { get; set; }
        public string ReceivedType { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
}
