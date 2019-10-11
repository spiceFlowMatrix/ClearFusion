using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class PurchasedGeneratorDetail: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long Id { get; set; }
        public int Voltage { get; set; }
        public int StartingUsage { get; set; }
        public int IncurredUsage { get; set; }
        public int FuelConsumptionRate { get; set; }
        public int MobilOilConsumptionRate { get; set; }
        public int OfficeId { get; set; }
        public int ModelYear { get; set; }
        public long PurchaseId { get; set; }

        [ForeignKey("PurchaseId")]
        public StoreItemPurchase StoreItemPurchase { get; set; }
        public GeneratorItemDetail GeneratorItemDetail { get; set; }
    }
}