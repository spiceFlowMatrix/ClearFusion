using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Accounting;

namespace HumanitarianAssistance.Domain.Entities {
    public class JobGrade : BaseEntity {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        [Column (Order = 1)]
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public long? ChartOfAccountNewId { get; set; }

        [ForeignKey ("ChartOfAccountNewId")]
        public ChartOfAccountNew ChartOfAccount { get; set; }
    }
}