using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Models
{
    public class EmployeeEvaluationTrainingModel
    {
        public int EmployeeEvaluationTrainingId { get; set; }
        public string TrainingProgram { get; set; }
        public string Program { get; set; }
        public string Participated { get; set; }
        public string CatchLevel { get; set; }
        public string RefresherTrm { get; set; }
        public string OthRecommendation { get; set; }
        public int EmployeeAppraisalDetailsId { get; set; }
    }
}
