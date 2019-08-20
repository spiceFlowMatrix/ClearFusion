using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class FeasibilityCriteriaDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long FeasibilityId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectDetail ProjectDetail { get; set; }
        public long ProjectId { get; set; }
        public bool? CapacityAvailableForProject { get; set; }
        public bool? TrainedStaff { get; set; }
        public bool? ByEquipment { get; set; }
        public bool? ExpandScope { get; set; }
        public bool? GeoGraphicalPresence { get; set; }
        public bool? ThirdPartyContract { get; set; }
        public int? CostOfCompensationMonth { get; set; }
        public double? CostOfCompensationMoney { get; set; }
        public bool? AnyInKindComponent { get; set; }
        public bool? UseableByOrganisation { get; set; }
        public bool? FeasibleExpertDeploy { get; set; }
        public bool? FeasibilityExpert { get; set; }
        public bool? EnoughTimeForProject { get; set; }
        public bool? ProjectAllowedBylaw { get; set; }
        public bool? ProjectByLeadership { get; set; }
        public bool? IsProjectPractical { get; set; }
        public bool? PresenceCoverageInProject { get; set; }
        public bool? ProjectInLineWithOrgFocus { get; set; }
        public bool? EnoughTimeToPrepareProposal { get; set; }
        public double? ProjectRealCost { get; set; }
        public bool? IsCostGreaterthenBudget { get; set; }
        public int? PerCostGreaterthenBudget { get; set; }
        public bool? IsFinancialContribution { get; set; }
        public bool? IsSecurity { get; set; }
        public bool? IsGeographical { get; set; }
        public bool? IsSeasonal { get; set; }

    }
}
