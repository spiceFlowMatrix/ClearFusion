

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities.Store
{
    public class MotorMaintenance : BaseEntityWithoutId
    {
        [Key]
        public string MaintenanceId { get; set; }
        public string Order { get; set; }
        public int Generator { get; set; }
        public int Vehicle { get; set; }

        public string Description { get; set; }
        [Required]
        public string StoreName { get; set; } // This is how we determine if the store is Consumable, Expendable, Non-Expendable etc

        [ForeignKey("Order")]
        public StorePurchaseOrder StorePurchaseOrder { get; set; }
        [ForeignKey("Vehicle")]
        public PurchaseVehicle PurchaseVehicle { get; set; }
        [ForeignKey("Generator")]
        public PurchaseGenerator PurchaseGenerator { get; set; }
    }
}
