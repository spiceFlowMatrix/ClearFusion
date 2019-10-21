using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class GeneratorItemDetail: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long Id { get; set; }
        public long PurchaseId { get; set; }
        public long GeneratorPurchaseId { get; set; }
        [ForeignKey("GeneratorPurchaseId")]
        public PurchasedGeneratorDetail PurchasedGeneratorDetail { get; set; }
        [ForeignKey("PurchaseId")]
        public StoreItemPurchase StoreItemPurchase { get; set; }
    }
}