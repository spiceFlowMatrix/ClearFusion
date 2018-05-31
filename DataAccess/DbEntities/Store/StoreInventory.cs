using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities
{
    public class StoreInventory : BaseEntityWithoutId
    {
        [Key]
        public string InventoryId { get; set; }
        public string InventoryCode { get; set; }
        public string InventoryDescription { get; set; }
        public int InventoryAccount { get; set; }

        [ForeignKey("InventoryAccount")]
        public ChartAccountDetail ChartAccountDetails { get; set; }

        public List<StoreInventoryItem> InventoryItems { get; set; }
    }
}