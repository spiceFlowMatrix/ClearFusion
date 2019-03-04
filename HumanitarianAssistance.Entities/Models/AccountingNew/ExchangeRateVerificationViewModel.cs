using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.AccountingNew
{
    public class ExchangeRateVerificationViewModel
    {
        public long ExRateVerificationId { get; set; }
        public DateTime Date { get; set; }
        public bool IsVerified { get; set; } = false;
    }

    public class ExchangeRateVerificationFilter
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? IsVerified { get; set; } = false;
    }

    public class ExchangeRateDetailViewModel
    {
        public long ExchangeRateId { get; set; }
        public int FromCurrency { get; set; }
        public int ToCurrency { get; set; }
        public double Rate { get; set; }
    }
}
