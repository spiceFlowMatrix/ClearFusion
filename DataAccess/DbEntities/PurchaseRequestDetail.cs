using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class PurchaseRequestDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int PurchaseRequestID { get; set; }
        public int? ProjectNumber { get; set; }
        [StringLength(50)]
        public string Item { get; set; }
        public string ItemSpecification { get; set; }
        [StringLength(50)]
        public string Unit { get; set; }
        public float? UnitCost { get; set; }
        public DateTime? StartDate { get; set; }
        [StringLength(10)]
        public string OfficeCode { get; set; }
        [StringLength(10)]
        public string BudgetLine { get; set; }
        [StringLength(10)]
        public string Job { get; set; }
        [StringLength(20)]
        public string PurchaseOrderNo { get; set; }
        public float? AvailableItems { get; set; }
        public float? ItemsToPurchased { get; set; }
        public byte? Status { get; set; }
        public byte? ApprovalType { get; set; }
        public string Attachments { get; set; }
        public string Comment { get; set; }
        [StringLength(100)]
        public string GoodReceivedAttachment { get; set; }
        public string ControlComments { get; set; }
        public string ControlPurchaseOrderComments { get; set; }
        public string PurchaseOrderComments { get; set; }
        public byte? PurchaseType { get; set; }
        public Boolean? IsShowInAvailableItems { get; set; }
        [StringLength(5)]
        public string CurrencyCode { get; set; }
        [StringLength(200)]
        public string AnnouncementAttachment { get; set; }
        [StringLength(200)]
        public string DistributionAttachment { get; set; }
        [StringLength(200)]
        public string OfferAttachment { get; set; }
    }
}
