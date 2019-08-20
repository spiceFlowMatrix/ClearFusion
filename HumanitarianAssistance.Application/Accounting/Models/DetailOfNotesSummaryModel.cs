using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class DetailOfNotesSummaryModel
    {
        public string NoteName { get; set; }
        public double TotalDebits { get; set; }
        public double TotalCredits { get; set; }
        public double Balance { get; set; }
        public List<DetailOfNotesModel> AccountSummary { get; set; }
    }
}