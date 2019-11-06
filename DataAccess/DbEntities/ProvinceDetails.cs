using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class ProvinceDetails : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int ProvinceId { get; set; }
        [StringLength(50)]
        public string ProvinceName { get; set; }
        public int? CountryId { get; set; }
        public CountryDetails CountryDetails { get; set; }
    }
}
