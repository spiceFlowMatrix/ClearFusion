using HumanitarianAssistance.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Models
{
   public  class EmployeeHistoryDetailModel: BaseModel
    {
        public long HistoryID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? HistoryDate { get; set; }
        public string Description { get; set; }
    }
}
