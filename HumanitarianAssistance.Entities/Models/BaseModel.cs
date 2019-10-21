using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
   public class BaseModel
    {
        public BaseModel()
        {
            
        }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedById { get; set; }
       
        public string ModifiedById { get; set; }
       
        public bool? IsDeleted { get; set; }


    }
}
