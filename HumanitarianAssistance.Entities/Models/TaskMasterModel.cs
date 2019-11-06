using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class TaskMasterModel : BaseModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public long? ProjectId { get; set; }
        public string Status { get; set; }
        public List<ActivityMasterModel> ActivityMasterList { get; set; }
    }
}
