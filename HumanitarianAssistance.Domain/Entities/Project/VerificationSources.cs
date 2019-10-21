using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
   public class VerificationSources: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long VerificationSourceId { get; set; }
        public string VerificationSourceName { get; set; }
        [ForeignKey("IndicatorQuestionId")]
        public long IndicatorQuestionId { get; set; }
        public ProjectIndicatorQuestions ProjectIndicatorQuestions { get; set; }
    }
}
