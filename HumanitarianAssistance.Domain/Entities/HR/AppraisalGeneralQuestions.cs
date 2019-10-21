using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class AppraisalGeneralQuestions : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int AppraisalGeneralQuestionsId { get; set; }
        public int? SequenceNo { get; set; }
        public string Question { get; set; }
        public string DariQuestion { get; set; }
        public int? OfficeId { get; set; }
    }
}
