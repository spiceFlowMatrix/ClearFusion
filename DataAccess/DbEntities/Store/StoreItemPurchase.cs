using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities.Store
{
    public class StoreItemPurchase : BaseEntityWithoutId
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

        [ForeignKey("InventoryItem")]
        public StoreInventoryItem StoreInventoryItem { get; set; }
        [ForeignKey("Currency")]
        public CurrencyDetails CurrencyDetails { get; set; }
        [ForeignKey("PurchasedById")]
        public EmployeeDetail EmployeeDetail { get; set; }
        [ForeignKey("UnitType")]
        public PurchaseUnitType PurchaseUnitType { get; set; }

        public List<StorePurchaseOrder> PurchaseOrders { get; set; }
    }
}
