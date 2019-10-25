using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Store
{
   
    public class StoreItemGroup : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long ItemGroupId { get; set; }
        public string ItemGroupCode { get; set; }
        public string ItemGroupName { get; set; }
        public string Description { get; set; }
        public long InventoryId { get; set; }
        [ForeignKey("InventoryId")]
        public StoreInventory StoreInventory { get; set; }
    }
}
