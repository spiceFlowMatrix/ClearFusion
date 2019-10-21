using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class EmployeeEvaluationTraining : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
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
