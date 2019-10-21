using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class GeneratorUsageHourDetail: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long Id {get; set;}
        public long GeneratorId {get; set;}
        public DateTime Month {get; set;}
        public double Hours {get; set;}
        [ForeignKey("GeneratorId")]
        public PurchasedGeneratorDetail PurchasedGeneratorDetail { get; set; }
    }
}