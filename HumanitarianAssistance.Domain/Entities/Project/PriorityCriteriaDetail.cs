using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class PriorityCriteriaDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long PriorityCriteriaDetailId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectDetail ProjectDetail { get; set; }
        public long ProjectId { get; set; }
        public bool? IncreaseEligibility { get; set; }
        public bool? IncreaseReputation { get; set; }
        public bool? ImproveDonorRelationship { get; set; }
        public bool? GoodCause { get; set; }
        public bool? ImprovePerformancecapacity { get; set; }
        public bool? SkillImprove { get; set; }
        public bool? FillingFundingGap { get; set; }
        public bool? NewSoftware { get; set; }
        public bool? NewEquipment { get; set; }
        public bool? CoverageAreaExpansion { get; set; }
        public bool? NewTraining { get; set; }
        public bool? ExpansionGoal { get; set; }
    }
}
