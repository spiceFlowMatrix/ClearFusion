using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public  class TransactionDetailModel
    {
        public string UserName { get; set; }
        public int CurrencyId { get; set; }
        public long BudgetLineId { get; set; }
    }
}
