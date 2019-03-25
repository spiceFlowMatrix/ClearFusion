using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    #region "Cash Flow"
    public class ProjectCashFlowFilterModel
    {
        public long ProjectId { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public long DonorID { get; set; }
    }
    public class ProjectCashFlowModel
    {
        public double? CreditList { get; set; }
        public double? DebitList { get; set; }
        public string Date { get; set; }
    }

    #endregion

    #region "Budget Line Breakdown"

    public class BudgetLineBreakdownFilterModel
    {
        BudgetLineBreakdownFilterModel()
        {
            BudgetLineId = new List<long?>();
        }

        public long? ProjectId { get; set; }
        public List<long?> BudgetLineId { get; set; }
        public DateTime? BudgetLineStartDate { get; set; }
        public DateTime? BudgetLineEndDate { get; set; }
    }
    public class BudgetLineBreakdownModel
    {
        public long? ProjectId { get; set; }
        public double? Debit { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Date { get; set; }
    }

    public class BudgetLineBreakdownListModel
    {
        public BudgetLineBreakdownListModel()
        {
            DebitTotal = 0.00;
        }
        public string Date { get; set; }
        public double? DebitTotal { get; set; }
    }
    #endregion


}
