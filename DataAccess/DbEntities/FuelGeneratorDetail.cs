using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class FuelGeneratorDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int FuelGeneratorID { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(50)]
	    public string GeneratorLocation { get; set; }
        public float? StartUsed { get; set; }
        public float? CurrentUsed { get; set; }
        public float? TotalMonthUse { get; set; }
        public float? StandardNorm { get; set; }
        public float? ConsumedNorm { get; set; }
        public float? NormDifference { get; set; }
        public float? FuelUsed { get; set; }
        public float? FuelRatePerLiter { get; set; }
        public float? FuelTotalPrice { get; set; }
        public float? MobilOilCurrentHR { get; set; }
        public float? MobilOilStartHR { get; set; }
        public float? MobilOilTotalHR { get; set; }
        public float? MobilOilStandardNorm { get; set; }
        public float? MobilOilUsed { get; set; }
        public float? MobilOilRatePerLiter { get; set; }
        public float? MobilOilTotalPrice { get; set; }
        public float? SpareParts { get; set; }
        public float? Wages { get; set; }
        public float? TotalExpenses { get; set; }
        public string Remarks { get; set; }
        [StringLength(10)]
        public string OfficeCode { get; set; }
        public DateTime? FuelDate { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string Attachments { get; set; }
    }
}
