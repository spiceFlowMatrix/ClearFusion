using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class StoreSourceCodeDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long SourceCodeId { get; set; }
        public int CodeTypeId { get; set; }
        [ForeignKey("CodeTypeId")]
        public CodeType CodeTypes { get; set; }
        public string Code { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string Fax { get; set; }
        [StringLength(100)]
        public string EmailAddress { get; set; }
        [StringLength(50)]
        public string Guarantor { get; set; }
    }
}
