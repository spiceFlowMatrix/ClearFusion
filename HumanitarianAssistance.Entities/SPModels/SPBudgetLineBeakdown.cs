using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.SPModels
{
    public class SPBudgetLineBeakdown
    {
        public DateTime VoucherDate { get; set; }
        public double Expenditure { get; set; }
        public int Currencyid { get; set; }
    }
}
