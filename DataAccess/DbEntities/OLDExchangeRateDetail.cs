using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class OLDExchangeRateDetail : BaseEntity
    {
        [StringLength(10)]
        public string RegCode { get; set; }
        public DateTime? RateDate { get; set; }
        [StringLength(5)]
        public string CurrencyCode { get; set; }
        public float? Rate { get; set; }
        public Boolean? Sent { get; set; }
        public Boolean? Status { get; set; }
    }
}
