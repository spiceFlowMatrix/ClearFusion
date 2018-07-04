using System;

namespace HumanitarianAssistance.ViewModels.Models.Store
{
    public class StoreItemPurchaseModel : BaseModel
    {
        public string PurchaseId { get; set; }
        public string SerialNo { get; set; }
        public string ItemId { get; set; }

        public DateTime PurchaseDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public int CurrencyId { get; set; }
        public int UnitTypeId { get; set; }
        public long UnitCost { get; set; }
        public int Quantity { get; set; }

        public bool ApplyDepreciation { get; set; }
        public long DepreciationRate { get; set; }

        public string Image { get; set; }
        public string ImageName { get; set; }
        public string ImageFileType { get; set; }

        public int PurchasedById { get; set; }
    }

    public class StoreItemPurchaseViewModel : BaseModel
    {
        public string PurchaseId { get; set; }
        public string SerialNo { get; set; }                    // Barcode Value
        public string InventoryItem { get; set; }               // Item Id
        public DateTime PurchaseDate { get; set; }              // Date Of Purchase
        public DateTime DeliveryDate { get; set; }              // The date that the item arrived at it's desired location or a service took place.		
        public int Currency { get; set; }                       // Currency ID
        public int UnitType { get; set; }
        public long UnitCost { get; set; }
        public int Quantity { get; set; }
        public bool ApplyDepreciation { get; set; }
        public double DepreciationRate { get; set; }
        public string ImageFileName { get; set; }               // Image String
        public string Invoice { get; set; }                     // Invoice String
        public int PurchasedById { get; set; }
        public string ItemId { get; set; }
        public long TotalCost { get; set; }
        public int CurrentQuantity { get; set; }
        public int ItemType { get; set; }
        public int PurchasedBy { get; set; }

        //Newly Added Fields
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
    }

    public class ItemPurchaseWithDataModel
    {
        public StoreItemPurchaseModel Purchase { get; set; }
        public PurchaseData Data { get; set; }
    }

    public class PurchaseData
    {
        public PurchaseVehicleModel Vehicle { get; set; }
    }

    public class StoreItemPurchaseDocumentModel : BaseModel
    {
        public string PurchaseId { get; set; }
        public string DocumentName { get; set; }
        public string File { get; set; }
        public string FileType { get; set; }
    }
}
