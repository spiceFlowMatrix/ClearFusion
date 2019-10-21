using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class spTransactionBudgetModel
    {
        public int? CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public double? Credit { get; set; }
        public double? Debit { get; set; }               
        public DateTime? TransactionDate { get; set; }
        public DateTime? CreatedDate { get; set; }  
    }
}
