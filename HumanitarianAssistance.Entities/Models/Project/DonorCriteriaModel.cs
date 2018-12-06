using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
   public class DonorCriteriaModel
    {
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
        public bool? OtherDeliverableType { get; set; }
        public bool? PastWorkingExperience { get; set; }
        public bool? CriticismPerformance { get; set; }
        public bool? TimeManagement { get; set; }
        public bool? MoneyAllocation { get; set; }
        public bool? Accountability { get; set; }
        public bool? DeliverableQuality { get; set; }
        public bool? DonorFinancingHistory { get; set; }
        public bool? ReligiousStanding { get; set; }
        public bool? PoliticalStanding { get; set; }


      
    }
}
