using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class InterviewFeedbackDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long FeedbackId { get; set; }
        public long ScheduleId { get; set; }
        public InterviewScheduleDetails InterviewScheduleDetails { get; set; }
        public int InterviewerId { get; set; }
        [ForeignKey("InterviewerId")]
        public EmployeeDetail EmployeeDetails { get; set; }
        public int RoundId { get; set; }
        public InterviewRoundTypeMaster InterviewRoundTypeMasters { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
