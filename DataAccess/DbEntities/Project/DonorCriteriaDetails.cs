using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Project
{
    public class DonorCriteriaDetails : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long DonorCEId { get; set; }
        [ForeignKey("ProjectId")]
        public long ProjectId { get; set; }
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
