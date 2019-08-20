using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities
{
    public class CurrencyDetails : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int CurrencyId { get; set; }
        [StringLength(5)]
        public string CurrencyCode { get; set; }
        [StringLength(50)]
        public string CurrencyName { get; set; }
    }
}
