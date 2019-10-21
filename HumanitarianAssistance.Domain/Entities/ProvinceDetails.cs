using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities
{
    public class ProvinceDetails : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int ProvinceId { get; set; }
        [StringLength(50)]
        public string ProvinceName { get; set; }
        public int? CountryId { get; set; }
        public CountryDetails CountryDetails { get; set; }
    }
}
