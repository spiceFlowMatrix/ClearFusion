using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class ProgramDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long ProgramId { get; set; }
        [ForeignKey("ProjectId")]
        public long ProjectId { get; set; }
        public string ProgramName { get; set; }
        public string ProgramCode { get; set; }

    }
}
