using System;

namespace HumanitarianAssistance.Application.Project.Models {
    public class HiringRequestDetailsModel {
        public string Office { get; set; }
        public string Position { get; set; }
        public string JobGrade { get; set; }
        public int? TotalVacancy { get; set; }
        public int? FilledVacancy { get; set; }
        public string PayCurrency { get; set; }
        public double? PayHourlyRate { get; set; }
        public string BudgetLine { get; set; }
        public string JobType { get; set; }
        public DateTime? AnouncingDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string ContractType { get; set; }
        public int? ContractDuration { get; set; }
        public string JobShift { get; set; }
    }
}