using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.SPModels
{
    public class SPDetailOfNotes
    {
        public int NoteId { get; set; }
        public string NoteName { get; set; }
        public long AccountId { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }

    }
}
