using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class StoreItemGroupModelNew
    {
        public long Id { get; set; }
        public long InventoryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int? ItemTypeCategory { get; set; }
        public List<StoreInventoryItemModelNew> children { get; set; }



    }
}
