using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class VehicleItemDetail: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long Id { get; set; }
        public long PurchaseId { get; set; }
        public long VehiclePurchaseId { get; set; }
        [ForeignKey("VehiclePurchaseId")]
        public PurchasedVehicleDetail PurchasedVehicleDetail { get; set; }
        [ForeignKey("PurchaseId")]
        public StoreItemPurchase StoreItemPurchase { get; set; }
    }
}