using HumanitarianAssistance.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Models
{
  public class BudgetLineTypeModel : BaseModel
    {
        public string BudgetLineTypeName { get; set; }
        public int BudgetLineTypeId { get; set; }
    }
}
