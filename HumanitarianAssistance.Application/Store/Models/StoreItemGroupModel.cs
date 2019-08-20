using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class StoreItemGroupModel
    {
        public long ItemGroupId { get; set; }
        public string InventoryId { get; set; }
        public string ItemGroupName { get; set; }
        public string ItemGroupCode { get; set; }
        public string Description { get; set; }
    }
}
