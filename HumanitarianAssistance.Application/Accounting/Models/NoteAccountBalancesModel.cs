using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class NoteAccountBalancesModel
    {
        public int NoteId { get; set; }
        public string NoteName { get; set; }
        public int NoteHeadId { get; set; }
        public string NoteHeadName { get; set; }
        public List<AccountBalanceModel> AccountBalances { get; set; }
    }
}