using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class PMUActivityDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int ActivityID { get; set; }
        [Key, ForeignKey("PMUProjectDetail")]
        public int? PMUProjectID { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime? PlannedStartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public int? Slippage { get; set; }
        public int? BudgetLine { get; set; }
        public string Employees { get; set; }
        public Boolean? IsCompleted { get; set; }
        public Boolean? IsChecked { get; set; }
        public Boolean? IsMonitoring { get; set; }
        [StringLength(10)]
        public string ActivityLocation { get; set; }
        [StringLength(100)]
        public string DeviationJustification { get; set; }
        [StringLength(100)]
        public string ImplementationMethod { get; set; }
        [StringLength(100)]
        public string Challenges { get; set; }
        [StringLength(100)]
        public string OvercomingChallenges { get; set; }
        public byte? SchedulerTaskType { get; set; }
        public byte? RecurringType { get; set; }
        public byte? RecurringDay { get; set; }
        public byte? RecurringMonth { get; set; }
        [StringLength(10)]
        public string RecurringWeekDays { get; set; }
    }
}
