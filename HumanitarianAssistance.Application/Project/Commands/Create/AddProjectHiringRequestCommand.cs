using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create {
    public class AddProjectHiringRequestCommand : BaseModel, IRequest<ApiResponse> {
        public string HiringRequestCode { get; set; }
        public int? Position { get; set; }
        public int? TotalVacancy { get; set; }
        public int? Office { get; set; }
        public int? GradeId { get; set; }
        public long? ProjectId { get; set; }
        public int? JobCategory { get; set; }
        public string MinEducationLevel { get; set; }
        public string Organization { get; set; }
        public int? ProvinceId { get; set; }
        public int? ContractType { get; set; }
        public int? ContractDuration { get; set; }
        public int? Gender { get; set; }
        public string SalaryRange { get; set; }
        public DateTime? AnouncingDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public int? Country { get; set; }
        // public string JobType { get; set; }
        public int? JobShift { get; set; }
        public int? PayCurrency { get; set; }
        public string JobStatus { get; set; }
        public string Experience { get; set; }
        public string Background { get; set; }
        public int? Nationality { get; set; }
        public string SpecificDutiesAndResponsibilities { get; set; }
        public string KnowledgeAndSkillsRequired { get; set; }
        public string SubmissionGuidelines { get; set; }
        public int HiringRequestStatus { get; set; }
        public long? BudgetLine { get; set; }
        public int JobType { get; set; }
        public int JobGrade { get; set; }
        public int? EducationDegree { get; set; }
        public int? Profession { get; set; }
        public double? PayHourlyRate { get; set; }
    }
}