using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Domain.Entities.Project;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class StoreItemPurchase : BaseEntity
    {
        [Key]
        public string PurchaseId { get; set; }
        [Required]
        public string SerialNo { get; set; }
        [Required]
        public string InventoryItem { get; set; }

        public DateTime PurchaseDate { get; set; }
        public DateTime DeliveryDate { get; set; } // The date that the item arrived at it's desired location or a service took place.

        [Required]
        public int Currency { get; set; }
        public int UnitType { get; set; }
        public long UnitCost { get; set; }
        public int Quantity { get; set; }

        public bool ApplyDepreciation { get; set; }
        public double DepreciationRate { get; set; }

        public string ImageFileType { get; set; }
        public string ImageFileName { get; set; }

        public string InvoiceFileType { get; set; }
        public string InvoiceFileName { get; set; }

        public int PurchasedById { get; set; }

        public long? VoucherId { get; set; }
        public DateTime VoucherDate { get; set; }
        public int? AssetTypeId { get; set; } // 1. Cash , 2. In Kind
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int? Status { get; set; }
        public int? ReceiptTypeId { get; set; }
        public string ReceivedFromLocation { get; set; }
        public long? ProjectId { get; set; }

        public long? BudgetLineId { get; set; }
        public int? PaymentTypeId { get; set; }
        public bool IsPurchaseVerified { get; set; } = false;
        public long? VerifiedPurchaseVoucher { get; set; }
        public int? OfficeId { get; set; }
        public int? JournalCode { get; set; }


        [ForeignKey("InventoryItem")]
        public StoreInventoryItem StoreInventoryItem { get; set; }
        [ForeignKey("Currency")]
        public CurrencyDetails CurrencyDetails { get; set; }
        [ForeignKey("PurchasedById")]
        public EmployeeDetail EmployeeDetail { get; set; }
        [ForeignKey("UnitType")]
        public PurchaseUnitType PurchaseUnitType { get; set; }

        [ForeignKey("VoucherId")]
        public VoucherDetail VoucherDetail { get; set; }

        [ForeignKey("Status")]
        public StatusAtTimeOfIssue StatusAtTimeOfIssue { get; set; }

        [ForeignKey("ReceiptTypeId")]
        public ReceiptType ReceiptType { get; set; }

        [ForeignKey("ProjectId")]
        public ProjectDetail ProjectDetail { get; set; }


        public List<StorePurchaseOrder> PurchaseOrders { get; set; }
    }
}
