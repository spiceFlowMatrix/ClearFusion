using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.SPModels
{
    public class SPVoucherSummaryReportModel
    {
        public string VoucherCode { get; set; }
        public string VoucherDescription { get; set; }
        public string TransactionDescription { get; set; }
        public string AccountName { get; set; }
        public string Currency { get; set; }
        public double Amount { get; set; }
        public string TransactionType { get; set; }

    }
}
