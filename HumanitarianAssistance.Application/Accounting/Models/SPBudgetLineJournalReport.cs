using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class SPBudgetLineJournalReport
    {
        public string ChartOfAccountNewCode { get; set; }
        public string Description { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double Expenditure { get; set; }
    }
    public class FinalBudgetLineJournalReport{
        public string Logo { get; set; }
        public List<SPBudgetLineJournalReport> reportList { get; set; }
    }
}