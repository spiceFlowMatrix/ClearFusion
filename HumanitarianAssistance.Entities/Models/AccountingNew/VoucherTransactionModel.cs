using HumanitarianAssistance.ViewModels.Models.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.AccountingNew
{
    public class VoucherTransactionsModel
    {
        public VoucherTransactionsModel()
        {
            BudgetLineList = new List<ProjectBudgetLineDetailModel>();
        }
        public long TransactionId { get; set; }
        public long? AccountNo { get; set; }
        public string Description { get; set; }
        public int? ProjectId { get; set; }
        public int? BudgetLineId { get; set; }
        public long? JobId { get; set; }
        public string JobName { get; set; }
        public double? Credit { get; set; }
        public double? Debit { get; set; }
        public long? VoucherNo { get; set; }
        public bool? IsDeleted { get; set; }
        public List<ProjectBudgetLineDetailModel> BudgetLineList { get; set; }
    }
}
