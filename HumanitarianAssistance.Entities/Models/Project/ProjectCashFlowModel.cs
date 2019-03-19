using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
   public class ProjectCashFlowModel
    {
        public long ProjectId { get; set; }

        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }

        public long DonorID { get; set; }
    }
    public class BudgetLineCashFlowModel
    {
        public int? ProjectId { get; set; }

        public DateTime? BudgetLineStartDate { get; set; }
        public DateTime? BudgetLineEndDate { get; set; }

        public long? BudgetLieID { get; set; }
        public double? Debit { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Month { get; set; }
    }

    public class BLTransactionCashFlowModel
    {
        public BLTransactionCashFlowModel()
        {
            DebitList = new List<BudgetLineCashFlowModel>();
        }
        public string Month { get; set; }
        public List<BudgetLineCashFlowModel> DebitList { get; set; }
    }
}
