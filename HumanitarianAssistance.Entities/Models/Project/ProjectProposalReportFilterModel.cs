using System;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public class ProjectProposalReportFilterModel
    {
        public string ProjectName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int StartDateFilterOption { get; set; }
        public int DueDateFilterOption { get; set; }
        public int CurrencyId { get; set; }
        public double Amount { get; set; }
        public int AmountFilterOption { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsLate { get; set; }
    }
}
