using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class ExchangeRateDetail : BaseEntity
    {
        [Key, Column(Order = 0)]
        [StringLength(10)]
        public string RegCode { get; set; }
        [Key, Column(Order = 1)]
        public DateTime RateDate { get; set; }
        [Key, Column(Order = 2)]
        [StringLength(5)]
	    public string CurrencyCode { get; set; }
        public float? Rate { get; set; }
        public Boolean? Sent { get; set; }
        public Boolean? Status { get; set; }
    }
}
