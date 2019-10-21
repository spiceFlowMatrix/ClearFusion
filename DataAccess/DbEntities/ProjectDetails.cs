using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class ProjectDetails : BaseEntityWithoutId
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1)]
		public long ProjectId { get; set; }
		[MaxLength(50)]
		[Required]
		public string ProjectName { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int? CurrencyId { get; set; }
		public double? Budget { get; set; }
		public double? ReceivableAmount { get; set; }
		public double? PayableAmount { get; set; }
		public double? CurrentBalance { get; set; }
		public int? Status { get; set; }		
	}
	
}
