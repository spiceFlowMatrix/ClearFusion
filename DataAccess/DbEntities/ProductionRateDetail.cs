using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class ProductionRateDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int RateID { get; set; }
        [StringLength(50)]
        public string JobNature { get; set; }
        public int? Duration { get; set; }
        public float? UnitRate { get; set; }
        public float? Price { get; set; }
        [StringLength(50)]
        public string Quality { get; set; }
        [StringLength(10)]
        public string Medium { get; set; }
    }
}
