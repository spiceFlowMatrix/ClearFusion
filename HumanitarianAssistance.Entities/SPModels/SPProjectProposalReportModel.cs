using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.SPModels
{
    public class SPProjectProposalReportModel
    {
        public string ProjectCode { get; set; }
        public string ProjectsName { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public int ProjectCurrencyId { get; set; }
        public double BudgetEstimation { get; set; }
        public double Progress { get; set; }
        public int ColorCode { get; set; }
        public DateTime? ReviewCompletionDate { get; set; }
        public int DueDays { get; set; }
    }

    public class SPProjectProposalReportAmountSummaryModel
    {
        public double ProjectAmount { get; set; }
        public int ProjectCurrency { get; set; }
    }
}
