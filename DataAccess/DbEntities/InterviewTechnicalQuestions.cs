using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class InterviewTechnicalQuestions: BaseEntityWithoutId
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1, TypeName = "serial")]
		public int InterviewTechnicalQuestionsId { get; set; }		
		public string Question { get; set; }
		public int OfficeId { get; set; }
	}
}
