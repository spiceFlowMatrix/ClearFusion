namespace HumanitarianAssistance.Application.Store.Models
{
    public class GeneratorModel
    {
        public long GeneratorId { get; set; }
        public double Voltage { get; set; }
        public double StartingUsage { get; set; }
        public double IncurredUsage { get; set; }
        public double FuelConsumptionRate { get; set; }
        public double MobilOilConsumptionRate { get; set; }
        public int OfficeId { get; set; }
        public int ModelYear { get; set; }
        public long PurchaseId { get; set; }
        public string PurchasedBy { get; set; }
        public string PurchaseName { get; set; }
        public string OfficeName { get; set; }
    }
}