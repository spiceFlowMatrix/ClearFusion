namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class DetailOfNotesModel
    {
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
    }
}