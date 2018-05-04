using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class InterviewTechQuesModel
    {
		public int TechnicalQuestionId { get; set; }
		public int Poor { get; set; }
		public int Fair { get; set; }
		public int Good { get; set; }
		public int Excellent { get; set; }
		public int Perfect { get; set; }
	}
}
