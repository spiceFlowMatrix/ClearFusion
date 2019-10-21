using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
   public class DonorFilterModel
    {
        public string FilterValue { get; set; }
        public bool? DonorNameFlag { get; set; }
        public bool? DateFlag { get; set; }

        public int? pageIndex { get; set; }
        public int? pageSize { get; set; }
        public int? totalCount { get; set; }
    }
}
