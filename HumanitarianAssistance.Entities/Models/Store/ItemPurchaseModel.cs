using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Store
{
    public class ItemPurchaseModel: BaseModel
    {
		public string PurchaseId { get; set; }					
		public string SerialNo { get; set; }					// Barcode Value
		public string InventoryItem { get; set; }				// Item Id
		public DateTime PurchaseDate { get; set; }				// Date Of Purchase
		public DateTime DeliveryDate { get; set; }				// The date that the item arrived at it's desired location or a service took place.		
		public int Currency { get; set; }						// Currency ID
		public int UnitType { get; set; }						
		public long UnitCost { get; set; }
		public int Quantity { get; set; }
		public bool ApplyDepreciation { get; set; }
		public double DepreciationRate { get; set; }		
		public string ImageFileName { get; set; }               // Image String
		public string InvoiceFileName { get; set; }             // Invoice String
		public int PurchasedById { get; set; }					// Employee ID
	}
}
