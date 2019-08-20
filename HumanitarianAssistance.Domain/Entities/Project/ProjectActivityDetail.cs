using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.HR;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class ProjectActivityDetail : BaseEntity
    {
        public ProjectActivityDetail()
        {
            Recurring = false;
            RecurringCount = 0;
            IsCompleted = false;
            Target = 0;
            Achieved = 0;

        }

        //Planning
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long ActivityId { get; set; }

        [MaxLength(200)]
        public string ActivityName { get; set; }

        public string ActivityDescription { get; set; }
        public string ChallengesAndSolutions { get; set; }
        public DateTime? PlannedStartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }

        public long? BudgetLineId { get; set; }
        [ForeignKey("BudgetLineId")]
        public ProjectBudgetLineDetail ProjectBudgetLineDetail { get; set; }

        public int? EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public EmployeeDetail EmployeeDetail { get; set; }

        public int? StatusId { get; set; }
        [ForeignKey("StatusId")]
        public ActivityStatusDetail ActivityStatusDetail { get; set; }

        public bool? Recurring { get; set; }
        public int? RecurringCount { get; set; }
        public int RecurrinTypeId { get; set; }

        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public bool IsCompleted { get; set; }


        public ICollection<ProjectActivityProvinceDetail> ProjectActivityProvinceDetail { get; set; }
        public List<ProjectActivityExtensions> ProjectActivityExtensionList { get; set; }

        public long? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public ICollection<ProjectActivityDetail> ProjectSubActivityList { get; set; }


        [Range(0, 100)]
        public float? Target { get; set; }
        [Range(0, 100)]
        public float? Achieved { get; set; }
        public string SubActivityTitle { get; set; }

    }
}
