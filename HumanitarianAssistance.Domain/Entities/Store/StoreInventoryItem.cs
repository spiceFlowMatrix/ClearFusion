using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    // when creating inventory items, a type must be specified for it.
    // general type items are just base items, but the other item types
    // will involve some additional logic to create entities for those items as
    // well. creating purchases for things like vehicles/generators/fuel/etc will
    // involve creating entries for those items as well in the database
    // At the moment, these non-general item types have only to do with the 
    // transport module but we might be adding more specific item types later
    // and those item types may involve additional logic on top of purchases and orders
    // that is specific to that item type.
    // Please check the readmes in the StoreService class, I've written comments comments 
    // and added code snippets to further clarify what to do with specific item types
    public class StoreInventoryItem : BaseEntity
    {
        [Key]
        public string ItemId { get; set; }
        public string ItemInventory { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public long? ItemGroupId { get; set; }
        [ForeignKey("ItemGroupId")]
        public StoreItemGroup StoreItemGroup { get; set; }

        //public long Voucher { get; set; }
        public int ItemType { get; set; } // This must be used by the front-end to determine the UI to present for the item type
                                          // Types will include general purchase, vehicle, generator, fuel, maintenance, spare parts
                                          // General purchase will cover most items in the store, the additional types is for generators and vehicles management

        public string MasterInventoryCode { get; set; }

        [ForeignKey("ItemInventory")]
        public StoreInventory Inventory { get; set; }
        //[ForeignKey("Voucher")]
        //      public VoucherDetail VoucherDetail { get; set; }
        [ForeignKey("ItemType")]
        public InventoryItemType ItemTypes { get; set; }

        public List<StoreItemPurchase> StoreItemPurchases { get; set; }

    }
}