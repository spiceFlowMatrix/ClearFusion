using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities
{
    public partial class BudgetReceivable : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long BudgetReceivalbeId { get; set; }
        //public long BudgetId { get; set; }
        public long? ProjectId { get; set; }
        public ProjectDetails ProjectDetails {get;set;}
        //public ProjectBudget ProjectBudget { get; set; }
		public long BudgetLineId { get; set; }
		public ProjectBudgetLine ProjectBudgetLine { get; set; }
		public double Amount { get; set; }
		public DateTime ExpectedDate { get; set; }
        
	}
}
