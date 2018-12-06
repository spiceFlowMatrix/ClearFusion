using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
  public class EligibilityCriteriaDetailModel
    {
        public long EligibilityId { get; set; }
        public long ProjectId { get; set; }
        public bool? DonorCriteriaMet { get; set; }
        public bool? EligibilityDealine { get; set; }
        public bool? CoPartnership { get; set; }
    }
}
