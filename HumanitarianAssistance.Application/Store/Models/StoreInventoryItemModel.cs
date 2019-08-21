using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class StoreInventoryItemModel 
    {
        public string ItemId { get; set; }
        public string ItemInventory { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public long ItemGroupId { get; set; }
        public int ItemType { get; set; }
    }
}
