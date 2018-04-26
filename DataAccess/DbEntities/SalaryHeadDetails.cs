using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class SalaryHeadDetails : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int SalaryHeadId { get; set; }
        public int HeadTypeId { get; set; }
        [StringLength(50)]
        public string HeadName { get; set; }
        public string Description { get; set; }
    }
}
