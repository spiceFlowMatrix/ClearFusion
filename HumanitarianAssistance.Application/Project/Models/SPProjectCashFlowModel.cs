using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class SPProjectCashFlowModel
    {
        public DateTime VoucherDate { get; set; }
        public double Expenditure { get; set; }
        public double Income { get; set; }
        public int Currencyid { get; set; }
        public DateTime BudgetLineDate { get; set; }
    }
    public class ProjectExpectedBudget
    {
        public double TotalExpectedProjectBudget { get; set; }
    }
}
