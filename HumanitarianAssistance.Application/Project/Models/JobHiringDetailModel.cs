using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class JobHiringDetailModel
    {
        public long? JobId { get; set; }
        public string JobCode { get; set; }
        public string JobDescription { get; set; }
        public int? ProfessionId { get; set; }
        public int? TotalVacancies { get; set; }    
        public double? PayRate { get; set; }
        public int? OfficeId { get; set; }
        public int? GradeId { get; set; }    
        public int? CurrencyId { get; set; }
    }
}
