using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Project
{
   public class SecurityConsiderationMultiSelect: BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long SecurityConsiderationMultiSelectId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectDetail ProjectDetail { get; set; }
        public long ProjectId { get; set; }
        [ForeignKey("SecurityConsiderationId")]
        public SecurityConsiderationDetail SecurityConsiderationDetail { get; set; }
        public long SecurityConsiderationId { get; set; }
        public long? SecurityConsiderationSelectedId { get; set; }


    }
}
