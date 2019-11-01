using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class ExchangeRateDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int ExchangeRateId { get; set; }
        public int FromCurrency { get; set; }
        public int ToCurrency { get; set; }
        public DateTime Date { get; set; }
        public decimal Rate { get; set; }
        public int OfficeId { get; set; }
    }
}
