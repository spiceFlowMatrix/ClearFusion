using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Accounting;

namespace HumanitarianAssistance.Domain.Entities
{
    public class JournalDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int JournalCode { get; set; }
        [StringLength(100)]
        public string JournalName { get; set; }
        public byte? JournalType { get; set; }
		public List<VoucherDetail> VoucherDetails { get; set; }
	}
}
