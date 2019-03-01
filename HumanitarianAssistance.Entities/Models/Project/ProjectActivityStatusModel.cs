using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public class ProjectActivityStatusModel
    {
        public int? ActivityOnSchedule { get; set; }
        public int? LateStart { get; set; }
        public int? LateEnd { get; set; }
        public int? Slippage { get; set; }
        public int? Progress { get; set; }
        public int? ProjectDuration { get; set; }

    }
}
