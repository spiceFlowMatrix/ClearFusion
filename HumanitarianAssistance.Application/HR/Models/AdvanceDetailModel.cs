namespace HumanitarianAssistance.Application.HR.Models
{
    public class AdvanceDetailModel
    {
        public int AdvanceId { get; set; }
        public double AdvanceAmount { get; set; }
        public double BalanceAmount { get; set; }
        public double InstallmentToBePaid { get; set; }
    }
}