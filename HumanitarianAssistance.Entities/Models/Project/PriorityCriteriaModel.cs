using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
   public class PriorityCriteriaModel
    {
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
