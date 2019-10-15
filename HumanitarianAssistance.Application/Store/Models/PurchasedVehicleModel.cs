namespace HumanitarianAssistance.Application.Store.Models
{
    public class PurchasedVehicleModel
    {
        public long? Id { get; set; }
        public string PlateNo { get; set; }
        public int EmployeeId { get; set; }
        public int StartingMileage { get; set; }
        public int IncurredMileage { get; set; }
        public int FuelConsumptionRate { get; set; }
        public int MobilOilConsumptionRate { get; set; }
        public int OfficeId { get; set; }
        public int ModelYear { get; set; }
        public long? PurchaseId { get; set; }
    }
}