using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
  public  class CEPriorityDetailModel
    {
        public long? PriorityOtherDetailId { get; set; }
        public string Name { get; set; }
        public long ProjectId { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
