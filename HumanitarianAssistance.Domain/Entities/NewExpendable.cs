using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities
{
    public class NewExpendable : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int StoreItemID { get; set; }

        [StringLength(255)]
        public string StoreItemCode { get; set; }
        [StringLength(255)]
        public string MasterCode { get; set; }
        [StringLength(255)]
        public string StoreDescription { get; set; }
        [StringLength(255)]
        public string OfficeCode { get; set; }
        [StringLength(255)]
        public string Bline { get; set; }
        [StringLength(255)]
        public string Area { get; set; }
        public float? Sector { get; set; }
        [StringLength(255)]
        public string CostBook { get; set; }
        public float? Project { get; set; }
        [StringLength(255)]
        public string Program { get; set; }
        public float? Job { get; set; }
        public string VoucherNo { get; set; }
        [StringLength(255)]
        public string VoucherDate { get; set; }
        [StringLength(255)]
        public string PurchaseOrderNo { get; set; }
        [StringLength(255)]
        public string PurchaseOrderDate { get; set; }
        [StringLength(255)]
        public string ModelType { get; set; }
        [StringLength(255)]
        public string Maker { get; set; }
        [StringLength(255)]
        public string EngineNo { get; set; }
        [StringLength(255)]
        public string IdentificationNo { get; set; }
        [StringLength(255)]
        public string ChasisNo { get; set; }
        [StringLength(255)]
        public string RegistrationNo { get; set; }
        [StringLength(255)]
        public string InvoiceNo { get; set; }
        [StringLength(255)]
        public string InvoiceDate { get; set; }
        [StringLength(255)]
        public string AssetType { get; set; }
        [StringLength(255)]
        public string Unit { get; set; }
        public float? Quantity { get; set; }
        [StringLength(255)]
        public string CurrencyCode { get; set; }
        public float? Price { get; set; }
        [StringLength(255)]
        public string ReceiptVoucherNo { get; set; }
        public DateTime? ReceiptVoucherDate { get; set; }
        [StringLength(255)]
        public string ReceiptFromEmployeeID { get; set; }
        [StringLength(255)]
        public string ReceiptFromLocation { get; set; }
        [StringLength(255)]
        public string IssueVoucherNo { get; set; }
        public DateTime? IssueVoucherDate { get; set; }
        [StringLength(255)]
        public string IssuedToEmployeeID { get; set; }
        [StringLength(255)]
        public string IssuedToLocation { get; set; }
        [StringLength(255)]
        public string ReceiveType { get; set; }
        [StringLength(255)]
        public string Status { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
    }
}
