using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class EmployeeLanguages : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int SpeakLanguageId { get; set; }
        //public int LanguageName { get; set; }
        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public LanguageDetail LanguageDetail { get; set; }
        public int Reading { get; set; }
        public int Writing { get; set; }
        public int Speaking { get; set; }
        public int Listening { get; set; }
        public int EmployeeId { get; set; }

    }
}
