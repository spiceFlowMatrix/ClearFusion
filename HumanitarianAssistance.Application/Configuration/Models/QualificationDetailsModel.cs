using HumanitarianAssistance.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Models
{
   public class QualificationDetailsModel : BaseModel
    {
        public int QualificationId { get; set; }
        public string QualificationName { get; set; }
    }
}
