using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class ProjectJobDetailModel
    {
        public long ProjectJobId { get; set; }
        public string ProjectJobCode { get; set; }
        public string ProjectJobName { get; set; }
        public long ProjectId { get; set; }
    }
}
