﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities.Store
{
    public class PurchaseVehicle : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int VehicleId { get; set; }
        public long PurchaseId { get; set; }

        public string VehicleDescription { get; set; }

        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleMakeYear { get; set; }
        public string VehiclePlate { get; set; }
        public string VehicleSerialNo { get; set; }

        public string VehicleImageName { get; set; }
        public string VehicleImageFileName { get; set; }
        public string VehicleImageFileType { get; set; }
        
        [ForeignKey("PurchaseId")]
        public StoreItemPurchase ItemPurchase { get; set; }
    }
}