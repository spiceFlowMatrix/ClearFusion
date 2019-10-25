using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
   public class DonorEligibilityCriteriaModel
    {
        public long? DonorEligibilityDetailId { get; set; }
        public string Name { get; set; }
        public long ProjectId { get; set; }
    }
}
