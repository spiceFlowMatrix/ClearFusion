using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Marketing
{
    public class PolicyModel
    {
        public long? PolicyId { get; set; }
        public string PolicyName { get; set; }
        public long? LanguageId { get; set; }
        public long? MediumId { get; set; }
        public long? MediaCategoryId { get; set; }
        public string CreatedById { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Description { get; set; }
    }
}
