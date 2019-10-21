using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Accounting;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class PaymentTypes : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int PaymentId { get; set; }
        public string Name { get; set; }
        public long ChartOfAccountNewId { get; set; }
        [ForeignKey("ChartOfAccountNewId")]
        ChartOfAccountNew ChartOfAccountNew { get; set; }
    }
}
