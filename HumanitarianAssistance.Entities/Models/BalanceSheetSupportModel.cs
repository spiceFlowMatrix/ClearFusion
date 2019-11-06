using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class BalanceSheetSupportModel
    {
        public int? ChartofAccountCode { get; set; }

        public string Narration { get; set; }
        public int BlanceType { get; set; } // 1 = Sum , 2 = CR , 3 = DR
        public int? FinancialReportTypeId { get; set; } // 1 = Balance Sheet , 2 = Income and Expance
        public int? AccountTypeId { get; set; }
        public int? AccountLevelId { get; set; }


        public List <VoucherTransactionModel> CreditAccountlist { get; set; }
        public List<VoucherTransactionModel> DebitAccountlist { get; set; }

    }
}
