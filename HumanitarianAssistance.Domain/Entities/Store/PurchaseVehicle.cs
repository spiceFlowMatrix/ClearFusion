using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class PurchaseVehicle : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int VehicleId { get; set; }
        public string Purchase { get; set; }

        public string VehicleDescription { get; set; }

        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleMakeYear { get; set; }
        public string VehiclePlate { get; set; }
        public string VehicleSerialNo { get; set; }

        public string VehicleImageName { get; set; }
        public string VehicleImageFileName { get; set; }
        public string VehicleImageFileType { get; set; }

        [ForeignKey("Purchase")]
        public StoreItemPurchase ItemPurchase { get; set; }
    }
}