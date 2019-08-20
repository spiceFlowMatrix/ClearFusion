using System;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using MediatR;
using System.Collections.Generic;
namespace HumanitarianAssistance.Application.Project.Commands.Common
{
   public class AddEditFeasibilityCriteriaCommand : BaseModel, IRequest<ApiResponse>
    {
        public long? FeasibilityId { get; set; }
        public long? ProjectId { get; set; }
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
