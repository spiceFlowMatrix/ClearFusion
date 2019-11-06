using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public class ProjectAssignToModel
    {
        public long ProjectAssignToId { get; set; }
        public long ProjectId { get; set; }
        public int EmployeeId { get; set; }
    }
}
