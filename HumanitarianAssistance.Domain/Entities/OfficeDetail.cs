using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities
{
    public class OfficeDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int OfficeId { get; set; }
        [StringLength(5)]
        public string OfficeCode { get; set; }
        [StringLength(100)]
        public string OfficeName { get; set; }
        [StringLength(100)]
        public string SupervisorName { get; set; }
        [StringLength(50)]
        public string PhoneNo { get; set; }
        [StringLength(50)]
        public string FaxNo { get; set; }
        public string OfficeKey { get; set; }
    }
}
