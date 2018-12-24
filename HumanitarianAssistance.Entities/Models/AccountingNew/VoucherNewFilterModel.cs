using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.AccountingNew
{
   public class VoucherNewFilterModel
    {
        public string FilterValue { get; set; }
        public bool CodeFlag { get; set; }
        public bool NameFlag  { get; set; }
        public bool JournalFlag  { get; set; }
        public bool DateFlag  { get; set; }
    }

    
}
