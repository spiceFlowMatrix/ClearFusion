using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public class ProvinceMultiSelectModel
    {
        public long ProvinceMultiSelectId { get; set; }

        public long ProjectId { get; set; }

        public List<int> ProvinceId { get; set; }

        public long? ProvinceSelectionId { get; set; }
    }
}
