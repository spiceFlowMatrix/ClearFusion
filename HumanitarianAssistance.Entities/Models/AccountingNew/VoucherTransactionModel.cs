using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.AccountingNew
{
    public class VoucherTransactionsModel
    {
        public long TransactionId { get; set; }
        public long? AccountNo { get; set; }
        public string Description { get; set; }
        public int? ProjectId { get; set; }
        public int? BudgetLineId { get; set; }
        public double? Credit { get; set; }
        public double? Debit { get; set; }
        public long? VoucherNo { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
