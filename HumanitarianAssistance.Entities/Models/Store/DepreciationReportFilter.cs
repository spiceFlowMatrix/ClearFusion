using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Store
{
    public class DepreciationReportFilter
    {
        //Must filter on store name, inventory, item, and purchase

        public int? StoreId { get; set; }
        public string InventoryId { get; set; }
        public string ItemId { get; set; }
        public string PurchaseId { get; set; }
        public long ItemGroupId { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}
