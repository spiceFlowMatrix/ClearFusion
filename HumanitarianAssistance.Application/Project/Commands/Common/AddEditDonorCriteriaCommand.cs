using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using MediatR;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
   public class AddEditDonorCriteriaCommand: BaseModel, IRequest<ApiResponse>
    {
        public AddEditDonorCriteriaCommand()
        {
            donorFilterModel = new List<DonorFilterModel>();
        }
        public long? ProjectId { get; set; }
        public long? DonorCEId { get; set; }
        public int? MethodOfFunding { get; set; }
        public bool? PastFundingExperience { get; set; }
        public bool? ProposalAccepted { get; set; }
        public bool? ProposalExperience { get; set; }
        public bool? Professional { get; set; }
        public bool? FundsOnTime { get; set; }
        public bool? EffectiveCommunication { get; set; }
        public bool? Dispute { get; set; }
        public bool? OtherDeliverable { get; set; }
        public string OtherDeliverableType { get; set; }
        public bool? PastWorkingExperience { get; set; }
        public bool? CriticismPerformance { get; set; }
        public bool? TimeManagement { get; set; }
        public bool? MoneyAllocation { get; set; }
        public bool? Accountability { get; set; }
        public bool? DeliverableQuality { get; set; }
        public int? DonorFinancingHistory { get; set; }
        public int? ReligiousStanding { get; set; }
        public int? PoliticalStanding { get; set; }
        public bool? DonorCriteriaMet { get; set; }
        public bool? EligibilityDealine { get; set; }
        public bool? CoPartnership { get; set; }

        public List<DonorFilterModel> donorFilterModel { get; set; }

    }
}
