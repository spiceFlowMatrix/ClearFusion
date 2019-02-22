using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
  public  class TransactionBudgetModel
    {
        public string  UserName { get; set; }
        public double? Credit { get; set; }
        public double? Debit { get; set; }
        public string CurrencyName { get; set; }
        public int? CurrencyId { get; set; }
        public DateTime? TransactionDate { get; set; }
       
    }
}
