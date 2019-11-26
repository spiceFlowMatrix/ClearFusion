using System;

namespace HumanitarianAssistance.Application.Project.Models {
    public class ProjectHiringRequestDetailModel {
        public long? HiringRequestId { get; set; }
        public long? ProjectId { get; set; }
        public long? JobCategory { get; set; }
        public string MinEducationLevel { get; set; }
        public int? TotalVacancy { get; set; }
        public int? Position { get; set; }
        public string Organization { get; set; }
        public int? Office { get; set; }
        public string ContractType { get; set; }
        public int? ContractDuration { get; set; }
        public int? Gender { get; set; }
        public int? Nationality { get; set; }
        public string SalaryRange { get; set; }
        public DateTime? AnouncingDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public int? Country { get; set; }
        public int? Province { get; set; }
        public int? JobShift { get; set; }
        public string JobStatus { get; set; }
        public string Experience { get; set; }
        public string Background { get; set; }
        public string SpecificDutiesAndResponsibilities { get; set; }
        public string KnowledgeAndSkillsRequired { get; set; }
        public string SubmissionGuidelines { get; set; }
        public int? PayCurrency { get; set; }
        public long? BudgetLine { get; set; }
        public int JobType { get; set; }
        public int? JobGrade { get; set; }
        public int? EducationDegree { get; set; }
        public int? Profession { get; set; }
        public double? PayHourlyRate { get; set; }
    }
}