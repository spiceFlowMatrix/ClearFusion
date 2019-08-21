using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
		public int InterviewDetailsId { get; set; }
		public string Question { get; set; }
		//public TechnicalQuestion TechnicalQuestion { get; set; }
		//public int Poor { get; set; }
		//public int Fair { get; set; }
		//public int Good { get; set; }
		//public int Excellent { get; set; }
		//public int Perfect { get; set; }
		public string Answer { get; set; }
	}
}
