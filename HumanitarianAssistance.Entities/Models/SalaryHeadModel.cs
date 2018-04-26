using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class SalaryHeadModel : BaseModel
    {
        public int SalaryHeadId { get; set; }
        public int HeadTypeId { get; set; }
        public string HeadName { get; set; }
        public string Description { get; set; }
    }
}
