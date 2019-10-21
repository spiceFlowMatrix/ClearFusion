using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class ItemSpecificationMaster : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int ItemSpecificationMasterId { get; set; }
        public string ItemSpecificationField { get; set; }
        public int OfficeId { get; set; }
        public OfficeDetail OfficeDetail { get; set; }
        public int ItemTypeId { get; set; }
        [ForeignKey("ItemTypeId")]
        public InventoryItemType InventoryItemType { get; set; }

    }
}
