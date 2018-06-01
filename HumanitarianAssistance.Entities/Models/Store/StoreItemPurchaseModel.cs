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
        public string SerialNo { get; set; }
        public string ItemId { get; set; }

        public DateTime PurchaseDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public string UnitType { get; set; }
        public string Currency { get; set; }
        public long UnitCost { get; set; }
        public int Quantity { get; set; }
        public long TotalCost { get; set; }
        public int CurrentQuantity { get; set; }

        public string ItemType { get; set; }

        public bool ApplyDepreciation { get; set; }

        public string ImageFileName { get; set; }

        public string PurchasedBy { get; set; }
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
