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
        public int InterviewId { get; set; }         
    }
}