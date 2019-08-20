using HumanitarianAssistance.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Models
{
    public class CountryDetailsModel : BaseModel
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
