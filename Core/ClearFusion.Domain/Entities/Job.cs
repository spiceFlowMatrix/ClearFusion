namespace ClearFusion.Domain.Entities
{
    public class Job : IBaseEntity
    {
        public string Description { get; set; }
        public string Benefits { get; set; }
        public TimePeriod ContractDurationPeriod { get; set; }
        public int ContractDurationTime { get; set; }
        public JobType Type { get; set; }
        public decimal HourlyPayRate { get; set; }
        public CurrencyCodes PayCurrency { get; set; }
        public int TotalVacancies { get; set; }
        public int FilledVacancies { get; set; }
        public bool Active { get; set; }
    }
}