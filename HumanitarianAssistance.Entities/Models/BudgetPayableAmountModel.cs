using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class BudgetPayableAmountModel :BaseModel
    {
        public long BudgetPayableAmountId { get; set; }
        public long BudgetPayableId { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }
        public string Remark { get; set; }

        public string ProjectName { get; set; }
        public string BudgetLineName { get; set; }
        
    }
}
