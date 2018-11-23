using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
   public class ProjectDetailNewModel
    {
        public long ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime? StartData { get; set; }
        public DateTime? EndDate { get; set; }
        public long? ProjectPhaseDetailsId { get; set; }
        public bool? IsProposalComplate { get; set; }
        public string ProjectPhase { get; set; }
        public string TotalDaysinHours  { get; set; }
        public bool? IsWin { get; set; }
        public bool? IsApproved { get; set; }
    }
}
