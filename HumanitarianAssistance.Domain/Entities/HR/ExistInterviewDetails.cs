using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class ExistInterviewDetails : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int ExistInterviewDetailsId { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public EmployeeDetail EmployeeDetail { get; set; }

        public string DutiesOfJob { get; set; }
        public string TrainingAndDevelopmentPrograms { get; set; }
        public string OpportunityAdvancement { get; set; }
        public string SalaryTreatment { get; set; }
        public string BenefitProgram { get; set; }
        public string WorkingConditions { get; set; }
        public string WorkingHours { get; set; }
        public string CoWorkers { get; set; }
        public string Supervisors { get; set; }
        public string GenderFriendlyEnvironment { get; set; }
        public string OverallJobSatisfaction { get; set; }

        public bool Benefits { get; set; }
        public bool BetterJobOpportunity { get; set; }
        public bool FamilyReasons { get; set; }
        public bool NotChallenged { get; set; }
        public bool Pay { get; set; }
        public bool PersonalReasons { get; set; }
        public bool Relocation { get; set; }
        public bool ReturnToSchool { get; set; }
        public bool ConflictWithSuoervisors { get; set; }
        public bool ConflictWithOther { get; set; }
        public bool WorkRelationship { get; set; }
        public bool CompanyInstability { get; set; }
        public bool CareerChange { get; set; }
        public bool HealthIssue { get; set; }

        public string HadGoodSynergy { get; set; }
        public string HadAdequateEquipment { get; set; }
        public string WasAdequatelyStaffed { get; set; }
        public string WasEfficient { get; set; }

        public string JobWasChallenging { get; set; }
        public string SkillsEffectivelyUsed { get; set; }
        public string JobOrientation { get; set; }
        public string WorkLoadReasonable { get; set; }
        public string SufficientResources { get; set; }
        public string WorkEnvironment { get; set; }
        public string ComfortableAppropriately { get; set; }
        public string Equipped { get; set; }

        public string HadKnowledgeOfJob { get; set; }
        public string HadKnowledgeSupervision { get; set; }
        public string WasOpenSuggestions { get; set; }
        public string RecognizedEmployeesContribution { get; set; }

        public string GaveFairTreatment { get; set; }
        public string WasAvailableToDiscuss { get; set; }
        public string WelcomedSuggestions { get; set; }
        public string MaintainedConsistent { get; set; }
        public string ProvidedRecognition { get; set; }
        public string EncouragedCooperation { get; set; }
        public string ProvidedDevelopment { get; set; }

        public bool Question { get; set; }
        public string Explain { get; set; }
    }
}
