using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Models
{
    public class InterviewTechnicalQuestionsModel
    { 
        public int InterviewTechnicalQuestionsId { get; set; }
        public string Question { get; set; }
        public int OfficeId { get; set; }
    }
}
