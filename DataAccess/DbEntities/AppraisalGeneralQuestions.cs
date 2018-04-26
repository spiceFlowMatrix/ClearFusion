using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class AppraisalGeneralQuestions: BaseEntityWithoutId
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1, TypeName = "serial")]
		public int AppraisalGeneralQuestionsId { get; set; }
		public int SequenceNo { get; set; }
		public string Question { get; set; }
		public string DariQuestion { get; set; }
	}
}
