using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class BudgetLineBreakdownModel
    {
        public List<double> Expenditure { get; set; }
        public List<DateTime> Date { get; set; }
        public List<double?> TotalExpectedBudget { get; set; }
    }
}
