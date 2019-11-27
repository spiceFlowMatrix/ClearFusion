using System;

namespace HumanitarianAssistance.Application.Project.Models {
    public class HiringRequestDetailsModel {
        public int? OfficeId { get; set; }
        public int? DesignationId { get; set; }
        public string Office { get; set; }
        public string Position { get; set; }
        public string JobGrade { get; set; }
        public int? TotalVacancy { get; set; }
        public int? FilledVacancy { get; set; }
        public string PayCurrency { get; set; }
        public double? PayHourlyRate { get; set; }
        public string BudgetLine { get; set; }
        public int? JobType { get; set; }
        public DateTime? AnouncingDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string ContractType { get; set; }
        public int? ContractDuration { get; set; }
        public string JobShift { get; set; }
        public string Profession { get; set; }
        public string KnowledgeAndSkillsRequired { get; set; }
        public string EducationDegree { get; set; }
        public string TotalExperienceInYear { get; set; }
    }
}