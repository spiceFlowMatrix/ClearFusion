using HumanitarianAssistance.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Models
{
   public class EmployeeRelativeInfoModel : BaseModel
    {
        public int EmployeeRelativeInfoId { get; set; }
        public int EmployeeInfoReferencesId { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string Position { get; set; }
        public string Organization { get; set; }
        public int EmployeeID { get; set; }
        public long PhoneNo { get; set; }
        public string Email { get; set; }
    }
}
