namespace HumanitarianAssistance.Application.Store.Models
{
    public class PurchasedVehicleModel
    {
        public long? Id { get; set; }
        public string PlateNo { get; set; }
        public int EmployeeId { get; set; }
        public double StartingMileage { get; set; }
        public double IncurredMileage { get; set; }
        public double FuelConsumptionRate { get; set; }
        public double MobilOilConsumptionRate { get; set; }
        public int OfficeId { get; set; }
        public int ModelYear { get; set; }
        public long? PurchaseId { get; set; }
        public string ChasisNo { get; set; }
        public string RegistrationNo { get; set; }
        public string Remarks { get; set; }
        public string EngineNo { get; set; }
        public string ManufacturerCountry { get; set; }
    }
}