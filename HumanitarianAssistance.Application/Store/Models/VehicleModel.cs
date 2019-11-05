namespace HumanitarianAssistance.Application.Store.Models
{
    public class VehicleModel
    {
        public long VehicleId { get; set; }
        public string PlateNo { get; set; }
        public int EmployeeId { get; set; }
        public double StartingMileage { get; set; }
        public double IncurredMileage { get; set; }
        public double CurrentMileage { get; set; }
        public double TotalFuelUsage {get; set;}
        public double TotalMobilOilUsage {get; set;}
        public double StandardFuelConsumptionRate { get; set; }
        public double ActualFuelConsumptionRate { get; set; }
        public double StandardMobilOilConsumptionRate { get; set; }
        public double ActualMobilOilConsumptionRate { get; set; }
        public double FuelTotalCost {get; set;}
        public double MobilOilTotalCost {get; set;}
        public double SparePartsTotalCost {get; set;}
        public double ServiceAndMaintenanceTotalCost {get; set;}
        public double GeneratorStartingCost {get; set;}
        public int ModelYear { get; set; }
        public int OfficeId { get; set; }
        public string EmployeeName {get; set;}
        public string PurchaseName {get; set;}
        public long PurchaseId {get; set;}
        public string OfficeName {get; set;}
        public double VehicleStartingCost { get; set; }
    }
}