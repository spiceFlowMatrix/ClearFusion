using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class MonthlyExchangeRateDetail : BaseEntity
    {
        public int? RateMonth { get; set; }
        public int? RateYear { get; set; }
        [StringLength(5)]
        public string CurrencyCode { get; set; }
        public float? Rate { get; set; }
    }
}
