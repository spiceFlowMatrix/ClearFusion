using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HumanitarianAssistance.Domain.Entities.Accounting;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class StoreInventory : BaseEntity
    {
        public StoreInventory()
        {
            InventoryItems = new List<StoreInventoryItem>();
        }

        [Key]
        public string InventoryId { get; set; }
        public string InventoryCode { get; set; }
        public string InventoryName { get; set; }
        public string InventoryDescription { get; set; }
        //public long InventoryChartOfAccount { get; set; }
        public int AssetType { get; set; }
        public long InventoryDebitAccount { get; set; }
        [ForeignKey("InventoryDebitAccount")]
        public ChartOfAccountNew ChartDebitAccountDetails { get; set; }

        public long? InventoryCreditAccount { get; set; }
        [ForeignKey("InventoryCreditAccount")]
        public ChartOfAccountNew ChartCreditAccountDetails { get; set; }

        public List<StoreInventoryItem> InventoryItems { get; set; }
    }
}