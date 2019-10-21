using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class AccountOpendingAndClosingBL
    {
        public double? ClosingBalance { get; set; }
        public double? OpeningBalance { get; set; }
        public string OpenningBalanceType { get; set; }
        public string ClosingBalanceType { get; set; }
    }
}
