using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities
{
    public class StoreInventory : BaseEntityWithoutId
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
		public int InventoryDebitAccount { get; set; }
		[ForeignKey("InventoryDebitAccount")]
        public ChartAccountDetail ChartDebitAccountDetails { get; set; }

		public int? InventoryCreditAccount { get; set; }
		[ForeignKey("InventoryCreditAccount")]
		public ChartAccountDetail ChartCreditAccountDetails { get; set; }

		public List<StoreInventoryItem> InventoryItems { get; set; }
    }
}