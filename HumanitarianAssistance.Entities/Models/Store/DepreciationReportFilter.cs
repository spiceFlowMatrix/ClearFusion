using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Store
{
    public class DepreciationReportFilter
    {
        //Must filter on store name, inventory, item, and purchase

        public int? StoreId { get; set; }
        public long InventoryId { get; set; }
        public long ItemId { get; set; }
        public long PurchaseId { get; set; }
        public long ItemGroupId { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}
