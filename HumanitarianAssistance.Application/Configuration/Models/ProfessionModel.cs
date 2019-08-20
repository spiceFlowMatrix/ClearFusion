using HumanitarianAssistance.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Models
{
   public class ProfessionModel : BaseModel
    {
        public int ProfessionId { get; set; }
        public string ProfessionName { get; set; }
    }
}
