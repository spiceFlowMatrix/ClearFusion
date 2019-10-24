using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
   public class ProgramModel
    {
        public long? ProgramId { get; set; }
        public string ProgramName { get; set; }
        public string ProgramCode { get; set; }
        public long? ProjectId { get; set; }
    }
}
