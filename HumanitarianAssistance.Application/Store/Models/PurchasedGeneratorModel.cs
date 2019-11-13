namespace HumanitarianAssistance.Application.Store.Models
{
    public class PurchasedGeneratorModel
    {
        public long? Id { get; set; }
        public double Voltage { get; set; }
        public double StartingUsage { get; set; }
        public double IncurredUsage { get; set; }
        public double FuelConsumptionRate { get; set; }
        public double MobilOilConsumptionRate { get; set; }
        public int OfficeId { get; set; }
        public int ModelYear { get; set; }
        public long? PurchaseId { get; set; }
    }
}