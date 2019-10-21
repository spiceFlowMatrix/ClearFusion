using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
   public  class DonorModel
    {
        public long? DonorId { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string ContactDesignation { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonCell { get; set; }
    }
}
