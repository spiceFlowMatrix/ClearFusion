using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class PurchaseGenerator : BaseEntity
    {
        [Key]
        public int GeneratorId { get; set; }
        [Required]
        public long PurchaseId { get; set; }

        public string GeneratorBrand { get; set; }
        public string GeneratorModel { get; set; }
        public string MakeYear { get; set; }
        public string SerialBarCode { get; set; }

        [ForeignKey("PurchaseId")]
        public StoreItemPurchase ItemPurchase { get; set; }
    }
}
