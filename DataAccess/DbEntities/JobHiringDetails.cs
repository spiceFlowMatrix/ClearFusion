using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class JobHiringDetails : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long JobId { get; set; }
        [StringLength(50)]
        public string JobCode { get; set; }
        public string JobDescription { get; set; }
        public int? ProfessionId { get; set; }
        public ProfessionDetails ProfessionDetails { get; set; }
        public int Unit { get; set; }
        public int OfficeId { get; set; }
        public OfficeDetail OfficeDetails { get; set; }
        public bool IsActive { get; set; }
        public int? GradeId { get; set; }
        [ForeignKey("GradeId")]
        public JobGrade JobGrade { get; set; }
    }
}
