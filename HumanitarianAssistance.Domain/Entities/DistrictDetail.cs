using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities
{
    public class DistrictDetail :BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long DistrictID {get;set;}
        [StringLength(50)]
        public string District { get; set; }
        public Nullable<int> ProvinceID { get; set; }

    }
}
