using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Marketing
{
    public class JobPriceDetails : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long JobPriceId { get; set; }
        public double UnitRate { get; set; }
        public int Units { get; set; }
        public double FinalRate { get; set; }
        public double FinalPrice { get; set; }
        public double Discount { get; set; }
        public float DiscountPercent { get; set; }
        public double TotalPrice { get; set; }
        public bool IsInvoiceApproved { get; set; }
        [ForeignKey("JobId")]
        public long JobId { get; set; }
        public long Minutes { get; set; }
        public JobDetails JobDetails { get; set; }
    }
}
