using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
  public class SecurityConsiderationMultiSelectModel
    {
        public long? SecurityConsiderationMultiSelectId { get; set; }
        public long ProjectId { get; set; }
        public List<long?> SecurityConsiderationId { get; set; }
        public long? SecurityConsiderationSelectedId { get; set; }
    }
}
