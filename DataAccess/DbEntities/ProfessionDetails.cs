using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class ProfessionDetails : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int ProfessionId { get; set; }
        [StringLength(100)]
        public string ProfessionName { get; set; }
        public string ProfessionDari { get; set; }
    }
}
