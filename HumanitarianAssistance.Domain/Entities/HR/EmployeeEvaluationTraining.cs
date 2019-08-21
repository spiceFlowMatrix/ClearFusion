using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class EmployeeEvaluationTraining : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
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
