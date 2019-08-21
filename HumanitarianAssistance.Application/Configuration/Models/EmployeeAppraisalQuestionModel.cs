using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Models
{
    public class EmployeeAppraisalQuestionModel
    {
        public int? EmployeeAppraisalQuestionsId { get; set; }
        public string QuestionEnglish { get; set; }
        public string QuestionDari { get; set; }
        public int AppraisalGeneralQuestionsId { get; set; }
        public int SequenceNo { get; set; }
        public int? Score { get; set; }
        public string Remarks { get; set; }
    }
}
