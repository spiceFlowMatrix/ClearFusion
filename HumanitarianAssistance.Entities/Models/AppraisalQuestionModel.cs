using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class AppraisalQuestionModel
    {
        public int? EmployeeAppraisalQuestionsId { get; set; }
        public int AppraisalGeneralQuestionsId { get; set; }
		public int SequenceNo { get; set; }
		public string Question { get; set; }
		public string DariQuestion { get; set; }
	    public int? OfficeId { get; set; }
	}
}
