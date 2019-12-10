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
        public string ManufacturerCountry { get; set; }
        public string PersonRemarks { get; set; }
        public string EngineNo { get; set; } 
        public string RegistrationNo { get; set; }
        public string ChasisNo { get; set; }

        [ForeignKey("EmployeeId")]
        public EmployeeDetail EmployeeDetail { get; set; }
        [ForeignKey("PurchaseId")]
        public StoreItemPurchase StoreItemPurchase { get; set; }
        public List<VehicleItemDetail> VehicleItemDetail { get; set; }
        public List<VehicleMileageDetail> VehicleMileageDetail {get; set;}
    }
}