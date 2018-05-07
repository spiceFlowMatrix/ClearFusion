using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class InterviewTechnicalQuestion:BaseEntityWithoutId
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1, TypeName = "serial")]
		public int InterviewTechnicalQuestionId { get; set; }
		public InterviewDetails InterviewDetails { get; set; }
		public int InterviewDetailsId { get; set; }
		public int TechnicalQuestionId { get; set; }
		public TechnicalQuestion TechnicalQuestion { get; set; }
		//public int Poor { get; set; }
		//public int Fair { get; set; }
		//public int Good { get; set; }
		//public int Excellent { get; set; }
		//public int Perfect { get; set; }
		public string Answer { get; set; }
	}
}
