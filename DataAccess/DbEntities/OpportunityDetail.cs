using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class OpportunityDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long OpportunityID { get; set; }
        [StringLength(20)]
        public string OpportunityNumberType { get; set; }
        public int? OpportunityNumber { get; set; }
        [StringLength(100)]
        public string OpportunityDescription { get; set; }
        public string Provinces { get; set; }
        public string Districts { get; set; }
        [StringLength(200)]
        public string OfficeCodes { get; set; }
        public string Sector { get; set; }
        [StringLength(10)]
        public string CurrencyCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public float? Budget { get; set; }
        public float? DirectBeneficiaryMale { get; set; }
        public float? DirectBeneficiaryFemale { get; set; }
        public float? IndirectBeneficiaryMale { get; set; }
        public float? IndirectBeneficiaryFemale { get; set; }
        public string ProjectGoal { get; set; }
        public string ProjectObjective { get; set; }
        public string MainActivities { get; set; }
        [StringLength(100)]
        public string DonorName { get; set; }
        [StringLength(100)]
        public string DonorContactPerson { get; set; }
        [StringLength(100)]
        public string DonorContactDesignation { get; set; }
        [StringLength(100)]
        public string DonorContactEmail { get; set; }
        [StringLength(40)]
        public string DonorContactPhone { get; set; }
        [StringLength(100)]
        public string EOIAttachment { get; set; }
        [StringLength(100)]
        public string BudgetAttachment { get; set; }
        [StringLength(100)]
        public string ConceptAttachment { get; set; }
        [StringLength(100)]
        public string ContractAttachment { get; set; }
        [StringLength(100)]
        public string ProposalAttachment { get; set; }
        [StringLength(100)]
        public string PresentationAttachment { get; set; }
        public DateTime? REOIReceiveDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        [StringLength(100)]
        public string StrengthConsideration { get; set; }
        [StringLength(100)]
        public string GenderConsideration { get; set; }
        public string GenderRemarks { get; set; }
        [StringLength(100)]
        public string Security { get; set; }
        public string SecurityConsideration { get; set; }
        public string SecurityRemarks { get; set; }
        public byte? Status { get; set; }
        public string Comment { get; set; }
        public string PDTComment { get; set; }
        [StringLength(10)]
        public string ProjectCode { get; set; }
    }
}
