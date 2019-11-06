using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public class CountryMultiSelectModel
    {
        public long CountryMultiSelectId { get; set; }

        public long ProjectId { get; set; }

        public List<int?> CountryId { get; set; }
         
        public long? CountrySelectionId { get; set; }
    }
}
