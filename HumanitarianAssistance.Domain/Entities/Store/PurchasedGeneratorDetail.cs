using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HumanitarianAssistance.Domain.Entities.HR;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class PurchasedGeneratorDetail: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long Id { get; set; }
        public double Voltage { get; set; }
        public double StartingUsage { get; set; }
        public double IncurredUsage { get; set; }
        public double FuelConsumptionRate { get; set; }
        public double MobilOilConsumptionRate { get; set; }
        public int OfficeId { get; set; }
        public int ModelYear { get; set; }
        public long PurchaseId { get; set; }
        public string ManufacturerCountry { get; set; }
        public int EmployeeID { get; set; } 
        public string PersonRemarks { get; set; }
        public string EngineNo { get; set; } 
        public string RegistrationNo { get; set; }
        public string ChasisNo { get; set; }

        [ForeignKey("PurchaseId")]
        public StoreItemPurchase StoreItemPurchase { get; set; }
        public List<GeneratorItemDetail> GeneratorItemDetail { get; set; }
        public List<GeneratorUsageHourDetail> GeneratorUsageHourList {get; set;}
        [ForeignKey("EmployeeID")]
        public EmployeeDetail EmployeeDetail { get; set; }  
    }
}