using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Accounting
{
    public class ExchangeRate : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long ExchangeRateId { get; set; }
        public DateTime? Date { get; set; }
        public int? FromCurrency { get; set; }
        public int? ToCurrency { get; set; }
        public double? Rate { get; set; }
        [ForeignKey("FromCurrency")]
        public CurrencyDetails CurrencyFrom { get; set; }
        [ForeignKey("ToCurrency")]
        public CurrencyDetails CurrencyTo { get; set; }


        public string CurrencyCode { get; set; }
        public int? OfficeId { get; set; }
        public OfficeDetail OfficeDetail { get; set; }
        public string OfficeCode { get; set; }

    }
}
