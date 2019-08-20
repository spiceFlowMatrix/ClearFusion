using HumanitarianAssistance.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Models
{
  public  class FinancialYearDetailModel: BaseModel
    {
        public int FinancialYearId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FinancialYearName { get; set; }
        public string Description { get; set; }
        public Boolean IsDefault { get; set; }
    }
}
