using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project {
    public class EducationDegreeDetail : BaseEntity {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        [Column (Order = 1)]
        public long EducationDegreeId { get; set; }

        [StringLength (100)]
        public string EducationDegreeName { get; set; }
    }
}