using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class VehicleMileageDetail: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long Id {get; set;}
        public long VehicleId {get; set;}
        public int Month {get; set;}
        public int Mileage {get; set;}
        [ForeignKey("VehicleId")]
        public PurchasedVehicleDetail PurchasedVehicleDetail { get; set; }
    }
}