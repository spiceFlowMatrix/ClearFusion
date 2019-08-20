using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class EmployeeEvaluation: BaseEntity
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1)]
		public int EmployeeEvaluationId { get; set; }
		public DateTime CurrentAppraisalDate { get; set; }
		public int EmployeeId { get; set; }


		//public string TrainingProgram { get; set; }
		//public string Program { get; set; }
		//public string Participated { get; set; }
		//public string CatchLevel { get; set; }
		//public string RefresherTrm { get; set; }
		//public string OthRecommendation { get; set; }


		//public string StrongPoints { get; set; }
		//public string WeakPoints { get; set; }


		public string FinalResultQues1 { get; set; }
		public string FinalResultQues2 { get; set; }
		public string FinalResultQues3 { get; set; }
		public string FinalResultQues4 { get; set; }
		public string FinalResultQues5 { get; set; }
		public int DirectSupervisor { get; set; }
		public string AppraisalTeamMember1 { get; set; }
		public string AppraisalTeamMember2 { get; set; }
		public string CommentsByEmployee { get; set; }
		public string EvaluationStatus { get; set; }
        public int EmployeeAppraisalDetailsId { get; set; }

    }
}
