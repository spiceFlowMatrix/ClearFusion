using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class OfficeDetailModel : BaseModel
    {
        public int OfficeId { get; set; }
        [Required]
        public string OfficeCode { get; set; }
        [Required]
        public string OfficeName { get; set; }
        public string SupervisorName { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNo { get; set; }
        [Required]
        public string OfficeKey { get; set; }
    }

    public class OfficeDetailsModelDelete : BaseModel
    {
        public int OfficeId { get; set; }
    }
}
