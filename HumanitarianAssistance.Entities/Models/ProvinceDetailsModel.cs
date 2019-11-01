using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class ProvinceDetailsModel : BaseModel
    {
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public int? CountryId { get; set; }
    }
}
