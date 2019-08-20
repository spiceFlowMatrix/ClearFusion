using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class ProjectIndicatorQuestions : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long IndicatorQuestionId { get; set; }
        public string IndicatorQuestion { get; set; }
        public long ProjectIndicatorId { get; set; }
        [ForeignKey("ProjectIndicatorId")]
        public ProjectIndicators ProjectIndicators { get; set; }
    }
}
