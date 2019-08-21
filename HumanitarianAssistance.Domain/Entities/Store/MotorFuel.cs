using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    // Fuel must be assigned to a vehicle OR generator when an order for it is generated.
    // A fuel entry should NEVER be assignable to both vehicle and generator.
    public class MotorFuel : BaseEntity
    {
        [Key]
        public string FuelId { get; set; }
        public string Order { get; set; }
        public int Vehicle { get; set; } // We should never be able to select a vehicle AND generator for a part. Only one should be selected. This should be taken care of by front-end
        public int Generator { get; set; }

        public long FuelQuantity { get; set; }

        [ForeignKey("Order")]
        public StorePurchaseOrder PurchaseOrder { get; set; }
        [ForeignKey("Vehicle")]
        public PurchaseVehicle PurchaseVehicle { get; set; }
        [ForeignKey("Generator")]
        public PurchaseGenerator PurchaseGenerator { get; set; }
    }
}
