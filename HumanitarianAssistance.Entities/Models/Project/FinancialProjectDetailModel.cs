using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public class FinancialProjectDetailModel
    {
        public long? FinancialProjectDetailId { get; set; }      
        
        public long ProjectId { get; set; }
        public int? ProjectSelectionId { get; set; }
        public string ProjectName { get; set; }
    }
}
