using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.DbEntities.Project;

namespace DataAccess.DbEntities
{
	public partial class ProjectBudgetLine : BaseEntityWithoutId
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1)]
		public long BudgetLineId { get; set; }
		public string Description { get; set; }
		public long ProjectId { get; set; }
		public ProjectDetails ProjectDetails { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public double AmountReceivable { get; set; }
		public double AmountPayable { get; set; }
		public int? BudgetLineTypeId { get; set; }
        public BudgetLineType BudgetLineType { get; set; }
	}
}
