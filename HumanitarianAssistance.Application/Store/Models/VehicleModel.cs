namespace HumanitarianAssistance.Application.Store.Models
{
    public class VehicleModel
    {
        public long VehicleId { get; set; }
        public string PlateNo { get; set; }
        public int EmployeeId { get; set; }
        public double StartingMileage { get; set; }
        public double IncurredMileage { get; set; }
        public double FuelConsumptionRate { get; set; }
        public double MobilOilConsumptionRate { get; set; }
        public int ModelYear { get; set; }
        public int OfficeId { get; set; }
        public string EmployeeName {get; set;}
        public string PurchaseName {get; set;}
        public long PurchaseId {get; set;}
        public string OfficeName {get; set;}
    }
}