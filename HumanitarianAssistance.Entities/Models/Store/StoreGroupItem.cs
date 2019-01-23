using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Store
{
    public class StoreItemGroupModel
    {
        public long GroupItemId { get; set; }
        public string InventoryId { get; set; }
        public string GroupItemName { get; set; }
        public string GroupCode { get; set; }
        public string Description { get; set; }
    }
}
