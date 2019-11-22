using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Project;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class InterviewTechnicalQuestion:BaseEntity
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1)]
		public int InterviewTechnicalQuestionId { get; set; }
        [ForeignKey("InterviewDetailsId")]
		public InterviewDetails InterviewDetails { get; set; }
		public int? InterviewDetailsId { get; set; }
		public string Question { get; set; }
		public string Answer { get; set; }
		[ForeignKey ("InterviewId")]
		public ProjectInterviewDetails ProjectInterviewDetails { get; set; }
		public int? InterviewId { get; set; }
 		
		[ForeignKey("QuestionId")]
		public TechnicalQuestion TechnicalQuestion { get; set; }
		public int? QuestionId { get; set; }
        public int? Score { get; set; }
	}
}
