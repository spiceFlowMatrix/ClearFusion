using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities.Store
{
    public class VehicleLocation : BaseEntity
    {
        [Required]
        public int Vehicle { get; set; }
        [Required]
        public long Latitude { get; set; }
        [Required]
        public long Longitude { get; set; }
        [Required]
        public DateTime Time { get; set; }

        [ForeignKey("Vehicle")]
        public PurchaseVehicle PurchaseVehicle { get; set; }
    }
}
