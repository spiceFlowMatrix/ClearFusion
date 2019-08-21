using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class StoreInventoryModel
    {
        public string InventoryId { get; set; }
        public string InventoryCode { get; set; }
        public string InventoryName { get; set; }
        public string InventoryDescription { get; set; }
        public int AssetType { get; set; }
        public long InventoryDebitAccount { get; set; }
        public long? InventoryCreditAccount { get; set; }
    }
}
