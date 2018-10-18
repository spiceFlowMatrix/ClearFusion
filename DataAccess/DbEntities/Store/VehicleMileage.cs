using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities.Store
{
    public class VehicleMileage : BaseEntity
    {
        [Required]
        public int Vehicle { get; set; }
        [Required]
        public long Mileage { get; set; }
        public bool Verified { get; set; }

        [ForeignKey("Vehicle")]
        public PurchaseVehicle PurchaseVehicle { get; set; }
    }
}
