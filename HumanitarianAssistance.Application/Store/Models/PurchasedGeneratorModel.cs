namespace HumanitarianAssistance.Application.Store.Models
{
    public class PurchasedGeneratorModel
    {
        public long? Id { get; set; }
        public int Voltage { get; set; }
        public int StartingUsage { get; set; }
        public int IncurredUsage { get; set; }
        public int FuelConsumptionRate { get; set; }
        public int MobilOilConsumptionRate { get; set; }
        public int OfficeId { get; set; }
        public int ModelYear { get; set; }
        public long? PurchaseId { get; set; }
    }
}