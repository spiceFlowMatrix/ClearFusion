using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class ProjectBudgetLineModel : BaseModel
    {
        public long BudgetLineId { get; set; }
        public string Description { get; set; }
        public long? ProjectId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public double AmountReceivable { get; set; }
        public double AmountPayable { get; set; }
        public string BudgetLineTypeName { get; set; }
        public int? BudgetLineTypeId { get; set; }
    }
}
