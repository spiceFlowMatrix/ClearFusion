using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Project
{
    public class ProjectJobDetail: BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long ProjectJobId { get; set; }
        public string ProjectJobCode { get; set; }
        public string ProjectJobName { get; set; }
      

    }
}
