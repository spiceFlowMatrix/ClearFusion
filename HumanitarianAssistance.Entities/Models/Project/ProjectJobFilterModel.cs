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

        public int? pageIndex { get; set; }
        public int? pageSize { get; set; }
        public int? totalCount { get; set; }
    

}
}
