using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class ProjectHiringRequestModel
    {
        public string Description { get; set; }
        public string Office { get; set; }
        public string JobCode { get; set; }
        public string JobGrade { get; set; }
        public string Position { get; set; }
        public int? TotalVacancies { get; set; }
        public int? FilledVacancies { get; set; }
        public string PayCurrency { get; set; }
        public double? PayRate { get; set; }
        public string Status { get; set; }
        public long? HiringRequestId { get; set; }

    }
}
