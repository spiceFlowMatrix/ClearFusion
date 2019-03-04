using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public class ProjectActivityStatusModel
    {
        public ProjectActivityStatusModel()
        {
            ProjectDuration = 0;
            ActivityOnSchedule = 0;
            LateStart = 0;
            LateEnd = 0;
            Progress = 0;
            Slippage = 0;
        }
        public int? ActivityOnSchedule { get; set; }
        public int? LateStart { get; set; }
        public int? LateEnd { get; set; }
        public int? Slippage { get; set; }
        public float? Progress { get; set; }
        public int? ProjectDuration { get; set; }

    }
}
