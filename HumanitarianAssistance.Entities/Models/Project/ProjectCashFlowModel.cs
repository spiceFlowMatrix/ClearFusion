using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    #region "Cash Flow"
    public class ProjectCashFlowFilterModel
    {
        public long ProjectId { get; set; }
        public int CurrencyId { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public long DonorID { get; set; }
    }
    public class ProjectCashFlowModel
    {
        public List<DateTime> Date { get; set; }
        public List<double> Expenditure { get; set; }
        public List<double> Income { get; set; }
    }

    #endregion

    #region "Budget Line Breakdown"

    public class BudgetLineBreakdownFilterModel
    {
        BudgetLineBreakdownFilterModel()
        {
            BudgetLineId = new List<long>();
        }

        public long ProjectId { get; set; }
        public List<long> BudgetLineId { get; set; }
        public DateTime BudgetLineStartDate { get; set; }
        public DateTime BudgetLineEndDate { get; set; }
        public int CurrencyId { get; set; }
    }
    public class BudgetLineBreakdownModel
    {
        public List<double> Expenditure { get; set; }
        public List<DateTime> Date { get; set; }
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
