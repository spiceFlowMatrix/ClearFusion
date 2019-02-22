using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
   public class BudgetLineFilterModel
    {
        public string FilterValue { get; set; }
        public bool BudgetLineIdFlag { get; set; }
        public bool BudgetCodeFlag { get; set; }
        public bool BudgetNameFlag { get; set; }
        public bool  ProjectJobIdFlag { get; set; }
        public bool InitialBudgetFlag { get; set; }
        public bool DateFlag { get; set; }
        public bool ProjectJobNameFlag { get; set; }


        public int? pageIndex { get; set; }
        public int? pageSize { get; set; }
        public int? totalCount { get; set; }


     
    }
}
