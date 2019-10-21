using System;

namespace HumanitarianAssistance.Domain.Entities
{
    public class BaseEntity
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public bool IsDeleted { get; set; }
    }
}
