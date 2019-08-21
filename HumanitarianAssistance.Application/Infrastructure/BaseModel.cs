using System;

namespace HumanitarianAssistance.Application.Infrastructure
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreatedById = null;
            ModifiedById = null;
        }

        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public bool IsDeleted { get; set; }

    }
}