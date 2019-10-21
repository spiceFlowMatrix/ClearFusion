using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Models
{
    public class AdvancesHistoryModel
    {
        public double InstallmentPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public double BalanceAmount { get; set; }
        public long? AdvanceId { get; set; }

    }
}
