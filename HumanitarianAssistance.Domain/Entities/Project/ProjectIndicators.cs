using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class ProjectIndicators : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long ProjectIndicatorId { get; set; }
        public string IndicatorName { get; set; }
        public string IndicatorCode { get; set; }
        public string Description { get; set; }
        public List<ProjectIndicatorQuestions> ProjectIndicatorQuestions { get; set; }
        [ForeignKey("ProjectId")]
        public long ProjectId { get; set; }
        public ProjectDetail ProjectDetail { get; set; }
    }
}
