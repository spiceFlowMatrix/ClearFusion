using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class JournalDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int JournalCode { get; set; }
        [StringLength(100)]
        public string JournalName { get; set; }
        public byte? JournalType { get; set; }
        public List<VoucherDetail> VoucherDetails { get; set; }
    }
}
