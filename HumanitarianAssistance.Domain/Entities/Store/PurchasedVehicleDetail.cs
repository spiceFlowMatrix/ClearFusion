using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HumanitarianAssistance.Domain.Entities.HR;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class PurchasedVehicleDetail: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long Id { get; set; }
        public string PlateNo { get; set; }
        public int EmployeeId { get; set; }
        public double StartingMileage { get; set; }
        public double IncurredMileage { get; set; }
        public double FuelConsumptionRate { get; set; }
        public double MobilOilConsumptionRate { get; set; }
        public int OfficeId { get; set; }
        public int ModelYear { get; set; }
        public long PurchaseId { get; set; }

        [ForeignKey("EmployeeId")]
        public EmployeeDetail EmployeeDetail { get; set; }
        [ForeignKey("PurchaseId")]
        public StoreItemPurchase StoreItemPurchase { get; set; }
        public VehicleItemDetail VehicleItemDetail { get; set; }
        public List<VehicleMileageDetail> VehicleMileageDetail {get; set;}
    }
}