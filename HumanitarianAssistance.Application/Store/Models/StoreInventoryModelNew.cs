using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Models
{
   public class StoreInventoryModelNew
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AssetType { get; set; }
        public long? InventoryDebitAccount { get; set; }
        public long? InventoryCreditAccount { get; set; }

        public List<StoreItemGroupModelNew> children { get; set; }


    }

}
