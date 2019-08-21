using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Accounting
{
    public class ExchangeRateDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int ExchangeRateId { get; set; }
        public int FromCurrency { get; set; }
        public int ToCurrency { get; set; }
        public DateTime Date { get; set; }
        public decimal Rate { get; set; }
        public int OfficeId { get; set; }
    }
}
