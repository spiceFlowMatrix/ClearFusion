using DataAccess.DbEntities.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class ItemSpecificationMaster:BaseEntityWithoutId
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1, TypeName = "serial")]
		public int ItemSpecificationMasterId { get; set; }
		public string ItemSpecificationField { get; set; }
		public int OfficeId { get; set; }
		public OfficeDetail OfficeDetail { get; set; }
		public int ItemTypeId { get; set; }
		[ForeignKey("ItemTypeId")]
		public InventoryItemType InventoryItemType { get; set; }

	}
}
