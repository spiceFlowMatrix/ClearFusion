using HumanitarianAssistance.Common;
using HumanitarianAssistance.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DbEntities
{
   public class BaseEntity
    {
        public BaseEntity()
        {
            Id = GUIDGenerator.Generate();
        }
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedById { get; set; }
        //public AppUser CreatedBy { get; set; }
        public string ModifiedById { get; set; }
        //public AppUser ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

    }

}
