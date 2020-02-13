using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Models
{
    public class EmployeeEvaluationTrainingModel
    {
        public int EmployeeEvaluationTrainingId { get; set; }
        public int TrainingProgram { get; set; }
        public string Program { get; set; }
        public int Participated { get; set; }
        public int CatchLevel { get; set; }
        public int RefresherTrm { get; set; }
        public string OthRecommendation { get; set; }
        public int EmployeeAppraisalDetailsId { get; set; }
    }
}
