using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities
{
	public partial class BudgetReceivedAmount : BaseEntityWithoutId
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1)]
		public long BudgetReceivedAmountId { get; set; }
		public long BudgetReceivalbeId { get; set; }
		public BudgetReceivable BudgetReceivable { get; set; }
		public long ReceiptId { get; set; }
		public DateTime ReceivedDate { get; set; }
		public double Amount { get; set; }
        public string Remark { get; set; }
	}
}
