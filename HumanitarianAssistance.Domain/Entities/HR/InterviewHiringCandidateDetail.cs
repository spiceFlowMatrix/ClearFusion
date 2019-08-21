using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public  class InterviewHiringCandidateDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int InterviewHiringCandidateID { get; set; }
        [StringLength(50)]
	    public string EmployeeNo { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
        [StringLength(50)]
        public string Position { get; set; }
        public int? Grade { get; set; }
        [StringLength(10)]
        public string Office { get; set; }
        public string JobAnnouncement { get; set; }
        [StringLength(100)]
        public string JobAnnouncementAttachment { get; set; }
        [StringLength(50)]
        public string EmailAddress { get; set; }
        [StringLength(20)]
        public string TimeOfInterview { get; set; }
        public DateTime? DateOfInterview { get; set; }
        public byte? Status { get; set; }
    }
}
