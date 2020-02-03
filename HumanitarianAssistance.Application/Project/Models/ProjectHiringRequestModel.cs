using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class ProjectHiringRequestModel
    {
       // public string Description { get; set; }
        public string Office { get; set; }
        public int OfficeId { get; set; }
       // public string JobCode { get; set; }
        public string JobGrade { get; set; }
        public string Position { get; set; }
        public int? TotalVacancies { get; set; }
        public int? FilledVacancies { get; set; }
        public string PayCurrency { get; set; }
        public double? PayRate { get; set; }
       // public string Status { get; set; }
        public long? HiringRequestId { get; set; }
        public string HiringRequestCode { get; set; }
        public int? HiringRequestStatus { get; set; }
        public int? JobType { get; set; }
        public string JobCategory { get; set; }
        public string BudgetName { get; set; }
        public long? BudgetLineId { get; set; }
        public string AnouncingDate { get; set; }
        public string ClosingDate { get; set; }
        public int? ContractType { get; set; }
        public int? ContractDuration { get; set; }
        public int? Shift { get; set; }
        public string EducationDegree { get; set; }
        public string Profession { get; set; }
        public string Experience { get; set; }
        public string KnowledgeAndSkills { get; set; }
        public string SpecificDutiesAndResponsibilities { get; set; }
        public string SubmissionGuidelines { get; set; }


    }
}
