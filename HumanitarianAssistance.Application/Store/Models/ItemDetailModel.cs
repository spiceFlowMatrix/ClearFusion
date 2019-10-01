using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class ItemDetailModel
    {
        public long ItemId { get; set; }
        public long InventoryTypeId { get; set; }
        public long InventoryId { get; set; }
        public long ItemGroupId { get; set; }
        public string PurchaseName { get; set; }
        public long PurchaseId { get; set; }
    }
}