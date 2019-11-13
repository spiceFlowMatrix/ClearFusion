using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class JournalDetailModel : BaseModel
    {
        public int JournalCode { get; set; }
        public string JournalName { get; set; }
        public byte? JournalType { get; set; }
    }

    public class JournalDetailModelDelete : BaseModel
    {
        public int JournalCode { get; set; }
    }
}
