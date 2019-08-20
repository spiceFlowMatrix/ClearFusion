using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.HR;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class HiringRequestCandidates : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long CandidateId { get; set; }
        [ForeignKey("HiringRequestId")]
        public long HiringRequestId { get; set; }
        public ProjectHiringRequestDetail ProjectHiringRequestDetail { get; set; }
        [ForeignKey("EmployeeID")]
        public int? EmployeeID { get; set; }
        public EmployeeDetail EmployeeDetail { get; set; }
        public bool IsShortListed { get; set; }
        public bool IsSelected { get; set; }



    }
}
