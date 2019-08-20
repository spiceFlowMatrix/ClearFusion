using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class TransactionBudgetModel
    {
        public string UserName { get; set; }
        public double? Credit { get; set; }
        public double? Debit { get; set; }
        public string CurrencyName { get; set; }
        public int? CurrencyId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public double? DebitPercentage { get; set; }



        public long? BudgetLineId { get; set; }
        public string BudgetCode { get; set; }
        public string BudgetName { get; set; }
        public double? InitialBudget { get; set; }
        public long? ProjectId { get; set; }
        public long? ProjectJobId { get; set; }
        public string ProjectJobName { get; set; }
        public string ProjectJobCode { get; set; }
        public string BudgetCodeName { get; set; }
    }
}
