using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class InterviewTechnicalQuestions: BaseEntity
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1)]
		public int InterviewTechnicalQuestionsId { get; set; }		
		public string Question { get; set; }
		public int OfficeId { get; set; }
	}
}
