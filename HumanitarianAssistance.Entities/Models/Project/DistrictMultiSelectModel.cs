using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public class DistrictMultiSelectModel
    {
        public long DistrictMultiSelectId { get; set; }
        public long ProjectId { get; set; }
        public List<long?> DistrictID { get; set; }
        public long? DistrictSelectionId { get; set; }
        public List<int> ProvinceId { get; set; }

    }
}
