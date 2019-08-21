using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Marketing
{
    public class InvoiceGeneration : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long InvoiceId { get; set; }
        [ForeignKey("JobId")]
        public long? JobId { get; set; }
        public JobDetails JobDetails { get; set; }
        public long? PlayoutMinutes { get; set; }
        public long? TotalMinutes { get; set; }
        public double? TotalPrice { get; set; }
        public long? JobPrice { get; set; }
        [ForeignKey("CurrencyId")]
        public long? CurrencyId { get; set; }
        public CurrencyDetails CurrencyDetails { get; set; }
    }
}
