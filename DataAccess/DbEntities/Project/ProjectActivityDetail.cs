using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Project
{
    public class ProjectActivityDetail : BaseEntityWithoutId
    {
        public ProjectActivityDetail()
        {
            Recurring = false;
            RecurringCount = 0;
            ImplementationProgress = 0;
            ImplementationStatus = false;

            MonitoringProgress = 0;
            MonitoringStatus = false;
            MonitoringScore = 0;
            MonitoringFrequency = 0;
        }
        //Planning
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? BudgetLineId { get; set; }
        [ForeignKey("BudgetLineId")]
        public ProjectBudgetLineDetail ProjectBudgetLineDetail { get; set; }
        public int? EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public EmployeeDetail EmployeeDetail { get; set; }
        public int? OfficeId { get; set; }
        [ForeignKey("OfficeId")]

        public OfficeDetail OfficeDetail { get; set; }
        public int? StatusId { get; set; }
        [ForeignKey("StatusId")]
        public ActivityStatusDetail ActivityStatusDetail { get; set; }
        public bool? Recurring { get; set; }
        public int? RecurringCount { get; set; }
        public int RecurrinTypeId { get; set; }
        //Implementation
        [Range(0, 100)]
        public float? ImplementationProgress { get; set; }
        public bool? ImplementationStatus { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public string ImplementationMethod { get; set; }
        public string ImplementationChalanges { get; set; }
        public string OvercomingChallanges { get; set; }
        public DateTime? ExtensionStartDate { get; set; }
        public DateTime? ExtensionEndDate { get; set; }

        //Monitoring
        [Range(0, 100)]
        public float? MonitoringProgress { get; set; }
        public bool? MonitoringStatus { get; set; }

        public int? MonitoringScore { get; set; }

        public int? MonitoringFrequency { get; set; }
        public string VerificationSource { get; set; }

        public string Strengths { get; set; }

        public string Weeknesses { get; set; }
        public string MonitoringChallenges { get; set; }
        public string Recommendation { get; set; }
        public string Comments { get; set; }
    }
}
