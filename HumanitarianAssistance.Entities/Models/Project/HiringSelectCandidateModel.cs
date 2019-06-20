using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public class HiringSelectCandidateModel
    {
        public int EmployeeId { get; set; }
        public long HiringRequestId { get; set; }
        public long ProjectId { get; set; }
        public long BudgetLineId { get; set; }
    }
}
