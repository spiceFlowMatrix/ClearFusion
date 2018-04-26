using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
  public  class JournalVoucherViewModel
    {
        public string JournalCode { get; set; }
        public string VoucherNo { get; set; }
        public double? Amount { get; set; }

        public string TransactionNo { get; set; }

        public string TransactionDate { get; set; }

        public string TransactionType { get; set; }

        public string AccountCode { get; set; }


    }
}
