using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Project
{
   public class TargetBeneficiaryDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long TargetId { get; set; }
        [ForeignKey("ProjectId")]
        public long ProjectId { get; set; }
        public int TargetType { get; set; }
        public string TargetName { get; set; }    
    }
}
