using HumanitarianAssistance.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Models
{
   public class CurrentFinancialYearModel: BaseModel
    {
        public int FinancialYearId { get; set; }
        public string FinancialYearName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
