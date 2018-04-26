using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumanitarianAssistance.Entities.Models
{
    public partial class AccountNoteDetail
    {
        [Key]
        public long AccountCode { get; set; }
        public string Narration { get; set; }
        public long? AccountNote { get; set; }
        public string BalanceType { get; set; }
    }
}
