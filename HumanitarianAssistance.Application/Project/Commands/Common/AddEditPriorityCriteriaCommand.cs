using System;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using MediatR;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
  public  class AddEditPriorityCriteriaCommand : BaseModel, IRequest<ApiResponse>
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
