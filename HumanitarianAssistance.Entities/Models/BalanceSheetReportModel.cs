using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
   public class BalanceSheetReportModel
    {
        public int AccountCode { get; set; }
        public double Amount { get; set; }
        public List<BalanceSheetReportModel> Accounts { get; set; }

    }

    public class BalanceSheetAccountModel
    {
        public int AccountCode { get; set; }
        public double Amount { get; set; }
    }
}
