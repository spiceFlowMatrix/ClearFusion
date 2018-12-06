using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
   public class FinancialCriteriaModel
    {
        public long FinancialCriteriaDetailId { get; set; }       
        public long ProjectId { get; set; }
        public double? Total { get; set; }
        
        public double? ProjectActivities { get; set; }
        public double? Operational { get; set; }
        public double? Overhead_Admin { get; set; }
        public double? Lump_Sum { get; set; }
    }
}
