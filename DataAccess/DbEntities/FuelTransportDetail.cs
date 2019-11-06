using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class FuelTransportDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int FuelTransportID { get; set; }
        [StringLength(50)]
	    public string Title { get; set; }
        public float? FuelCurrentKM { get; set; }
        public float? FuelStartKM { get; set; }
        public float? FuelTotalKM { get; set; }
        public float? StandardNormKM { get; set; }
        public float? ConsumedNormKM { get; set; }
        public float? NormDifference { get; set; }
        public float? FuelUsed { get; set; }
        public float? RatePerLiter { get; set; }
        public float? FuelTotalPrice { get; set; }
        public float? MobilOilCurrentKM { get; set; }
        public float? MobilOilStartKM { get; set; }
        public float? MobilOilTotalKM { get; set; }
        public float? MobilOilStandardNorm { get; set; }
        public float? MobilOilUsed { get; set; }
        public float? MobilOilRatePerLiter { get; set; }
        public float? MobilOilTotalPrice { get; set; }
        public float? SpareParts { get; set; }
        public float? Wages { get; set; }
        public float TotalExpenses { get; set; }
        public string Remarks { get; set; }
        public DateTime? FuelDate { get; set; }
        [StringLength(10)]
        public string OfficeCode { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        [StringLength(20)]
        public string NumberPlate { get; set; }
        [StringLength(50)]
        public string Location { get; set; }
        public string Attachments { get; set; }
    }
}
