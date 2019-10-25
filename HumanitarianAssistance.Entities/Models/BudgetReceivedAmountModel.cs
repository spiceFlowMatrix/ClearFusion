using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
   public class BudgetReceivedAmountModel :BaseModel
    {
        
        public long BudgetReceivedAmountId { get; set; }
        public long? BudgetReceivalbeId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public double Amount { get; set; }

        public string ProjectName { get; set; }
        public string BudgetLineName { get; set; }

        public string Remark { get; set; }

    }
}
