namespace HumanitarianAssistance.Application.Store.Models
{
    public class GeneratorModel
    {
        public long GeneratorId { get; set; }
        public double Voltage { get; set; }
        public double StartingUsage { get; set; }
        public double IncurredUsage { get; set; }
        public double CurrentUsage { get; set; }
        public double StandardFuelConsumptionRate { get; set; }
        public double ActualFuelConsumptionRate { get; set; }
        public double StandardMobilOilConsumptionRate { get; set; }
        public double ActualMobilOilConsumptionRate { get; set; }
        public double TotalFuelUsage { get; set; }
        public double TotalMobilOilUsage { get; set; }
        public double FuelTotalCost { get; set; }
        public double MobilOilTotalCost { get; set; }
        public double SparePartsTotalCost { get; set; }
        public double ServicesAndMaintenanceTotalCost { get; set; }
        public double GeneratorStartingCost { get; set; }
        public int OfficeId { get; set; }
        public int ModelYear { get; set; }
        public long PurchaseId { get; set; }
        public string PurchasedBy { get; set; }
        public string PurchaseName { get; set; }
        public string OfficeName { get; set; }
        public string ChasisNo { get; set; }
        public string RegistrationNo { get; set; }
        public int? EmployeeId { get; set; }
        public string EngineNo { get; set; }
        public string Remarks { get; set; }
        public string ManufacturerCountry { get; set; }
    }
}