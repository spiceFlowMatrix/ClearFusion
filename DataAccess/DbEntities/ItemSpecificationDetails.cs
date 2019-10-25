using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class ItemSpecificationDetails:BaseEntityWithoutId
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1, TypeName = "serial")]
		public int ItemSpecificationDetailsId { get; set; }
		public int ItemSpecificationMasterId { get; set; }
		public ItemSpecificationMaster ItemSpecificationMaster { get; set; }
		public string ItemId { get; set; }
		public string ItemSpecificationValue { get; set; }
	}
}
