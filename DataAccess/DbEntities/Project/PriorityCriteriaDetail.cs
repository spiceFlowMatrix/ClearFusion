using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Project
{
  public  class PriorityCriteriaDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long PriorityCriteriaDetailId { get; set; }
        [ForeignKey("ProjectId")]
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
