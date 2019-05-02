using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataAccess.DbEntities.Project
{
    public class ProjectMonitoringIndicatorQuestions : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long Id { get; set; }
        public long QuestionId { get; set; }
        public string Verification { get; set; }
        public int? Score { get; set; }
        public long MonitoringIndicatorId { get; set; }
    }
}
