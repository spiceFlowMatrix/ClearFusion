using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.SPModels
{
    public class SPProjectProposalReportModel
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public int ProjectCurrencyId { get; set; }
        public double BudgetEstimation { get; set; }
    }
}
