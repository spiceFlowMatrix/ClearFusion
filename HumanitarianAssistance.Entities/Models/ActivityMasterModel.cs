using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class ActivityMasterModel : BaseModel
    {
        public int ActivityId { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string ActivityName { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
