using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels
{
    public class ProjectBudgetLinesModel
    {
        public IList<ProjectBudgetModelNew> ProjectList { get; set; }
        
        public IList<BudgetLineModel> BudgetLines { get; set; } 

    }

    public class  ProjectBudgetModelNew
    {
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
    }
    public class BudgetLineModel
    {
        public long BudgetLineId { get; set; }
        public string Description { get; set; }
        public long ProjectId { get; set; }

    }
}
