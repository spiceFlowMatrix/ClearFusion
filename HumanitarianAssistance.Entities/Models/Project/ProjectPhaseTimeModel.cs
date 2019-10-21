using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
  public  class ProjectPhaseTimeModel
    {
        public long ProjectPhaseTimeId { get; set; }
        public long ProjectId { get; set; }
        public DateTime? PhaseStartData { get; set; }
        public DateTime? PhaseEndDate { get; set; }
    }
}
