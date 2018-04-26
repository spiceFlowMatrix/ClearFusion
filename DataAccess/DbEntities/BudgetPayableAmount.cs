using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class BudgetPayableAmount:BaseEntityWithoutId
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1)]
		public long BudgetPayableAmountId { get; set; }
		public long BudgetPayableId { get; set; }
		public BudgetPayable BudgetPayable { get; set; }
		public DateTime PaymentDate { get; set; }
		public double Amount { get; set; }
        public string Remark { get; set; }

	}
}
