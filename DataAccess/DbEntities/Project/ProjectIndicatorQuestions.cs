using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Project
{
    public class ProjectIndicatorQuestions : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long IndicatorQuestionId { get; set; }
        public string IndicatorQuestion { get; set; }
        public long ProjectIndicatorId { get; set; }
        [ForeignKey("ProjectIndicatorId")]
        public ProjectIndicators ProjectIndicators { get; set; }
    }
}
