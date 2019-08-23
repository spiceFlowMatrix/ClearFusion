using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities.Store
{
    public class MotorSparePart : BaseEntityWithoutId
    {
        [Key]
        public string PartId { get; set; }
        public long Order { get; set; }
        public int Generator { get; set; } // We should never be able to select a vehicle AND generator for a part. Only one should be selected. This should be taken care of by front-end
        public int Vehicle { get; set; }

        public string PartName { get; set; }
        public string PartDescription { get; set; }
        public bool PartUsed { get; set; }

        [ForeignKey("Order")]
        public StorePurchaseOrder StorePurchaseOrder { get; set; }
        [ForeignKey("Vehicle")]
        public PurchaseVehicle PurchaseVehicle { get; set; }
        [ForeignKey("Generator")]
        public PurchaseGenerator PurchaseGenerator { get; set; }
    }
}
