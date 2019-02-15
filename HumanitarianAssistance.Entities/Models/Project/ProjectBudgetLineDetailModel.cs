using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
   public class ProjectBudgetLineDetailModel
    {
        public long BudgetLineId { get; set; }
        public string BudgetCode { get; set; }
        public string BudgetName { get; set; }
        public double? InitialBudget { get; set; }
        public long? ProjectId { get; set; }
        public long? ProjectJobId { get; set; }
        public string ProjectJobName { get; set; }
        public  string ProjectJobCode { get; set; }
        public int? CurrencyId { get; set; }
        public string  CurrencyName { get; set; }
       

    }
}
