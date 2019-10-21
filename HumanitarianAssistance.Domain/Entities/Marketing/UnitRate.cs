using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Marketing
{
    public class UnitRate : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long UnitRateId { get; set; }
        public double UnitRates { get; set; }
        [ForeignKey("CurrencyId")]
        public long? CurrencyId { get; set; }
        public CurrencyDetails CurrencyDetails { get; set; }
        [ForeignKey("MediumId")]
        public long? MediumId { get; set; }
        public Medium Medium { get; set; }
        [ForeignKey("TimeCategoryId")]
        public long? TimeCategoryId { get; set; }
        public TimeCategory TimeCategories { get; set; }
        [ForeignKey("NatureId")]
        public long? NatureId { get; set; }
        public Nature Natures { get; set; }
        [ForeignKey("QualityId")]
        public long? QualityId { get; set; }
        public Quality Qualities { get; set; }
        [ForeignKey("ActivityTypeId")]
        public long? ActivityTypeId { get; set; }
        public ActivityType ActivityTypes { get; set; }
        [ForeignKey("MediaCategoryId")]
        public long? MediaCategoryId { get; set; }
        public MediaCategory MediaCategories { get; set; }
    }
}
