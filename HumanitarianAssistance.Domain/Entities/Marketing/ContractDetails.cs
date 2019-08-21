using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Marketing
{
    public class ContractDetails : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long ContractId { get; set; }
        public string ContractCode { get; set; }
        [ForeignKey("ClientId")]
        public long? ClientId { get; set; }
        public ClientDetails ClientDetails { get; set; }
        public string ClientName { get; set; }
        [ForeignKey("ActivityTypeId")]
        public long? ActivityTypeId { get; set; }
        public ActivityType ActivityTypes { get; set; }
        [ForeignKey("UnitRateId")]
        public long? UnitRateId { get; set; }
        public UnitRate UnitRates { get; set; }

        public double UnitRate { get; set; }
        [ForeignKey("CurrencyId")]
        public int? CurrencyId { get; set; }
        public CurrencyDetails CurrencyDetails { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey("LanguageId")]
        public long? LanguageId { get; set; }
        public LanguageDetail LanguageDetail { get; set; }
        [ForeignKey("MediumId")]
        public long? MediumId { get; set; }
        public Medium Mediums { get; set; }
        [ForeignKey("NatureId")]
        public long? NatureId { get; set; }
        public Nature Natures { get; set; }
        [ForeignKey("TimeCategoryId")]
        public long? TimeCategoryId { get; set; }
        public TimeCategory TimeCategories { get; set; }
        [ForeignKey("MediaCategoryId")]
        public long? MediaCategoryId { get; set; }
        public MediaCategory MediaCategories { get; set; }
        
        public long? QualityId { get; set; }
        [ForeignKey("QualityId")]
        public Quality Qualities { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDeclined { get; set; }
    }
}
