using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.SPModels
{
    public class SPProjectProposalReportModel
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public float Progress { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public double BudgetEstimate { get; set; }
    }
}
