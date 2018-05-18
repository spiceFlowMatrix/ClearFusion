using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class CurrencyDetails : BaseEntityWithoutId
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1)]
		public int CurrencyId { get; set; }		
		[StringLength(5)]
        public string CurrencyCode { get; set; }
        [StringLength(50)]
        public string CurrencyName { get; set; }
        public float? CurrencyRate { get; set; }
        public List<ExchangeRate> ExchangeRateListFrom { get; set; }
        public List<ExchangeRate> ExchangeRateListTo { get; set; }
		public bool Status { get; set; }

	}
}
