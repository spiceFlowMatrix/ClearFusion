using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HumanitarianAssistance.Domain.Entities.HR;

namespace HumanitarianAssistance.Domain.Entities.Project {
    public class HiringRequestCandidateStatus : BaseEntity {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        [Column (Order = 1)]
        public long CandidateStatusId { get; set; }

        [ForeignKey ("CandidateId")]
        public long? CandidateId { get; set; }
        public CandidateDetails CandidateDetails { get; set; }

        [ForeignKey ("EmployeeID")]
        public int? EmployeeID { get; set; }
        public EmployeeDetail EmployeeDetails { get; set; }
        public int CandidateStatus { get; set; }

        [ForeignKey ("InterviewId")]
        public int? InterviewId { get; set; }
        public ProjectInterviewDetails ProjectInterviewDetails { get; set; }

        [ForeignKey ("ProjectId")]
        public long? ProjectId { get; set; }
        public ProjectDetail ProjectDetail { get; set; }

        [ForeignKey ("HiringRequestId")]
        public long HiringRequestId { get; set; }
        public ProjectHiringRequestDetail ProjectHiringRequestDetail { get; set; }
    }
}