using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
   public class ProjectJobFilterModel
    {
            
        public string FilterValue { get; set; }
        public bool? ProjectJobNameFlag { get; set; }
        public bool? DateFlag { get; set; }

        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int? TotalCount { get; set; }
        public long? ProjectId { get; set; }
    }
}
